using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Interfaces;

namespace eoTouchDelivery.Core.Services
{
    public static class SettingService
    {
        public const string ServerAddressKey = "ServerAddress";
        public const string DatabaseNameKey = "DatabaseName";
        public const string HandHeldNumberKey = "HandHeldNumber";

        public static string DefaultServerAddress = "10.10.10.10";
        public static string DefaultDatabaseName = "Your Database";
        public static string DefaultHandHeldNumber = "1";

        public static string ServerAddress => Current.GetValueOrDefault(ServerAddressKey, DefaultServerAddress);

        public static string DatabaseName => Current.GetValueOrDefault(DatabaseNameKey, DefaultDatabaseName);

        public static string HandHeldNumber => Current.GetValueOrDefault(HandHeldNumberKey, DefaultHandHeldNumber);

        static Lazy<ISettingService> _settings = new Lazy<ISettingService>(CreateSettings, LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static ISettingService Current
        {
            get
            {
                var ret = _settings.Value;
                if (ret == null)
                { throw NotImplementedInReferenceAssembly(); }
                return ret;
            }
        }

        static ISettingService CreateSettings() => Xamarin.Forms.DependencyService.Get<ISettingService>();

        internal static Exception NotImplementedInReferenceAssembly() => new NotImplementedException("Setting Not Implemented");
    }
}
