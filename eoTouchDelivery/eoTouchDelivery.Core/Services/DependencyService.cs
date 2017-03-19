using System;
using System.Diagnostics;
using eoTouchDelivery.Core.Interfaces;

namespace eoTouchDelivery.Core.Services
{
    /// <summary>
    /// Identifies which services you want to register during the Init call.
    /// </summary>
    [Flags]
    public enum RegisterBehavior
    {
        /// <summary>
        /// Register the default (Forms) navigation service
        /// </summary>
        Navigation,
        /// <summary>
        /// Register the default (Forms) message visualizer
        /// </summary>
        MessageVisualizer
    }

    /// <summary>
    /// Static class to initialize the library
    /// </summary>
    public static class DependencyService
    {
        static IDependencyService _serviceLocator;

        /// <summary>
        /// This allows you to retrieve and customize the global dependency service
        /// used by the library (and app).
        /// </summary>
        /// <value>The service locator.</value>
        public static IDependencyService ServiceLocator => _serviceLocator ?? (_serviceLocator = new DependencyServiceWrapper ());

        /// <summary>
        /// Register the known services with the ServiceLocator type.
        /// </summary>
        /// <param name="defaultLocator">Service locator</param>
        /// <returns>IDependencyService</returns>
        public static IDependencyService Init(IDependencyService defaultLocator = null) => Init(defaultLocator, 
            RegisterBehavior.MessageVisualizer | RegisterBehavior.Navigation);

        /// <summary>
        /// Register the known services with the ServiceLocator type.
        /// </summary>
        /// <param name="registerBehavior">Services to register</param>
        /// <returns>IDependencyService</returns>
        public static IDependencyService Init(RegisterBehavior registerBehavior) => Init(null, registerBehavior);

        /// <summary>
        /// Registers the known services with the ServiceLocator type.
        /// </summary>
        /// <param name="defaultLocator">ServiceLocator, if null, DependencyService is used.</param>
        /// <param name="registerBehavior">Registration behavior</param>
        /// <returns>IDependencyService</returns>
        public static IDependencyService Init(IDependencyService defaultLocator, RegisterBehavior registerBehavior)
        {
            // If the ServiceLocator has already been set, then something used it before
            // Init was called. This is not allowed if they are going to change the locator.
            if (defaultLocator != null && _serviceLocator != null)
                throw new InvalidOperationException (
                    "Must call eoTouchDelivery.Init before using any library features; " +
                    "ServiceLocator has already been set.");

            // Assign the locator; either use the supplied one, or the default
            // DependencyService version if not supplied.
            if (defaultLocator == null)
            {
                defaultLocator = ServiceLocator;
            }
            else {
                Debug.Assert (_serviceLocator == null);
                _serviceLocator = defaultLocator;
            }

            // Register the services
            if (registerBehavior.HasFlag(RegisterBehavior.MessageVisualizer))
                defaultLocator.Register<IMessageVisualizerService, FormsMessageVisualizerService>();
            if (registerBehavior.HasFlag(RegisterBehavior.Navigation))
                defaultLocator.Register<INavigationService, FormsNavigationPageService>();

            defaultLocator.Register<IDependencyService>(defaultLocator);

            return defaultLocator;
        }
    }
}

