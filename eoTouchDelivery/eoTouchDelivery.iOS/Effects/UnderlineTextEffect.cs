﻿using eoTouchDelivery.iOS.Effects;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(UnderlineTextEffect), "UnderlineTextEffect")]

namespace eoTouchDelivery.iOS.Effects
{
	public class UnderlineTextEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			var label = Control as UILabel;
			var element = Element as Label;

			if (label == null || element == null)
				return;
			var attributes = new UIStringAttributes {UnderlineStyle = NSUnderlineStyle.Single};
			var attrString = new NSAttributedString(element.Text, attributes);
			label.AttributedText = attrString;
		}

		protected override void OnDetached()
		{
		}
	}
}