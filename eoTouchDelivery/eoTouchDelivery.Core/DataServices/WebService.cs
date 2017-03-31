using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator.Abstractions;

namespace eoTouchDelivery.Core.DataServices
{
	public class Polyline
	{
		public string points { get; set; }
	}
	public static class WebService
	{
		private const string ApiKey = "AIzaSyBB7fJS19pceu_gVySoCT50aqNIEwHux9c";
		//  private const string ApiKey = "AIzaSyCuQAaeu_jp_5L0AETvQ6vIC43RvULhonk";

		static HttpClient client = new HttpClient();
		public static List<Position> GetPolylineRoutes(string origin, string destination)
		{

			List<Position> Cordinates = new List<Position>();

			try
			{
				origin = origin.Replace(",", "");
				origin = origin.Replace(" ", "+");
				destination = destination.Replace(",", "");
				destination = destination.Replace(" ", "+");
				string url1 = "https://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&mode=Driving&key=" + ApiKey;



				HttpResponseMessage response = null;

				response = client.GetAsync(url1).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var datavalues = jObj["routes"][0].ToString();
						var root = JsonConvert.DeserializeObject<RootObject>(contents);
						var singleResult = root.routes[0].legs[0].steps;
						List<string> s = new List<string>();
						//foreach (var singleResult1 in root.routes[0].overview_polyline.points)
						//{
						//	s.Add(singleResult1.);

						//	// Do whatever you want with them.
						//}
						Cordinates = StaticDataModel.DecodePolylinePoints(root.routes[0].overview_polyline.points.ToString());




					}

				}
				return Cordinates;
			} catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			} finally
			{

			}

		}

		public class GeocodedWaypoint
		{
			public string geocoder_status { get; set; }
			public string place_id { get; set; }
			public List<string> types { get; set; }
			public bool? partial_match { get; set; }
		}

		public class Northeast
		{
			public double lat { get; set; }
			public double lng { get; set; }
		}

		public class Southwest
		{
			public double lat { get; set; }
			public double lng { get; set; }
		}

		public class Bounds
		{
			public Northeast northeast { get; set; }
			public Southwest southwest { get; set; }
		}

		public class Distance
		{
			public string text { get; set; }
			public int value { get; set; }
		}

		public class Duration
		{
			public string text { get; set; }
			public int value { get; set; }
		}

		public class EndLocation
		{
			public double lat { get; set; }
			public double lng { get; set; }
		}

		public class StartLocation
		{
			public double lat { get; set; }
			public double lng { get; set; }
		}

		public class Distance2
		{
			public string text { get; set; }
			public int value { get; set; }
		}

		public class Duration2
		{
			public string text { get; set; }
			public int value { get; set; }
		}

		public class EndLocation2
		{
			public double lat { get; set; }
			public double lng { get; set; }
		}

		public class Polyline
		{
			public string points { get; set; }
		}

		public class StartLocation2
		{
			public double lat { get; set; }
			public double lng { get; set; }
		}

		public class Step
		{
			public Distance2 distance { get; set; }
			public Duration2 duration { get; set; }
			public EndLocation2 end_location { get; set; }
			public string html_instructions { get; set; }
			public Polyline polyline { get; set; }
			public StartLocation2 start_location { get; set; }
			public string travel_mode { get; set; }
			public string maneuver { get; set; }
		}

		public class Leg
		{
			public Distance distance { get; set; }
			public Duration duration { get; set; }
			public string end_address { get; set; }
			public EndLocation end_location { get; set; }
			public string start_address { get; set; }
			public StartLocation start_location { get; set; }
			public List<Step> steps { get; set; }
			public List<object> traffic_speed_entry { get; set; }
			public List<object> via_waypoint { get; set; }
		}

		public class OverviewPolyline
		{
			public string points { get; set; }
		}

		public class Route
		{
			public Bounds bounds { get; set; }
			public string copyrights { get; set; }
			public List<Leg> legs { get; set; }
			public OverviewPolyline overview_polyline { get; set; }
			public string summary { get; set; }
			public List<object> warnings { get; set; }
			public List<object> waypoint_order { get; set; }
		}

		public class RootObject
		{
			public List<GeocodedWaypoint> geocoded_waypoints { get; set; }
			public List<Route> routes { get; set; }
			public string status { get; set; }
		}



	}
}
