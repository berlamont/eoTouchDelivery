using eoTouchDelivery.Droid.Renderers;
using System;
using eoTouchDelivery.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace eoTouchDelivery.Droid.Renderers
{
    public class CustomProgressBarRenderer : ProgressBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

            try
            {
                var solidTransparentColor = new Color(255, 255, 255, 1.0);
                Control.ProgressDrawable.SetColorFilter(solidTransparentColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
                Control.ProgressTintList = Android.Content.Res.ColorStateList.ValueOf(solidTransparentColor.ToAndroid());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}