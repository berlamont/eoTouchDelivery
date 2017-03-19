using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Converters
{
    /// <summary>
    /// Convert a byte array into an ImageSource object that can be bound to
    /// the <seealso cref="Image"/> control
    /// </summary>
    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return (ImageSource) null;
            var imageAsBytes = (byte[])value;
            var ms = new MemoryStream(imageAsBytes);
            var retSource = ImageSource.FromStream(() => ms);
            ms.Position = 0;

            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}