#define DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using eoTouchDelivery.Core.Interfaces;

namespace eoTouchDelivery.Core.Services
{
    /// <summary>
    /// Wrapper around static Xamarin.Forms DependencyService to allow it to
    /// be turned into a mockable interface for unit testing.
    /// </summary>
    public class DependencyServiceWrapper : IDependencyService
    {
        readonly MethodInfo _genericGetMethod;
        static readonly Dictionary<Type, object> DependencyInstances = new Dictionary<Type, object>();

        /// <summary>
        /// Constructor for the DS wrapper.
        /// </summary>
        public DependencyServiceWrapper()
        {
            _genericGetMethod = GetType().GetTypeInfo().GetDeclaredMethod("Get");
        }

        /// <summary>
        /// Retrieve a dependency based on the abstraction <typeparamref name="T"/>.
        /// This extends the default DependencyService capability by allowing this method
        /// to create types which are not registered but have a public constructor.
        /// </summary>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T Get<T>() where T : class
        {
            var targetType = typeof(T);
            if (DependencyInstances.ContainsKey(targetType))
                return DependencyInstances[targetType] as T;

            // Try the underlying DependencyService.
            var value = Xamarin.Forms.DependencyService.Get<T>();
            if (value != null)
                return value;

            try
            {
                // Try to create it ourselves.
                var typeInfo = targetType.GetTypeInfo();
                if (typeInfo.IsInterface || typeInfo.IsAbstract)
                    return null;

                // Look for a public, default constructor first.
                var ctors = typeInfo.DeclaredConstructors.Where(c => c.IsPublic).ToArray();
                if (ctors.Length == 0)
                    return null;

                var ctor = ctors.FirstOrDefault(c => c.GetParameters().Length == 0);
                if (ctor != null)
                    return Activator.CreateInstance(targetType) as T;

                // Pick the first public constructor found and create any parameters.
                return Activator.CreateInstance(targetType, ctors.First().GetParameters()
                    .Select(p => _genericGetMethod.MakeGenericMethod(p.ParameterType)
                    .Invoke(this, null))
                    .ToArray()) as T;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DependencyServiceWrapper failed to create {targetType.Name}: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Register a specific type as an abstraction
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        public void Register<T> () where T : class, new()
        {
			Xamarin.Forms.DependencyService.Register<T> ();
        }

        /// <summary>
        /// Register a type along with an abstraction type.
        /// </summary>
        /// <typeparam name="T">Abstraction type</typeparam>
        /// <typeparam name="TImpl">Type to create</typeparam>
        public void Register<T, TImpl> () 
            where T : class
            where TImpl : class, T, new()
        {
			Xamarin.Forms.DependencyService.Register<T, TImpl> ();
        }

        /// <summary>
        /// Register a specific instance with a type. This extends the
        /// built-in DependencyService by allowing a specific instance to be registered.
        /// </summary>
        /// <typeparam name="T">Type to register</typeparam>
        /// <param name="impl">Implementation</param>
        public void Register<T>(T impl) where T : class
        {
            DependencyInstances.Add(typeof(T), impl);
        }
    }
}
