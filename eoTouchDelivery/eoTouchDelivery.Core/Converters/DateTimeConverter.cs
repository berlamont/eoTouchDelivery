﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
            {
                throw new InvalidOperationException("The target must be a DateTime");
            }

            var date = (DateTime)value;
            bool converToLocal = (string)parameter == "ToLocal";

            var result = converToLocal
                ? date.ToLocalTime().ToString("dddd, MMMM dd")
                : date.ToString("dddd, MMMM dd");
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
