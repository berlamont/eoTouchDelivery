using System;
using System.Globalization;
using eoTouchDelivery.Core.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace eoTouchDelivery.Core.Converters
{
    public class MapColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pin = value as CustomPin;

            if (pin != null)
            {
                if (pin.Type == CustomPin.AnnotationType.From)
                    return Color.FromArgb("3062F5");

                if (pin.Type == CustomPin.AnnotationType.To)
                    return Color.FromArgb("FF5E4C");
            }

            return Colors.Blue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
