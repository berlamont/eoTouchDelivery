using eoTouchDelivery.Core.Interfaces;
using eoTouchDelivery.Core.Services;
using eoTouchDelivery.Core.ViewModels.Base;
using FFImageLoading.Forms.Touch;
using Foundation;
using UIKit;

namespace eoTouchDelivery.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.BlackOpaque, false);
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
			{
				TextColor = UIColor.White
			});
			global::Xamarin.Forms.Forms.Init();
			Xamarin.FormsMaps.Init();
			CachedImageRenderer.Init();

			ViewModelLocator.Instance.RegisterSingleton<INavigationService, iOSNavigationService>();

			LoadApplication(new App());

			return base.FinishedLaunching(application, launchOptions);
		}
	}
}
