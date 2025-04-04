using Android.Widget;
using System.ComponentModel;
using eoTouchDelivery.Core.Effects;
using eoTouchDelivery.Droid.Effects;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;

[assembly: ExportEffect(typeof(MaxLinesEffect), "MaxLinesEffect")]
namespace eoTouchDelivery.Droid.Effects
{
	public class MaxLinesEffect : PlatformEffect
	{
		TextView _control;

		protected override void OnAttached()
		{
			_control = Control as TextView;
			SetMaxLines();
		}

		protected override void OnDetached()
		{
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
		{
			if (args.PropertyName == NumberOfLinesEffect.NumberOfLinesProperty.PropertyName)
			{
				SetMaxLines();
			}
		}

		private void SetMaxLines()
		{
			var maxLines = NumberOfLinesEffect.GetNumberOfLines(Element);
			_control?.SetMaxLines(maxLines);
		}
	}
}