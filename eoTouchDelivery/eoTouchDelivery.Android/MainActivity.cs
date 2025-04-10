﻿using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using eoTouchDelivery.Core;
using FFImageLoading.Forms.Droid;

namespace eoTouchDelivery.Droid
{
	[Activity(Label = "eoTouchDelivery.Android", Icon = "@drawable/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			Xamarin.FormsMaps.Init(this, bundle);
			UserDialogs.Init(this);
			CachedImageRenderer.Init();

			LoadApplication(new App());
		}
	}
}