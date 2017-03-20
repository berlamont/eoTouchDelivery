using System;
using eoTouchDelivery.Core.DataServices;
using eoTouchDelivery.Core.DataServices.Base;
using eoTouchDelivery.Core.Interfaces;
using eoTouchDelivery.Core.Services;
using eoTouchDelivery.ViewModels;
using Microsoft.Practices.Unity;

namespace eoTouchDelivery.Core.ViewModels.Base
{
	public class ViewModelLocator
	{
		readonly IUnityContainer _unityContainer;

		protected ViewModelLocator()
		{
			_unityContainer = new UnityContainer();

			// providers
			_unityContainer.RegisterType<IRequestProvider, RequestProvider>();
			_unityContainer.RegisterType<ILocationProvider, LocationProvider>();
			_unityContainer.RegisterType<IMediaPickerService, MediaPickerService>();

			// services
			_unityContainer.RegisterType<IDialogService, DialogService>();
			RegisterSingleton<INavigationService, NavigationService>();

			// data services
			_unityContainer.RegisterType<IAuthenticationService, AuthenticationService>();
			_unityContainer.RegisterType<IProfileService, ProfileService>();

			// view models
			_unityContainer.RegisterType<CredentialViewModel>();
			_unityContainer.RegisterType<HomeViewModel>();
			_unityContainer.RegisterType<LoginViewModel>();
			_unityContainer.RegisterType<MainViewModel>();
			_unityContainer.RegisterType<MenuViewModel>();
			_unityContainer.RegisterType<ProfileViewModel>();
			_unityContainer.RegisterType<UserViewModel>();
			_unityContainer.RegisterType<ReportsViewModel>();
			_unityContainer.RegisterType<BookingViewModel>();
		}

		public static ViewModelLocator Instance { get; } = new ViewModelLocator();

		public T Resolve<T>() => _unityContainer.Resolve<T>();

		public object Resolve(Type type) => _unityContainer.Resolve(type);

		public void Register<T>(T instance)
		{
			_unityContainer.RegisterInstance(instance);
		}

		public void Register<TInterface, T>() where T : TInterface
		{
			_unityContainer.RegisterType<TInterface, T>();
		}

		public void RegisterSingleton<TInterface, T>() where T : TInterface
		{
			_unityContainer.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
		}
	}
}