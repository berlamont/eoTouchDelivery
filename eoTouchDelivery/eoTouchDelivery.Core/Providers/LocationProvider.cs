using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Models;

namespace eoTouchDelivery.Core.Services
{
    public class LocationProvider : ILocationProvider
    {
        private readonly TimeSpan PositionReadTimeout = TimeSpan.FromSeconds(5);

        public async Task<ILocationResponse> GetPositionAsync()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await CrossGeolocator.Current.GetPositionAsync((int)PositionReadTimeout.TotalMilliseconds);

                var geolocation = new GeoLocation
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude,
                };

                return geolocation;
            }
            catch (GeolocationException geoEx)
            {
                Debug.WriteLine(geoEx);
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine(ex);
            }

            return DemoHelper.DefaultLocation;
        }
    }
}
