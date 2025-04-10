﻿using eoTouchDelivery.Core.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchDelivery.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		const int ScrollMinLimit = 0;
		const int ScrollMaxLimit = 190;

		public HomePage()
		{
			InitializeComponent();
			mainScrollView.Scrolled += ScrollView_Scrolled;

			if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.Windows)
				ToolbarItems.Remove(ShowCustomRideToolbarItem);
		}

		void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
		{
			var val = MathHelper.ReMap(e.ScrollY, ScrollMinLimit, ScrollMaxLimit, 1, 0);

			infoPanel.Scale = val;
			infoPanel.Opacity = val;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			SuggestionsList.ListOrientation = Device.Idiom == TargetIdiom.Phone ? StackOrientation.Vertical : StackOrientation.Horizontal;
		}
	}
}