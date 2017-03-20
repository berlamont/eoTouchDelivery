using eoTouchDelivery.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using System.ComponentModel;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using eoTouchDelivery.Core.Helpers;
using System.Linq;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace eoTouchDelivery.Droid.Renderers
{
    class CustomMapRenderer : MapRenderer
    {
        const int NormalResource = Resource.Drawable.pushpin;
        const int FromResource = Resource.Drawable.pushpin_origin;
        const int ToResource = Resource.Drawable.pushpin_destiny;

        BitmapDescriptor _pinIcon;
        BitmapDescriptor _fromPinIcon;
        BitmapDescriptor _toPinIcon;
        List<CustomMarkerOptions> _tempMarkers;
        bool _isDrawnDone;

        public CustomMapRenderer()
        {
            _tempMarkers = new List<CustomMarkerOptions>();
            _pinIcon = BitmapDescriptorFactory.FromResource(NormalResource);
            _fromPinIcon = BitmapDescriptorFactory.FromResource(FromResource);
            _toPinIcon = BitmapDescriptorFactory.FromResource(ToResource);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var androidMapView = (MapView)Control;
            var formsMap = (CustomMap)sender;

            if (e.PropertyName.Equals("CustomPins") && !_isDrawnDone)
            {
                ClearPushPins(androidMapView);

                androidMapView.Map.MarkerClick += HandleMarkerClick;
                androidMapView.Map.MyLocationEnabled = formsMap.IsShowingUser;

                AddPushPins(androidMapView, formsMap.CustomPins);

                PositionMap();

                _isDrawnDone = true;
            }

            if (e.PropertyName.Equals("From"))
            {
                AddFromPushPin(androidMapView, formsMap.From);
            }

            if (e.PropertyName.Equals("To"))
            {
                AddToPushPin(androidMapView, formsMap.To);
            }
        }

        void HandleMarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            var marker = e.Marker;
            marker.ShowInfoWindow();

            var myMap = Element as CustomMap;

            var tempMarker = _tempMarkers
                .FirstOrDefault(m => m.MarkerOptions.Position.Latitude
                                     == marker.Position.Latitude &&
                                     m.MarkerOptions.Position.Longitude == marker.Position.Longitude);

            if (tempMarker == null)
                return;
            var formsPin = new CustomPin
            {
                Id = tempMarker.Id,
                Label = marker.Title,
                Address = marker.Snippet,
                Position = new Position(marker.Position.Latitude, marker.Position.Longitude)
            };

            if (myMap == null)
                return;
            myMap.SelectedPin = formsPin;

            if (_tempMarkers.All(p => p.MarkerOptions.Icon != _fromPinIcon))
            {
                formsPin.Type = CustomPin.AnnotationType.From;
                myMap.From = formsPin;
            }
            else
            {
                var from = _tempMarkers.FirstOrDefault(p => p.MarkerOptions.Icon == _fromPinIcon);

                if (@from != null
                    && _tempMarkers.All(p => p.MarkerOptions.Icon != _toPinIcon)
                    && @from.Id == myMap.SelectedPin.Id)
                {
                    myMap.From = null;
                }
                else
                {
                    if (_tempMarkers.Any(p => p.MarkerOptions.Icon == _fromPinIcon) &&
                        _tempMarkers.All(p => p.MarkerOptions.Icon != _toPinIcon))
                    {
                        formsPin.Type = CustomPin.AnnotationType.To;
                        myMap.To = formsPin;
                    }
                    else
                    {
                        var to = _tempMarkers.FirstOrDefault(p => p.MarkerOptions.Icon == _toPinIcon);

                        if (to != null)
                        {
                            myMap.To = null;
                        }
                    }
                }

                myMap.SelectedPin = null;

                if (formsPin.Type != CustomPin.AnnotationType.Normal)
                    myMap.SelectedPin = formsPin;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (changed)
            {
                _isDrawnDone = false;
            }
        }

        static void ClearPushPins(MapView mapView) => mapView.Map.Clear();

        void AddPushPins(MapView mapView, IEnumerable<CustomPin> pins)
        {
            foreach (var formsPin in pins)
            {
                var markerWithIcon = new MarkerOptions();

                markerWithIcon.SetPosition(new LatLng(formsPin.Position.Latitude, formsPin.Position.Longitude));
                markerWithIcon.SetTitle(formsPin.Label);
                markerWithIcon.SetSnippet(formsPin.Address);

                markerWithIcon.SetIcon(!string.IsNullOrEmpty(formsPin.PinIcon) ? _pinIcon : BitmapDescriptorFactory.DefaultMarker());

                mapView.Map.AddMarker(markerWithIcon);

                _tempMarkers.Add(new CustomMarkerOptions
                {
                    Id = formsPin.Id,
                    MarkerOptions = markerWithIcon
                });
            }
        }

        static void AddPushPins(MapView mapView, IEnumerable<CustomMarkerOptions> markers)
        {
            foreach (var marker in markers)
            {
                mapView.Map.AddMarker(marker.MarkerOptions);
            }
        }

        void AddFromPushPin(MapView mapView, CustomPin from)
        {
            // Reset previous From pushpin
            var fromTempMarker = _tempMarkers
                .FirstOrDefault(p => p.MarkerOptions.Icon == _fromPinIcon);

            fromTempMarker?.MarkerOptions.SetIcon(_pinIcon);

            // Set new From pushpin
            if (from != null)
            {
                from.Type = CustomPin.AnnotationType.From;

                var newFromTempMarker = _tempMarkers
                    .FirstOrDefault(p => p.Id == from.Id);

                newFromTempMarker?.MarkerOptions.SetIcon(_fromPinIcon);
            }

            ClearPushPins(mapView);
            AddPushPins(mapView, _tempMarkers);
        }

        void AddToPushPin(MapView mapView, CustomPin to)
        {
            // Reset previous To pushpin
            var toTempMarker = _tempMarkers
                .FirstOrDefault(p => p.MarkerOptions.Icon == _toPinIcon);

            toTempMarker?.MarkerOptions.SetIcon(_pinIcon);

            // Set new To pushpin
            if (to != null)
            {
                to.Type = CustomPin.AnnotationType.To;

                var newToTempMarker = _tempMarkers
                    .FirstOrDefault(p => p.Id == to.Id);

                newToTempMarker?.MarkerOptions.SetIcon(_toPinIcon);
            }

            ClearPushPins(mapView);
            AddPushPins(mapView, _tempMarkers);
        }

        void PositionMap()
        {
            var myMap = this.Element as CustomMap;
            if (myMap == null)
                return;
            var formsPins = myMap.CustomPins;

            var customPins = formsPins as CustomPin[] ?? formsPins.ToArray();
            if (!customPins.Any())
            {
                return;
            }

            var centerPosition = new Position(customPins.Average(x => x.Position.Latitude), customPins.Average(x => x.Position.Longitude));

            var minLongitude = customPins.Min(x => x.Position.Longitude);
            var minLatitude = customPins.Min(x => x.Position.Latitude);

            var maxLongitude = customPins.Max(x => x.Position.Longitude);
            var maxLatitude = customPins.Max(x => x.Position.Latitude);

            var distance = MapHelper.CalculateDistance(minLatitude, minLongitude,
                               maxLatitude, maxLongitude, 'M') / 2;

            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMiles(distance)));
        }
    }
}