﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Utils;
using eoTouchDelivery.Core.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchDelivery.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class iOSMainPage : TabbedPage
	{
		public iOSMainPage()
		{
			InitializeComponent();
			CurrentPageChanged += OnCurrentPageChanged;
			MessagingCenter.Subscribe<WalkAround>(this, MessengerKeys.WalkAroundRequested, OnWalkAroundRequested);
			MessagingCenter.Subscribe<WalkAround>(this, MessengerKeys.WalkAroundFinished, OnWalkAroundFinished);
			MessagingCenter.Subscribe<ReportRequest>(this, MessengerKeys.GoBackFromReportRequest, OnGoBackFromReportRequested);
		}

		public void AddPage(Page page, string title)
		{
			var navigationPage = new CustomNavigationPage(page)
			{
				Title = title,
				Icon = GetIconForPage(page)
			};

			if (page is WalkAroundPage || page is ReportsPage)
			{
				navigationPage.IsEnabled = Settings.CurrentWalkAroundId != 0;
			}

			if (_previousPage == null)
			{
				_previousPage = page;
			}

			Children.Add(navigationPage);
		}

		public bool TrySetCurrentPage(Page requiredPage)
		{
			return TrySetCurrentPage(requiredPage.GetType());
		}

		public bool TrySetCurrentPage(Type requiredPageType)
		{
			CustomNavigationPage page = GetTabPageWithInitial(requiredPageType);

			if (page != null)
			{
				CurrentPage = null;
				CurrentPage = page;
			}

			return page != null;
		}

		public async Task ClearNavigationForPage(Type type)
		{
			CustomNavigationPage page = GetTabPageWithInitial(type);

			if (page != null)
			{
				await page.Navigation.PopToRootAsync(false);
			}
		}

		CustomNavigationPage GetTabPageWithInitial(Type type)
		{
			CustomNavigationPage page = Children.OfType<CustomNavigationPage>()
				.FirstOrDefault(p =>
				{
					return p.CurrentPage.Navigation.NavigationStack.Count > 0
						? p.CurrentPage.Navigation.NavigationStack[0].GetType() == type
						: false;
				});

			return page;
		}

		static string GetIconForPage(Page page)
		{
			var icon = string.Empty;

			switch (page)
			{
				case
					if (page is HomePage)
					{
						
					}

					break;
			}

			if (page is HomePage)
			{
				icon = "menu_ic_home";
			}
			else if (page is ProfilePage)
			{
				icon = "menu_ic_profile";
			}
			else if (page is ReportsPage)
			{
				icon = "menu_ic_report_incident";
			}


			return icon;
		}

		void OnCurrentPageChanged(object sender, EventArgs e)
		{
			if (CurrentPage == null)
			{
				return;
			}

			if (!CurrentPage.IsEnabled)
			{
				CurrentPage = _previousPage;
			}
			else
			{
				_previousPage = CurrentPage;
				MessagingCenter.Send(this, MessengerKeys.iOSMainPageCurrentChanged);
			}
		}

		void OnWalkAroundRequested(WalkAround WalkAround)
		{
			SetMenuItemStatus(typeof(WalkAroundPage), true);
			SetMenuItemStatus(typeof(ReportsPage), true);
		}

		void OnWalkAroundFinished(WalkAround WalkAround) => SetMenuItemStatus(typeof(WalkAroundPage), false);

		void SetMenuItemStatus(Type pageType, bool enabled)
		{
			Page page = Children
				.OfType<CustomNavigationPage>()
				.FirstOrDefault(nav => nav.CurrentPage.GetType() == pageType);

			if (page != null)
			{
				page.IsEnabled = enabled;
			}
		}

		void OnGoBackFromReportRequested(ReportRequest issue) => TrySetCurrentPage(typeof(HomePage));
	}
}