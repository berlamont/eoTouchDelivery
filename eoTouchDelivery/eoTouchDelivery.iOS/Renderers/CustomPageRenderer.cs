using System;
using CoreGraphics;
using eoTouchDelivery.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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
