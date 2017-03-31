using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.ComponentModel;
using eoTouchDelivery.Core.Effects;
using eoTouchDelivery.Droid.Effects;

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