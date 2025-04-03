using eoTouchDelivery.Core.Helpers;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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

			// TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
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