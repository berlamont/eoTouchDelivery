﻿using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace eoTouchDelivery.Core.Converters
{
    /// <summary>
    /// This is a converter which can be used with the EventToCommandBehavior to 
    /// retrieve a single property or field from an EventArgs class coming from 
    /// an event to then pass into an ICommand as the parameter.
    /// </summary>
    /// <example>
    /// <!--[CDATA[
    /// <ListView ...>
    ///    <ListView.Behaviors>
    ///        <inf:EventToCommandBehavior Command = "{Binding TheCommand}" EventName="ItemTapped">
    ///            <inf:EventToCommandBehavior.EventArgsConverter>
    ///                <cvt:EventArgsConverter PropertyName = "Item" />
    ///            </inf:EventToCommandBehavior.EventArgsConverter>
    ///        </inf:EventToCommandBehavior>
    ///    </ListView.Behaviors>
    /// </ListView>
    /// ]]>-->
    /// </example>
    public class EventArgsConverter : IValueConverter
    {
        /// <summary>
        /// The property (or field) to retrieve; must be public and
        /// use exact casing.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Takes the EventArgs passed in the 'parameter' and pulls a single
        /// property or field value from it as the return value.
        /// </summary>
        /// <returns>Property or field value</returns>
        /// <param name="value">Event sender</param>
        /// <param name="targetType">Expected target type</param>
        /// <param name="parameter">EventArgs from event</param>
        /// <param name="culture">UI culture</param>
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Do some validation checks up front.
            if (parameter == null)
                return null;
            if (string.IsNullOrEmpty (PropertyName))
                throw new ArgumentNullException (nameof (PropertyName), $"{nameof (PropertyName)} must be set");

            var theType = parameter.GetType ().GetTypeInfo ();

            // Look for a public property first.
            var pi = theType.GetDeclaredProperty (PropertyName);
            if (pi != null) 
                return pi.GetValue (parameter);

            // Not found - see if it's a public field. This is unusual, but 
            // sometimes done on EventArgs types.
            var fi = theType.GetDeclaredField (PropertyName);
            if (fi == null)
                throw new ArgumentException ($"{nameof (PropertyName)} not found on {value.GetType ()}");

            return fi.GetValue (parameter);
        }

        /// <summary>
        /// Used to convert the value back from source > tatget. Not used with this converter
        /// </summary>
        /// <returns>Exceptiopn</returns>
        /// <param name="value">sender of the event</param>
        /// <param name="targetType">Expected target type</param>
        /// <param name="parameter">Event Args value</param>
        /// <param name="culture">UI culture</param>
        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException ();
        }
    }
}
