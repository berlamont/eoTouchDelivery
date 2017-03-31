using Android.Widget;
using eoTouchDelivery.Core.Effects;
using System;
using System.ComponentModel;
using eoTouchDelivery.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(DatePickerLineColorEffect), "DatePickerLineColorEffect")]
namespace eoTouchDelivery.Droid.Effects
{
	public class DatePickerLineColorEffect : PlatformEffect
	{
		EditText control;

		protected override void OnAttached()
		{
			control = Control as EditText;
			UpdateLineColor();
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
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}
	}
}