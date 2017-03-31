using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using eoTouchDelivery.Droid.Effects;


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