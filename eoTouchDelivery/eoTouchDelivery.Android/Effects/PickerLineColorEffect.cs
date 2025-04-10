﻿using Android.Widget;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Diagnostics;
using eoTouchDelivery.Core.Effects;
using eoTouchDelivery.Droid.Effects;

[assembly: ExportEffect(typeof(PickerLineColorEffect), "PickerLineColorEffect")]
namespace eoTouchDelivery.Droid.Effects
{
	public class PickerLineColorEffect : PlatformEffect
	{
		EditText control;

		protected override void OnAttached()
		{
			try
			{
				control = Control as EditText;
				UpdateLineColor();
			} catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
			control = null;
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
		{
			if (args.PropertyName == LineColorEffect.LineColorProperty.PropertyName)
			{
				UpdateLineColor();
			}
		}

		private void UpdateLineColor()
		{
			try
			{
				control.Background.SetColorFilter(LineColorEffect.GetLineColor(Element).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
			} catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}