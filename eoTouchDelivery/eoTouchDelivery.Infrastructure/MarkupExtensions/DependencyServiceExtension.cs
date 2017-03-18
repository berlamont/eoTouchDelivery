using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eoTouchDelivery.Infrastructure.MarkupExtensions
{
    /// <summary>
    /// This markup extension allows XAML to lookup dependencies 
    /// using the <see cref="DependencyService"/>.
    /// </summary>
    /// <example>
    /// <code>
    /// BindingContext="{DependencyService Type={x:Type someVMType}}"
    /// </code>
    /// </example>
    [ContentProperty ("Type")]
    public class DependencyServiceExtension : IMarkupExtension
    {
        /// <summary>
        /// Fetch target type for <seealso cref="DependencyService"/>
        /// </summary>
        /// <value>The fetch target.</value>
        public DependencyFetchTarget FetchTarget { get; set; }

        /// <summary>
        /// Type to retrieve (interface or class)
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Initializes the markup extension
        /// </summary>
        public DependencyServiceExtension()
        {
            // Default is a global instance.
            FetchTarget = DependencyFetchTarget.GlobalInstance;
        }

        /// <summary>
        /// Looks up the specified type and returns it to the XAML parser.
        /// </summary>
        /// <returns>Retrieved object</returns>
        /// <param name="serviceProvider">Service provider.</param>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Type == null)
                throw new InvalidOperationException("Type argument mandatory for DependencyService extension");

            // DependencyService.Get<T>();
            var mi = typeof (DependencyService).GetTypeInfo().GetDeclaredMethod("Get");
            var cmi = mi.MakeGenericMethod(Type);
            return cmi.Invoke(null, new object[] { FetchTarget });
        }
    }
}
