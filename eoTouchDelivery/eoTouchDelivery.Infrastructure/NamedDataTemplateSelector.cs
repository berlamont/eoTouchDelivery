using System;
using eoTouchDelivery.Infrastructure.Extensions;
using Xamarin.Forms;

namespace eoTouchDelivery.Infrastructure
{
    /// <summary>
    /// This is a simple DataTemplateSelector that matches resources by the typename of the
    /// object being data bound to the ListView.
    /// </summary>
    /// <remarks>
    /// To use it, add a copy into your resources and then assign it as the value for a
    /// <see cref="ListView"/> ItemTemplate. This will evaluate the bound object and, based
    /// on the typename, retrieve a resource (starting at that object and working up to App)
    /// by the name.
    /// </remarks>
    public class NamedDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// True to strip off the namespace and only look for the base typename.
        /// </summary>
        /// <value><c>true</c> if strip namespace; otherwise, <c>false</c>.</value>
        public bool StripNamespace { get; set; }

        /// <summary>
        /// Retrieves the DataTemplate for a given object using the typename of the 
        /// object as the resource key. Throws an exception if the resource is not found.
        /// </summary>
        /// <returns>The select template.</returns>
        /// <param name="item">Item.</param>
        /// <param name="container">Container.</param>
        protected override DataTemplate OnSelectTemplate (object item, BindableObject container)
        {
            if (item == null)
                throw new Exception ("Cannot create template for null object.");

            var itemType = item.GetType ();
            var typeName = (StripNamespace) ? itemType.Name : itemType.FullName;

            var ve = container as VisualElement;
            return ve.FindResource<DataTemplate> (typeName);
        }
   }
}

