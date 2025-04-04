using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using eoTouchDelivery.Droid.Effects;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;


[assembly: ExportEffect(typeof(UnderlineTextEffect), "UnderlineTextEffect")]
namespace eoTouchDelivery.Droid.Effects
{
	public class UnderlineTextEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			var label = Control as TextView;

			if (label != null)
			{
				label.PaintFlags |= Android.Graphics.PaintFlags.UnderlineText;
			}
		}

		protected override void OnDetached()
		{
		}
	}
}