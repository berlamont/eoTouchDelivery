using System;
using eoTouchDelivery.Core.Models;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;

namespace eoTouchDelivery.Core.Helpers
{
	public static class MapHelper
	{
		public static GeoLocation DefaultLocation = new GeoLocation {Latitude = 42.7670579, Longitude = -86.05780949};

		public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2, char unit)
		{
			var theta = lon1 - lon2;
			var dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));
			dist = Math.Acos(dist);
			dist = Rad2Deg(dist);
			dist = dist * 60 * 1.1515;
			if (unit == 'K')
				dist = dist * 1.609344;
			else if (unit == 'N')
				dist = dist * 0.8684;
			return dist;
		}

		static double Deg2Rad(double deg)
		{
			return deg * Math.PI / 180.0;
		}

		static double Rad2Deg(double rad)
		{
			return rad / Math.PI * 180.0;
		}

		public static void CenterMapInDefaultLocation(Map map)
		{
			var initialPosition = new Location(DefaultLocation.Latitude, DefaultLocation.Longitude);

			var mapSpan = MapSpan.FromCenterAndRadius(initialPosition, Distance.FromMiles(1.0));

			map.MoveToRegion(mapSpan);
		}
	}
}