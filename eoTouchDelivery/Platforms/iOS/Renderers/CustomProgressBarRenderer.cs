using CoreGraphics;
using eoTouchDelivery.Core.Controls;
using eoTouchDelivery.iOS.Renderers;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;

// TODO Xamarin.Forms.ExportRendererAttribute is not longer supported. For more details see https://github.com/dotnet/maui/wiki/Using-Custom-Renderers-in-.NET-MAUI
[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace eoTouchDelivery.iOS.Renderers
{
    public class CustomProgressBarRenderer : ProgressBarRenderer
    {
        protected override void OnElementChanged(
            ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

            Control.ProgressTintColor = Color.FromRgb(255, 255, 255).ToUIColor();
            var semiTransparentColor = new Color(255, 255, 255, 0.5);
            Control.TrackTintColor = semiTransparentColor.ToUIColor();
        }


        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            ClipsToBounds = true;
            Layer.MasksToBounds = true;
            Layer.CornerRadius = 5;

            var x = 1.0f;
            var y = 1.2f;

            var transform = CGAffineTransform.MakeScale(x, y);
            transform.TransformSize(Frame.Size);
            Transform = transform;
        }
    }
}
