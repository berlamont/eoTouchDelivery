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

[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]

namespace eoTouchDelivery.iOS.Effects
{
	public class EntryLineColorEffect : PlatformEffect
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
			var entry = Element as Entry;
			if (entry != null)
				Control.Bounds = new CGRect(0, 0, entry.Width, entry.Height);
		}

		void UpdateLineColor()
		{
			var lineLayer = _control.Layer.Sublayers.OfType<BorderLineLayer>().FirstOrDefault();

			if (lineLayer == null)
			{
				lineLayer = new BorderLineLayer
				{
					MasksToBounds = true,
					BorderWidth = 1.0f
				};
				_control.Layer.AddSublayer(lineLayer);
				_control.BorderStyle = UITextBorderStyle.None;
			}

			lineLayer.Frame = new CGRect(0f, Control.Frame.Height - 1f, Control.Bounds.Width, 1f);
			lineLayer.BorderColor = LineColorEffect.GetLineColor(Element).ToCGColor();
			_control.TintColor = _control.TextColor;
		}

		class BorderLineLayer : CALayer
		{
		}
	}
}