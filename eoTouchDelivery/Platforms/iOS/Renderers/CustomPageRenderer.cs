using System;
using CoreGraphics;
using eoTouchDelivery.iOS.Renderers;
using UIKit;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;

// TODO Xamarin.Forms.ExportRendererAttribute is not longer supported. For more details see https://github.com/dotnet/maui/wiki/Using-Custom-Renderers-in-.NET-MAUI
[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace eoTouchDelivery.iOS.Renderers
{
    public class CustomPageRenderer : PageRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var width = UIScreen.MainScreen.Bounds.Size.Width;
            var height = UIScreen.MainScreen.Bounds.Size.Height;

            var image = UIImage.FromBundle("background");
            var imageView = new UIImageView(new CGRect(0, 0, width, height))
            {
                Image = image,
                ContentMode = UIViewContentMode.ScaleAspectFill
            };
            View.AddSubview(imageView);
            View.SendSubviewToBack(imageView);
        }
    }
}
