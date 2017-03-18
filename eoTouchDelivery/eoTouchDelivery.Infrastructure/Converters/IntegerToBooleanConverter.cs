using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchDelivery.Converters
{
    /// <summary>
    /// Converts an integer value into a boolean true/false
    /// </summary>
    public class IntegerToBooleanConverter : IMarkupExtension, IValueConverter
    {
        /// <summary>
        /// Boolean value for zero; defaults to false.
        /// </summary>
        public bool ZeroOrNull { get; set; }

        /// <summary>
        /// Boolean value for non-zero; defaults to true.
        /// </summary>
        public bool Positive { get; set; }

        /// <summary>
        /// Boolean value for negative treqtment; defaults to false.
        /// </summary>
        /// <value><c>true</c> if negative treatment; otherwise, <c>false</c>.</value>
        public bool Negative { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public IntegerToBooleanConverter()
        {
            Positive = true;
            ZeroOrNull = false;
            Negative = false;
        }

        #region IValueConverter Members

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return ZeroOrNull;

            int result = System.Convert.ToInt32 (value, culture);
            return result < 0 ? Negative 
                : result == 0 
                    ? ZeroOrNull 
                    : Positive;
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
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Returns the converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
