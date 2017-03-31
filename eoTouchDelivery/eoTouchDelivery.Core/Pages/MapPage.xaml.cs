using System;
using System.Collections.Generic;
using System.Diagnostics;
using eoTouchDelivery.Core.DataServices;
using eoTouchDelivery.Core.Models;
using Xamarin.Forms;
/*using Xamarin.Forms.Maps;

namespace eoTouchDelivery.Core.Pages
{
	public partial class MapPage : ContentPage
	{
		public static int staticcustomPins = 0;
		public static int Count = 0;
		public static List<StoreModel> _storemodel = null;

		public MapPage()
		{
			InitializeComponent();
		var routes=	WebService.GetPolylineRoutes("5810 Averill Avenue SW, Wyoming, MI 49548", "65110 30th Street, Lawton, MI 49065");
		//	App.locator.PositionChanged+= Locator_PositionChanged;
		//	App.locator.StartListeningAsync(1, 2);

			_storemodel = new List<StoreModel>();

			DrawRoute(routes);
		}
		private void AddBarMarkerOnMap(List<StoreModel> _storemodel)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				if (customMap.Pins != null)
					customMap.Pins.Clear();
				CustomPin pin = null;
				List<CustomPin> customPins = new List<CustomPin>();
				try
				{
					for (int i = 0; i < _storemodel.Count; i++)
					{
						pin = new CustomPin
						{
							Pin = new Pin
							{
								Type = PinType.Place,
								Position = new Xamarin.Forms.Maps.Position(_storemodel[i].sourceLat, _storemodel[i].sourceLong),
								Label = "marker.png",
								Address = _storemodel[i].MarkerImage,
							},
							Id = "Xamarin",

						};
						customPins.Add(pin);
					}
					staticcustomPins = 0;
					customMap.CustomPins = new List<CustomPin> { pin };
					foreach (var Pin in customPins)
					{
						customMap.Pins.Add(Pin.Pin);
					}
					//customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(StaticDataModel.Lattitude, StaticDataModel.Longitude), Distance.FromMiles(3)));

				}
				catch (Exception ex)
				{

				}
			});


		}
		private void DrawRoute(List<Plugin.Geolocator.Abstractions.Position> routes)
		{
			try
			{
				for (int i = 0; i < routes.Count; i++)
				{ 
					customMap.RouteCoordinates.Add(new Position(routes[i].Latitude, routes[i].Longitude));
				}


				customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(routes[0].Latitude, routes[0].Longitude), Distance.FromMiles(1.0)));

				StoreModel sm = new StoreModel();

				///Here i am setting static Lat Long to show routes from your current location
				sm.sourceLat = routes[0].Latitude;
				sm.sourceLong = routes[0].Longitude;
				_storemodel.Add(sm);
				AddBarMarkerOnMap(_storemodel);
			}
			catch (Exception ex)
			{

			}

		}
		void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
		{
			Debug.WriteLine("Position changed: " + e.Position.Latitude);
			Debug.WriteLine("Position changed: " + e.Position.Longitude);
			Device.BeginInvokeOnMainThread(async () =>
						{
							StoreModel sm = new StoreModel();

				///Here i am setting static Lat Long to show routes from your current location
				            sm.sourceLat = 37.79752;
							sm.sourceLong = -122.40183;
				///Please uncomment this line when you have the exact destination address for route
							//sm.sourceLat = e.Position.Latitude;
							//sm.sourceLong = e.Position.Longitude;
				            
				           


				            StaticDataModel.Lattitude = e.Position.Latitude;
							StaticDataModel.Longitude = e.Position.Longitude;
							sm.MarkerImage = "marker_red";
							_storemodel.Add(sm);
							//AddBarMarkerOnMap(_storemodel);
						});
		}
	}
}
*/