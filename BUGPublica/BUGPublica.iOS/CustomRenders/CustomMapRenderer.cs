using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UIKit;

using BUGPublica.CustomRenders;
using BUGPublica.iOS.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using MapKit;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using CoreGraphics;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace BUGPublica.iOS.CustomRenders
{
    class CustomMapRenderer : MapRenderer
    {
        //bool _mapDrawn;
        bool _showIndexPin;
        MKMapView _map;
        CustomMap _customMap;
        ObservableCollection<CustomPin> _customPins;
        Dictionary<long, MKPointAnnotation> _annotations = new Dictionary<long, MKPointAnnotation>();
        Dictionary<long, MKAnnotationView> _annotationsViews = new Dictionary<long, MKAnnotationView>();
        Object _locker = new Object();


        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement != null)
            {
                //SE LIBERA LOS RECURSOS DEL MAPA
                var nativeMap = Control as MKMapView;
                if(nativeMap != null)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations);
                    nativeMap.GetViewForAnnotation = null;
                    nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                    nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                    nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                }
            }

            if(e.NewElement != null)
            {
                //SE OBTIENE EL MAPA Y SUS PINES
                _map = Control as MKMapView;
                _customMap = (CustomMap)e.NewElement;
                _customPins = _customMap.CustomPins;
                _showIndexPin = _customMap.ShowIndexPin;
                _customPins.CollectionChanged += OnPinCollectionChanged;

                //EVENTO QUE SE DISPARA CUANDO UN MARCADOR SE HACE VISIBLE EN EL MAPA
                _map.GetViewForAnnotation = GetViewForAnnotation;
                //EVENTO QUE SE DISPARA CUANDO SE PULSA SOBRE EL LADO DERECHO DEL CUADRO DE INFORMACION
                _map.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                //EVENTOS QUE SE DISPARAN CUANDO SE SELECCIONA Y QUITA SELECCION SOBRE UN MARCADOR
                _map.DidSelectAnnotationView += OnDidSelectAnnotationView;
                _map.DidDeselectAnnotationView += OnDidDeselectAnnotationView;

                // SE INDICA QUE SE HA CARGADO EL MAPA
                _customMap.MapLoaded();
            }
        }

        /// <summary>
        /// EVENTO CUANDO SE CAMBIA LA PROPIEDAD DE ALGUN PIN
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void PinOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CustomPin pin = (CustomPin)sender;
            MKPointAnnotation annotation;
            if (!_annotations.TryGetValue(pin.Id, out annotation))
            {
                return;
            }

            if (annotation == null)
            {
                return;
            }

            if (e.PropertyName == Pin.LabelProperty.PropertyName)
            {
                annotation.Title = pin.Label;
            }
            else if (e.PropertyName == Pin.AddressProperty.PropertyName)
            {
                annotation.Subtitle = pin.Address;
            }
            else if (e.PropertyName == Pin.PositionProperty.PropertyName)
            {
                annotation.Coordinate = new CoreLocation.CLLocationCoordinate2D(pin.Position.Latitude, pin.Position.Longitude);
            }
            else if(e.PropertyName == CustomPin.IsVisibleProperty.PropertyName)
            {
                MKAnnotationView annotationView;
                if (_annotationsViews.TryGetValue(pin.Id, out annotationView))
                {
                    annotationView.Hidden = !pin.IsVisible;
                }
            }

        }

        ///// <summary>
        ///// EVENTO CUANDO LA LISTA DE PINES HA SIDO MODIFICADA
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="notifyCollectionChangedEventArgs"></param>
        private void OnPinCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            //System.Diagnostics.Debug.WriteLine(notifyCollectionChangedEventArgs.Action.ToString());
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddPins(notifyCollectionChangedEventArgs.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    //RemovePins(notifyCollectionChangedEventArgs.OldItems);
                    break;
                    //case NotifyCollectionChangedAction.Replace:
                    //    RemovePins(notifyCollectionChangedEventArgs.OldItems);
                    //    AddPins(notifyCollectionChangedEventArgs.NewItems);
                    //    break;
                    //case NotifyCollectionChangedAction.Reset:
                    //    _markers?.ForEach(m => m.Remove());
                    //    _markers = null;
                    //    AddPins((IList)Element.Pins);
                    //    break;
                    //case NotifyCollectionChangedAction.Move:
                    //    //do nothing
                    //    break;
            }
        }

        /// <summary>
        /// AGREGA LOS PINES AL MAPA
        /// </summary>
        /// <param name="newItems"></param>
        private void AddPins(IList newItems)
        {
            if (_map == null) return;

            foreach (CustomPin pin in newItems)
            {
                pin.PropertyChanged += PinOnPropertyChanged;
                var annotation = CreateAnnotation(pin);
                //pin.Id = annotation;
                _map.AddAnnotation(annotation);
                _annotations[pin.Id] = annotation as MKPointAnnotation;
            }
        }

        private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation == null)
                return null;

           if (annotation is MKUserLocation)
                return null;

            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
                return null;

            string id = Convert.ToString(customPin.Id);
            MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(id);
            if(annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, id);
                UIImage image = UIImage.FromFile(customPin.Icon);
                if(_showIndexPin)
                {
                    image = image.Scale(new CGSize(image.Size.Width / 2, image.Size.Height / 2), 0);
                    annotationView.Image = DrawText(image, Convert.ToString(customPin.Index), UIScreen.MainScreen.Scale);
                }
                else
                {
                    annotationView.Image = image.Scale(new CGSize(image.Size.Width / 2, image.Size.Height / 2), 0);
                }
                annotationView.CalloutOffset = new CGPoint(0, 0);
                if(customPin.HasContent)
                    annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);

                try
                {
                    _annotationsViews.Add(customPin.Id, annotationView);
                }
                catch (Exception e) 
                { 
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    //_map.RemoveAnnotation(annotation);
                    //_annotations.Remove(customPin.Id);
                }   
            }
            annotationView.CanShowCallout = true;
            annotationView.Hidden = !customPin.IsVisible;

            return annotationView;

        }

        private void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            CustomPin pin = GetCustomPin(e.View);
            if (pin != null)
                pin.PinInfoClicked();
        }

        private void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// OBTIENE UN CUSTOM PIN EN BASE AL MARCADOR DADO
        /// </summary>
        /// <param name="annotation"></param>
        /// <returns></returns>
        private CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            if (annotation == null) return null;
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

        /// <summary>
        /// OBTIENE UN CUSTOM PIN EN BASE A LA VISTA DEL MARCADOR DADO
        /// </summary>
        /// <param name="annotationView"></param>
        /// <returns></returns>
        private CustomPin GetCustomPin(MKAnnotationView annotationView)
        {
            var annotation = _annotations[Convert.ToInt64(((CustomMKAnnotationView)annotationView).Id)];
            if (annotation == null) 
                return null;
            return GetCustomPin(annotation);
        }

        private UIImage DrawText(UIImage image, string text, nfloat scaleFactor)
        {
            nfloat width = image.Size.Width;
            nfloat radius = width / 2;
            nfloat height = image.Size.Height + radius * 2;
            nfloat fontSize = radius;

            CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();

            using (CGBitmapContext ctx = new CGBitmapContext(
                IntPtr.Zero, (nint)width, (nint)height, 8, 4 * (nint)width, CGColorSpace.CreateDeviceRGB(), CGImageAlphaInfo.PremultipliedFirst))
            {
                // SE DIBUJA LA IMAGEN EN EL CANVAS
                ctx.DrawImage(new CGRect(0, 0, (double)width, (double)image.Size.Height), image.CGImage);

                // DIBUJA EL CIRCULO
                ctx.SetFillColor(UIColor.Black.CGColor);
                ctx.SetStrokeColor(UIColor.Black.CGColor);
                ctx.AddArc(width / 2, height - radius, radius, 0, (nfloat)Math.PI * 2, false);
                ctx.DrawPath(CGPathDrawingMode.Fill);

                // SE ESTABLECE LA FUENTE A USAR
                ctx.SelectFont("Helvetica", fontSize, CGTextEncoding.MacRoman);

                // SE ESTABLECE EL COLOR DEL TEXTO
                ctx.SetFillColor(UIColor.White.CGColor);
                ctx.SetStrokeColor(UIColor.White.CGColor);

                // SE DIBUJA EL TEXTO DE MANERA INVISIBLE PARA OBTENER EL ANCHO DEL TEXTO
                ctx.SetTextDrawingMode(CGTextDrawingMode.Invisible);
                nfloat start, end, textWidth;
                start = ctx.TextPosition.X;
                ctx.ShowText(text);
                end = ctx.TextPosition.X;
                textWidth = end - start;

                // ESTABLECE EL MODO DIBUJO DE VUELTA A VISIBLE
                ctx.SetTextDrawingMode(CGTextDrawingMode.Fill);

                // DIBUJA EL TEXTO EN LAS COORDENADAS DADAS
                nfloat textX = width / 2 - textWidth / 2;
                nfloat textY = height - 3 * radius / 2;
                ctx.ShowTextAtPoint(textX, textY, text);

                return UIImage.FromImage(ctx.ToImage());
            }
        }
    }
}