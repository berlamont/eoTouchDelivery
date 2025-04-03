using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Controls
{
    public class CircularProgressControl : Grid
    {
        View _progress1;
        View _progress2;
        View _background1;
        View _background2;

        public CircularProgressControl()
        {
            // TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            _progress1 = Device.OS == TargetPlatform.Windows ? CreateImage("Assets/progress_done") : CreateImage("progress_done");
            // TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            _background1 = Device.OS == TargetPlatform.Windows ? CreateImage("Assets/progress_pending") : CreateImage("progress_pending");
            // TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            _background2 = Device.OS == TargetPlatform.Windows ? CreateImage("Assets/progress_pending") : CreateImage("progress_pending");
            // TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            _progress2 = Device.OS == TargetPlatform.Windows ? CreateImage("Assets/progress_done") : CreateImage("progress_done");
            HandleProgressChanged(1, 0);
        }

        View CreateImage(string v1)
        {
            var img = new Image {Source = ImageSource.FromFile(v1 + ".png")};
            Children.Add(img);
            return img;
        }

        public static BindableProperty ProgressProperty =
            BindableProperty.Create("Progress", typeof(double), typeof(CircularProgressControl), 0d, propertyChanged: ProgressChanged);

        static void ProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var c = bindable as CircularProgressControl;
            c?.HandleProgressChanged(Clamp((double) oldValue, 0, 1), Clamp((double) newValue, 0, 1));
        }

        static double Clamp(double value, double min, double max)
        {
            if (value <= max && value >= min)
                return value;
            return value > max ? max : min;
        }

        void HandleProgressChanged(double oldValue, double p)
        {
            if (p < .5)
            {
                if (oldValue >= .5)
                {
                    // this code is CPU intensive so only do it if we go from >=50% to <50%
                    _background1.IsVisible = true;
                    _progress2.IsVisible = false;
                    _background2.Rotation = 180;
                    _progress1.Rotation = 0;
                }
                var rotation = 360 * p;
                _background1.Rotation = rotation;
            }
            else
            {
                if (oldValue < .5)
                {
                    // this code is CPU intensive so only do it if we go from <50% to >=50%
                    _background1.IsVisible = false;
                    _progress2.IsVisible = true;
                    _progress1.Rotation = 180;
                }
                var rotation = 360 * p;
                _background2.Rotation = rotation;
            }
        }

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
    }
}
