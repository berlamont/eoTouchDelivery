using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Android.Support.V7.App;

namespace eoTouchDelivery.Droid.Activities
{
	[Activity(Label = "eoTouchDelivery",
		Icon = "@drawable/icon",
		Theme = "@style/Theme.Splash",
		MainLauncher = true,
		NoHistory = true,
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			InvokeMainActivity();
		}

		void InvokeMainActivity()
		{
			var mainActivityIntent = new Intent(this, typeof(MainActivity));
			StartActivity(mainActivityIntent);
		}
	}
}