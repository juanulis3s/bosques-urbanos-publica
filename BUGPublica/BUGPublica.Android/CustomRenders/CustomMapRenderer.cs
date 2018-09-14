using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;
using BUGPublica.CustomRenders;
using BUGPublica.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System;
using System.Collections;
using System.ComponentModel;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace BUGPublica.Droid.CustomRenders
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        bool _mapDrawn;
        bool _showIndexPin;
        GoogleMap _map;
        CustomMap _customMap;
        ObservableCollection<CustomPin> _customPins;
        Dictionary<long, Marker> _markers = new Dictionary<long, Marker>();

        public CustomMapRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                //SE QUITA EL EVENTO DE CLICK EN LA INFORMACION DEL LUGAR
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                //SE OBTIENE EL MAPA Y SUS PINES
                _customMap = (CustomMap)e.NewElement;
                _customPins = _customMap.CustomPins;
                _showIndexPin = _customMap.ShowIndexPin;
                _customPins.CollectionChanged += OnPinCollectionChanged;
                //SE CARGA EL MAPA
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            //SI YA SE DIBUJO EL MAPA, NO HACE NADA
            if (_mapDrawn) return;
            //SE ESTABLECE QUE SE HA DIBUJADO EL MAPA
            _mapDrawn = true;

            //SE ESTABLECE EL EVENTO AL PRESIONAR SOBRE UN CUADRO DE INFORMACION DEL MARCADOR
            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);

            //SE CAMBIA EL ESTILO DEL MAPA
            map.SetMapStyle(MapStyleOptions.LoadRawResourceStyle(Context, Resource.Raw.bug_style));

            //SE CONFIGURA EL LIMITE DONDE SE PUEDE MOVER LA CAMARA
            LatLngBounds bounds = new LatLngBounds(
                new LatLng(_customMap.LimitSouthWest.Latitude, _customMap.LimitSouthWest.Longitude),
                new LatLng(_customMap.LimitNorthEast.Latitude, _customMap.LimitNorthEast.Longitude));
            map.SetLatLngBoundsForCameraTarget(bounds);

            //SE OCULTA LOS BOTONES DE ZOOM Y TOOLBAR
            map.UiSettings.ZoomControlsEnabled = false;
            map.UiSettings.MapToolbarEnabled = false;

            //SE CONFIGURA EL ZOOM DEL MAPA
            map.SetMaxZoomPreference(_customMap.MaxZoom);
            map.SetMinZoomPreference(_customMap.MinZoom);
            
            //SE MUEVE LA CAMARA A LA POSICION DEL BOSQUE
            map.MoveCamera(CameraUpdateFactory.NewLatLng(
                new LatLng(_customMap.Position.Latitude, _customMap.Position.Longitude)));

            //SE GUARDA EL MAPA
            _map = map;

            //SE NOTIFICA QUE SE HA CARGADO EL MAPA
            _customMap.MapLoaded();
        } 

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            //SE CREA UN MARCADOR PARA ANDROID EN BASE AL PIN
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.Visible(((CustomPin)pin).IsVisible);
            Bitmap bitmap = BitmapFactory.DecodeFile(((CustomPin)pin).Icon);
            if (_showIndexPin)
            {
                var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                var customMarker = inflater.Inflate(Resource.Layout.route_marker, null);
                customMarker.Measure(MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                    MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
                ImageView imageView = customMarker.FindViewById<ImageView>(Resource.Id.custom_marker_imageview);
                TextView textView = customMarker.FindViewById<TextView>(Resource.Id.custom_marker_badge);
                imageView.SetImageBitmap(bitmap);
                textView.Text = ((CustomPin)pin).Index.ToString();
                Bitmap customMarkerBitmap = Bitmap.CreateBitmap(
                    customMarker.MeasuredWidth, customMarker.MeasuredHeight, Bitmap.Config.Argb8888);
                Canvas canvas = new Canvas(customMarkerBitmap);
                customMarker.Layout(0, 0, customMarker.MeasuredWidth, customMarker.MeasuredHeight);
                customMarker.Draw(canvas);
                marker.SetIcon(BitmapDescriptorFactory.FromBitmap(customMarkerBitmap));
            }
            else
            {
                //bitmap = Bitmap.CreateScaledBitmap(bitmap, (int)(bitmap.Width * 1.5f), (int)(bitmap.Height * 1.5f), false);
                float density = Context.Resources.DisplayMetrics.Density;
                bitmap = Bitmap.CreateScaledBitmap(bitmap, (int)(bitmap.Width * (density / 2)), (int)(bitmap.Height * (density / 2)), false);
          
                marker.SetIcon(BitmapDescriptorFactory.FromBitmap(bitmap));
            }

            return marker;
        } 

        /// <summary>
        /// EVENTO CUANDO LA LISTA DE PINES HA SIDO MODIFICADA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="notifyCollectionChangedEventArgs"></param>
        private void OnPinCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            //System.Diagnostics.Debug.WriteLine(notifyCollectionChangedEventArgs.Action.ToString());
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddPins(notifyCollectionChangedEventArgs.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemovePins(notifyCollectionChangedEventArgs.OldItems);
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
        /// EVENTO CUANDO LA PROPIEDAD DE UN PIN HA CAMBIADO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PinOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CustomPin pin = (CustomPin)sender;
            Marker marker;
            if(!_markers.TryGetValue(pin.Id, out marker))
            {
                return;
            }

            if (e.PropertyName == Pin.LabelProperty.PropertyName)
            {
                marker.Title = pin.Label;
            }
            else if (e.PropertyName == Pin.AddressProperty.PropertyName)
            {
                marker.Snippet = pin.Address;
            }
            else if (e.PropertyName == Pin.PositionProperty.PropertyName)
            {
                marker.Position = new LatLng(pin.Position.Latitude, pin.Position.Longitude);
            }
            else if (e.PropertyName == CustomPin.IsVisibleProperty.PropertyName)
            {
                marker.Visible = pin.IsVisible;
            }
        }

        /// <summary>
        /// AGREGA LOS PINES AL MAPA
        /// </summary>
        /// <param name="newItems"></param>
        private void AddPins(IList newItems)
        {
            if (_map == null) return;

            foreach(CustomPin pin in newItems)
            {
                var option = CreateMarker(pin);
                var marker = _map.AddMarker(option);

                //pin.Id = marker.Id
                pin.PropertyChanged += PinOnPropertyChanged;

                try
                {
                    _markers.Add(pin.Id, marker);
                }
                catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); }
            }
        }

        /// <summary>
        /// REMUEVE PINES DEL MAPA
        /// </summary>
        /// <param name="oldItems"></param>
        private void RemovePins(IList oldItems)
        {
            foreach(CustomPin pin in oldItems)
            {
                pin.PropertyChanged -= PinOnPropertyChanged;
                Marker marker;
                if(_markers.TryGetValue(pin.Id, out marker))
                {
                    marker.Remove();
                    _markers.Remove(pin.Id);
                }
            }
        }

        /// <summary>
        /// EVENTO AL HACER CLICK SOBRE UN CUADRO DE INFORMACION DE UN MARCADOR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            CustomPin pin = GetCustomPin(e.Marker);
            if(pin != null)
            {
                pin.PinInfoClicked();
            }
        }

        public AView GetInfoContents(Marker marker)
        {
            return null;
        }

        public AView GetInfoWindow(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                var customPin = GetCustomPin(marker);
                if (customPin == null)
                    return null;

                AView view;
                if(customPin.HasContent)
                    view = inflater.Inflate(Resource.Layout.pin_info_content, null);
                else
                    view = inflater.Inflate(Resource.Layout.pin_info, null);

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);

                if (infoTitle != null)
                    infoTitle.Text = marker.Title;

                return view;
            }

            return null;
        }

        /// <summary>
        /// OBTIENE UN CUSTOM PIN EN BASE A UN MARCADOR DE ANDROID
        /// </summary>
        /// <param name="annotation"></param>
        /// <returns></returns>
        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in _customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}