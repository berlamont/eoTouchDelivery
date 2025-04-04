namespace eoTouchDelivery.Core.Utils
{
    public sealed class OnCustomPlatform<T>
    {
        public OnCustomPlatform()
        {
            Android = default(T);
            iOS = default(T);
            Windows = default(T);
            Other = default(T);
        }

        public T Android { get; set; }

        public T iOS { get; set; }

        public T Windows { get; set; }

        public T Other { get; set; }

        public static implicit operator T(OnCustomPlatform<T> onPlatform)
        {
            // TODO Xamarin.Forms.Device.OS is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            switch (Microsoft.Maui.Controls.Device.OS)
            {
                case Xamarin.Forms.TargetPlatform.Android:
                    return onPlatform.Android;
                case Xamarin.Forms.TargetPlatform.iOS:
                    return onPlatform.iOS;
               case Xamarin.Forms.TargetPlatform.Windows:
                        return onPlatform.Windows;
                default:
                    return onPlatform.Other;
            }
        }
    }
}