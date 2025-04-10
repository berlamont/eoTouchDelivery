﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using CoreLocation;
using eoTouchDelivery.Core.Controls;
using eoTouchDelivery.Core.Helpers;
using eoTouchDelivery.iOS.Renderers;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace eoTouchDelivery.iOS.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        UIImage _normalImage = UIImage.FromFile("pushpin.png");
        UIImage _fromImage = UIImage.FromFile("pushpin_origen.png");
        UIImage _toImage = UIImage.FromFile("pushpin_destiny.png");

        List<MKAnnotationView> _tempAnnotations;
        List<CustomPin> _customPins;
        UIView _customPinView;
        bool _isDrawnDone;
        CustomMap _customMap;

        public CustomMapRenderer()
        {
            _tempAnnotations = new List<MKAnnotationView>();
            _customPins = new List<CustomPin>();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var iosMapView = (MKMapView)Control;
            _customMap = (CustomMap)sender;

            if (e.PropertyName.Equals("CustomPins") && !_isDrawnDone)
            {
                _customPins = _customMap.CustomPins.ToList();

                ClearPushPins(iosMapView);

                iosMapView.ZoomEnabled = true;
                iosMapView.GetViewForAnnotation = GetViewForAnnotation;
                iosMapView.DidSelectAnnotationView += OnDidSelectAnnotationView;
                iosMapView.DidDeselectAnnotationView += OnDidDeselectAnnotationView;

                AddPushPins(iosMapView, _customMap.CustomPins);
                PositionMap();

                _isDrawnDone = true;
            }

            if (e.PropertyName.Equals("From"))
            {
                AddFromPushPin(iosMapView, _customMap.From);
            }

            if (e.PropertyName.Equals("To"))
            {
                AddToPushPin(iosMapView, _customMap.To);
            }
        }

        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            var anno = annotation as MKAnnotation;
            var customPin = GetCustomPin(anno);

            if (customPin == null)
            {
                throw new Exception("Custom pin not found!");
            }

            annotationView = mapView.DequeueReusableAnnotation(customPin.Id.ToString());

            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, customPin.Id);
                annotationView.CalloutOffset = new CGPoint(0, 0);

                switch (customPin.Type)
                {
                    case CustomPin.AnnotationType.Normal:
                        annotationView.Image = _normalImage;
                        break;
                    case CustomPin.AnnotationType.From:
                        annotationView.Image = _fromImage;
                        break;
                    case CustomPin.AnnotationType.To:
                        annotationView.Image = _toImage;
                        break;
                }

                ((CustomMKAnnotationView)annotationView).Id = customPin.Id;
            }

            annotationView.CanShowCallout = true;

            return annotationView;
        }

        void AddPushPins(MKMapView mapView, IEnumerable<CustomPin> pins)
        {
            foreach (var formsPin in pins)
            {
                var annotation = new CustomMKAnnotation(new
                        CLLocationCoordinate2D
                        {
                            Latitude = formsPin.Position.Latitude,
                            Longitude = formsPin.Position.Longitude
                        },
                    formsPin.Label);

                mapView.AddAnnotation(annotation);

                _tempAnnotations.Add(GetViewForAnnotation(mapView, annotation));
            }
        }

        void AddFromPushPin(MKMapView mapView, CustomPin from)
        {
            var fromPrevious = _customPins
                .FirstOrDefault(p => p.Type == CustomPin.AnnotationType.From);

            if (fromPrevious != null)
            {
                fromPrevious.Type = CustomPin.AnnotationType.Normal;
            }

            if (from != null)
            {
                var fromPin = _customPins
                    .FirstOrDefault(p => p == from);

                if (fromPin != null)
                {
                    fromPin.Type = CustomPin.AnnotationType.From;
                }
            }

            ClearPushPins(mapView);
            AddPushPins(mapView, _customPins);
        }

        void AddToPushPin(MKMapView mapView, CustomPin to)
        {
            var toPrevious = _customPins
                .FirstOrDefault(p => p.Type == CustomPin.AnnotationType.To);

            if (toPrevious != null)
            {
                toPrevious.Type = CustomPin.AnnotationType.Normal;
            }

            if (to != null)
            {
                var toPin = _customPins
                    .FirstOrDefault(p => p == to);

                if (toPin != null)
                {
                    toPin.Type = CustomPin.AnnotationType.To;
                }
            }

            ClearPushPins(mapView);
            AddPushPins(mapView, _customPins);
        }

        CustomPin GetCustomPin(MKAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);

            foreach (var pin in _customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }

            return null;
        }

        void ClearPushPins(MKMapView mapView)
        {
            mapView.RemoveAnnotations(mapView.Annotations);
        }

        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            _customPinView = new UIView();
            e.View.AddSubview(_customPinView);

            var anotation = customView.Annotation as MKAnnotation;

            var selectedPin = GetCustomPin(anotation);

            if (!_customPins.Any(p => p.Type == CustomPin.AnnotationType.From))
            {
                var fromPin = _customPins.FirstOrDefault((p => p.Id == selectedPin.Id));
                fromPin.Type = CustomPin.AnnotationType.From;
                _customMap.From = fromPin;
                customView.Image = _fromImage;
                _customMap.SelectedPin = fromPin;
            }
            else
            {
                var fromPin = _customPins.FirstOrDefault((p => p.Type == CustomPin.AnnotationType.From));

                if (fromPin != null
                    && !_customPins.Any(p => p.Type == CustomPin.AnnotationType.To)
                    && fromPin.Id == selectedPin.Id)
                {
                    fromPin.Type = CustomPin.AnnotationType.Normal;
                    _customMap.From = null;
                    customView.Image = _normalImage;
                    _customMap.SelectedPin = null;
                }
                else
                {
                    if (_customPins.Any(p => p.Type == CustomPin.AnnotationType.From) &&
                        !_customPins.Any(p => p.Type == CustomPin.AnnotationType.To))
                    {
                        var toPin = _customPins.FirstOrDefault((p => p.Id == selectedPin.Id));
                        toPin.Type = CustomPin.AnnotationType.To;
                        _customMap.To = toPin;
                        customView.Image = _toImage;
                        _customMap.SelectedPin = toPin;
                    }
                    else
                    {
                        var to = _customPins.FirstOrDefault(p => p.Type == CustomPin.AnnotationType.To);

                        if (to != null)
                        {
                            to.Type = CustomPin.AnnotationType.Normal;
                            _customMap.To = null;
                            customView.Image = _normalImage;
                            _customMap.SelectedPin = null;
                        }
                    }
                }
            }
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                _customPinView.RemoveFromSuperview();
                _customPinView.Dispose();
                _customPinView = null;
            }
        }

        void PositionMap()
        {
            var myMap = this.Element as CustomMap;
            var formsPins = myMap.CustomPins;

            if (formsPins == null || formsPins.Count() == 0)
            {
                return;
            }

            var centerPosition = new Position(formsPins.Average(x => x.Position.Latitude), formsPins.Average(x => x.Position.Longitude));

            var minLongitude = formsPins.Min(x => x.Position.Longitude);
            var minLatitude = formsPins.Min(x => x.Position.Latitude);

            var maxLongitude = formsPins.Max(x => x.Position.Longitude);
            var maxLatitude = formsPins.Max(x => x.Position.Latitude);

            var distance = MapHelper.CalculateDistance(minLatitude, minLongitude,
                               maxLatitude, maxLongitude, 'M') / 2;

            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMiles(distance)));
        }
    }
}