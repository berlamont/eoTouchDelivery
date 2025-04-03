using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eoTouchDelivery.Core.DataServices.Interfaces;
using eoTouchDelivery.Core.Models;
using eoTouchDelivery.Core.Pages;
using eoTouchDelivery.Core.ViewModels;
using eoTouchDelivery.Core.ViewModels.Base;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Services
{
	/// <summary>
	///     Service to manage the Xamarin.Forms Stack navigation system.
	///     This understands both <c>NavigationPage</c> and <c>MasterDetailPage</c> with
	///     an embedded navigation page.
	/// </summary>
	public class NavigationService : INavigationService
	{

		protected readonly Dictionary<Type, Type> _mappings;
		IAuthenticationService _authenticationService;

		protected Application CurrentApplication
		{
			get
			{
				return Application.Current;
			}
		}

		public NavigationService(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
			_mappings = new Dictionary<Type, Type>();

			CreatePageViewModelMappings();
			CreateMessengerSubscriptions();
		}

		public Task InitializeAsync()
		{
			return _authenticationService.IsAuthenticated ? NavigateToAsync<MainViewModel>() : NavigateToAsync<LoginViewModel>();
		}

		public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
		{
			return InternalNavigateToAsync(typeof(TViewModel), null);
		}

		public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
		{
			return InternalNavigateToAsync(typeof(TViewModel), parameter);
		}

		public Task NavigateToAsync(Type viewModelType)
		{
			return InternalNavigateToAsync(viewModelType, null);
		}

		public Task NavigateToAsync(Type viewModelType, object parameter)
		{
			return InternalNavigateToAsync(viewModelType, parameter);
		}

		public async Task NavigateBackAsync()
		{
			if (CurrentApplication.MainPage is MainPage)
			{
				var mainPage = CurrentApplication.MainPage as MainPage;
				await mainPage.Detail.Navigation.PopAsync();
			}
			else if (CurrentApplication.MainPage != null)
			{
				await CurrentApplication.MainPage.Navigation.PopAsync();
			}
		}

		public virtual Task RemoveLastFromBackStackAsync()
		{
			var mainPage = CurrentApplication.MainPage as MainPage;

			if (mainPage != null)
			{
				mainPage.Detail.Navigation.RemovePage(
					mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
			}

			return Task.FromResult(true);
		}

		protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
		{
			Page page = CreateAndBindPage(viewModelType, parameter);

			if (page is MainPage)
			{
				CurrentApplication.MainPage = page;
			}
			else if (page is LoginPage)
			{
				CurrentApplication.MainPage = new CustomNavigationPage(page);
			}
			else if (CurrentApplication.MainPage is MainPage)
			{
				var mainPage = CurrentApplication.MainPage as MainPage;
				var navigationPage = mainPage.Detail as CustomNavigationPage;

				if (navigationPage != null)
				{
					await navigationPage.PushAsync(page);
				}
				else
				{
					navigationPage = new CustomNavigationPage(page);
					mainPage.Detail = navigationPage;
				}

				mainPage.IsPresented = false;
			}
			else
			{
				var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

				if (navigationPage != null)
				{
					await navigationPage.PushAsync(page);
				}
				else
				{
					CurrentApplication.MainPage = new CustomNavigationPage(page);
				}
			}

			await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
		}

		protected Type GetPageTypeForViewModel(Type viewModelType)
		{
			if (!_mappings.ContainsKey(viewModelType))
			{
				throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
			}

			return _mappings[viewModelType];
		}

		protected Page CreateAndBindPage(Type viewModelType, object parameter)
		{
			Type pageType = GetPageTypeForViewModel(viewModelType);

			if (pageType == null)
			{
				throw new Exception($"Mapping type for {viewModelType} is not a page");
			}

			Page page = Activator.CreateInstance(pageType) as Page;
			ViewModelBase viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as ViewModelBase;
			page.BindingContext = viewModel;

			if (page is IPageWithParameters)
			{
				((IPageWithParameters)page).InitializeWith(parameter);
			}

			return page;
		}

		void CreatePageViewModelMappings()
		{
			_mappings.Add(typeof(HomeViewModel), typeof(HomePage));
			_mappings.Add(typeof(LoginViewModel), typeof(LoginPage));

			// TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
			if (Device.OS == TargetPlatform.Windows)
			{

				if (Device.Idiom == TargetIdiom.Desktop)
				{
					_mappings.Add(typeof(ProfileViewModel), typeof(UwpProfilePage));
				}
				else
				{
					_mappings.Add(typeof(ProfileViewModel), typeof(ProfilePage));
				}
			}
			else
			{
				_mappings.Add(typeof(ProfileViewModel), typeof(ProfilePage));
			}

			_mappings.Add(typeof(ReportViewModel), typeof(ReportsPage));
			_mappings.Add(typeof(MainViewModel), typeof(MainPage));
		}

		private void CreateMessengerSubscriptions()
		{
			MessagingCenter.Subscribe<ReportRequest>(this, MessengerKeys.GoBackFromReportRequest, GoBackFromReportRequested);
		}

		private async void GoBackFromReportRequested(ReportRequest issue)
		{
			// TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
			if (Device.OS != TargetPlatform.iOS)
			{
				await NavigateBackAsync();
			}
		}
	}
}