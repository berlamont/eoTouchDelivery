using System;
using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using eoTouchDelivery.Core.Effects;
using eoTouchDelivery.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(DatePickerLineColorEffect), "DatePickerLineColorEffect")]
namespace eoTouchDelivery.iOS.Effects
{
	public class DatePickerLineColorEffect : PlatformEffect
	{
		UITextField _control;

		protected override void OnAttached()
		{
			try
			{
				_control = Control as UITextField;
				UpdateLineColor();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
			_control = null;
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);

			if (args.PropertyName != LineColorEffect.LineColorProperty.PropertyName && args.PropertyName != "Height")
				return;
			Initialize();
			UpdateLineColor();
		}

		void Initialize()
		{
			var datePicker = Element as DatePicker;
			if (datePicker != null)
				Control.Bounds = new CGRect(0, 0, datePicker.Width, datePicker.Height);
		}

		void UpdateLineColor()
		{
			if (_control == null)
				return;

			var lineLayer = _control.Layer.Sublayers.OfType<BorderLineLayer>()
				.FirstOrDefault();

			if (lineLayer == null)
			{
				lineLayer = new BorderLineLayer
				{
					MasksToBounds = true,
					BorderWidth = 1.0f
				};
				_control.Layer.AddSublayer(lineLayer);
			}

			lineLayer.Frame = new CGRect(0f, Control.Frame.Height - 1f, Control.Bounds.Width, 1f);
			lineLayer.BorderColor = LineColorEffect.GetLineColor(Element).ToCGColor();
		}

		class BorderLineLayer : CALayer
		{
		}
	}
}