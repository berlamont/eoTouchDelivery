using System;
using Xamarin.Forms;

namespace eoTouchDelivery.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for the Xamarin.Forms <c>Element</c> class.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Find a visual ancestor from a point in our tree.
        /// </summary>
        /// <returns>The owner.</returns>
        /// <param name="view">View.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T FindOwner<T> (this Element view) where T : Element
        {
            do {

                if (view is T)
                    return (T)view;
                view = view.Parent;

            } while (view != null);
            return default (T);
        }

        /// <summary>
        /// Find resource from a given visual element.
        /// Throws an exception if the named resource does not exist.
        /// </summary>
        /// <returns>The owner.</returns>
        /// <param name="view">View.</param>
        /// <param name="name">Name of the resource to locate.</param>
        /// <typeparam name="T">Type of resource being retrieved</typeparam>
        public static T FindResource<T> (this VisualElement view, string name)
        {
            T resource;
            if (!TryFindResource (view, name, out resource))
                throw new Exception ("Resource '" + name + "' not found.");
            return resource;
        }

        /// <summary>
        /// Find resource from a given visual element.
        /// Returns true if the resource is found, false if not.
        /// </summary>
        /// <returns>The owner.</returns>
        /// <param name="view">View.</param>
        /// <param name="name">Name of the resource we are looking for</param>
        /// <param name="resource">Returned resource</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool TryFindResource<T> (this VisualElement view, string name, out T resource)
        {
            if (view == null) {
                var rd = Application.Current?.Resources;

                if (rd != null
                    && rd.ContainsKey (name)) {
                    resource = (T)rd [name];
                    return true;
                }
                resource = default (T);
                return false;
            }

            if (view.Resources != null
                && view.Resources.ContainsKey (name)) {
                resource = (T)view.Resources [name];
                return true;
            }

            return TryFindResource<T> (view.Parent as VisualElement, name, out resource);
        }
    }
}

