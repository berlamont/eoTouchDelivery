using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace eoTouchDelivery.Core.DataServices
{
	public class StaticDataModel
	{
		public static double Lattitude = 0;
		public static double Longitude = 0;
		public static List<Position> DecodePolylinePoints(string encodedPoints)
		{
			if (encodedPoints == null || encodedPoints == "") return null;
			List<Position> poly = new List<Position>();
			char[] polylinechars = encodedPoints.ToCharArray();
			int index = 0;

			int currentLat = 0;
			int currentLng = 0;
			int next5bits;
			int sum;
			int shifter;

			try
			{
				while (index < polylinechars.Length)
				{
					// calculate next latitude
					sum = 0;
					shifter = 0;
					do
					{
						next5bits = (int)polylinechars[index++] - 63;
						sum |= (next5bits & 31) << shifter;
						shifter += 5;
					} while (next5bits >= 32 && index < polylinechars.Length);

					if (index >= polylinechars.Length)
						break;

					currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

					//calculate next longitude
					sum = 0;
					shifter = 0;
					do
					{
						next5bits = (int)polylinechars[index++] - 63;
						sum |= (next5bits & 31) << shifter;
						shifter += 5;
					} while (next5bits >= 32 && index < polylinechars.Length);

					if (index >= polylinechars.Length && next5bits >= 32)
						break;

					currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
					Position p = new Position();
					p.Latitude = Convert.ToDouble(currentLat) / 100000.0;
					p.Longitude = Convert.ToDouble(currentLng) / 100000.0;
					poly.Add(p);
				}
			} catch (Exception ex)
			{
				// logo it
			}
			return poly;
		}
		public String makeURL(double sourcelat, double sourcelog, double destlat, double destlog)
		{
			StringBuilder urlString = new StringBuilder();
			urlString.Append("http://maps.googleapis.com/maps/api/directions/json");
			urlString.Append("?origin=");
			//  from
			urlString.Append(sourcelat.ToString());
			urlString.Append(",");
			urlString.Append(sourcelog);
			urlString.Append("&destination=");
			//  to
			urlString.Append(destlat);
			urlString.Append(",");
			urlString.Append(destlog);
			urlString.Append("&sensor=false&mode=driving&alternatives=true");
			urlString.Append("&key=AIzaSyBB7fJS19pceu_gVySoCT50aqNIEwHux9c");
			return urlString.ToString();
		}
	}
}
