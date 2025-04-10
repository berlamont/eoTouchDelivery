﻿#define DEBUG
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchDelivery.Core.Converters
{
    /// <summary>
    /// This provides a debugging output for a binding converter
    /// </summary>
    public class DebugConverter : IValueConverter, IMarkupExtension
    {
        /// <summary>
        /// Outputs all parameters to the debug console.
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debug.WriteLine(string.Format(culture, "Convert: Value={0}, TargetType={1}, Parameter={2}, Culture={3}", 
                                          value, targetType, parameter, culture));
            return value;
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debug.WriteLine(string.Format(culture, "ConvertBack: Value={0}, TargetType={1}, Parameter={2}, Culture={3}",
                                          value, targetType, parameter, culture));
            return value;
        }

        /// <summary>
        /// Returns the converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            Debug.WriteLine("Creating DebugConverter()");
            return this;
        }
    }
}
