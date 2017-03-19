using System;
using System.Globalization;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Converters
{
    /// <summary>
    /// Support for validation with behaviors in XAML
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoolToObjectConverter<T> : IValueConverter
    {
        public T FalseObject { get; set; }

        public T TrueObject { get; set; }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture) => (bool)value ? TrueObject : FalseObject;

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture) => ((T)value).Equals(TrueObject);
    }
}