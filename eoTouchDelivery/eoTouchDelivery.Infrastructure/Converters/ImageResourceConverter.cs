using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchDelivery.Converters
{
    /// <summary>
    /// A custom IValueConverter which can be used with a {Binding} to convert
    /// a View Model string-based property into an ImageSource when the image
    /// is stored as an embedded resource. If you are not using a binding, then
    /// you can use the ImageResourceExtension to load an embedded resource.
    /// </summary>
    public class ImageResourceConverter : IValueConverter, IMarkupExtension
	{
        /// <summary>
        /// Prefix to prepend to the Resource ID (e.g. assembly + namespace + folder)
        /// Leave empty if the bound property specifies the full resource ID.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// A type in the same assembly as the image. This 
        /// is a required property set if you are in a UWP application
        /// and using .NET Native
        /// </summary>
        /// <value>The assembly.</value>
        public Type ResolvingType { get; set; }

        /// <summary>
        /// Convert a string-based value into an embedded resource
        /// </summary>
        /// <param name="value">Resource ID</param>
        /// <param name="targetType">ImageSource</param>
        /// <param name="parameter">Optional prefix</param>
        /// <param name="culture">Culture</param>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            if (targetType != typeof (ImageSource))
                throw new ArgumentException ("ImageResourceConverter should only be used with Image.Source");

            string resourceId = (value ?? "").ToString ();
            if (string.IsNullOrEmpty (resourceId))
                return null;

            string prefix;
            prefix = parameter != null 
                ? parameter.ToString () 
                : Prefix != null 
                           ? Prefix : "";
            if (!string.IsNullOrEmpty (prefix) 
                    && !prefix.EndsWith (".", StringComparison.Ordinal))
                prefix += ".";

            return ResolvingType != null 
                ? ImageSource.FromResource (prefix + resourceId, ResolvingType) 
                : ImageSource.FromResource (prefix + resourceId);
        }

        /// <summary>
        /// Used to convert a value from target > source; not used for this converter.
        /// </summary>
        /// <returns>Converted value</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Optional parameter</param>
        /// <param name="culture">Culture.</param>
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}

        /// <summary>
        /// Allows the value converter to be created inline; note that it is not
        /// shared if you use this approach.
        /// </summary>
        /// <returns>The Value Converter</returns>
        /// <param name="serviceProvider">Service provider.</param>
        public object ProvideValue (IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

