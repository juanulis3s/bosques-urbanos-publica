using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BUGPublica.Models;
using BUGPublica.CustomRenders;
using BUGPublica.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using System.Net.Http;

namespace BUGPublica
{
    public class RouteInfoPage : ContentPage
    {
        /// <summary>
        /// DISTANCIA MINIMA ENTRE LA UBICACION DEL USUARIO Y UN MARCADOR
        /// </summary>
        private const double DISTANCE_METERS = 20.0;

        private const double ONE_KM = 1000;

        private Dictionary<string, string> _cache = new Dictionary<string, string>();
        private List<CustomPin> _pins = new List<CustomPin>();
        private RouteItem _route;
        private CustomMap _map;
        private Object _locker = new Object();
        private int _totalMarkers;
        private int _markersDownloaded;
        private int _markersVisited;

        public RouteInfoPage (int bug, RouteItem route)
		{
            //SE ESTABLECE EL TITULO DE LA PAGINA
            Title = route.Title;

            //SE ALMACENA LA RUTA
            _route = route;
            _totalMarkers = route.Markers.Count;

            //SE CREA EL MAPA
            _map = BugMap.GetCustomMap(bug);
            _map.HorizontalOptions = LayoutOptions.FillAndExpand;
            _map.VerticalOptions = LayoutOptions.FillAndExpand;
            _map.ShowIndexPin = true;
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(_map.Position, Distance.FromMeters(1000)));
            _map.MapReady += async (sender, e) =>
            {
                await System.Threading.Tasks.Task.Delay(1000);
                //_map.IsShowingUser = true;
                DownloadIcon(_route.Markers[0]);
            };

            Content = new StackLayout {
				Children = {
					_map
				}
			};
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //EMPIEZA A SOLICITAR UBICACIONES
            StartListeningLocations();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //SE DETIENE LA CONSULTA DE LOCALIZACIONES
            StopListeningLocations();
        }

        /// <summary>
        /// CREA LOS PINES DE LA RUTA
        /// </summary>
        private void CreatePin(RouteMarker marker)
        {
            CustomPin pin = new CustomPin
            {
                Type = PinType.Place,
                Label = marker.Title,
                Position = new Position(marker.Latitude, marker.Longitude),
                Icon = marker.Icon,
                Id = marker.Id,
                HasContent = marker.Detail_Id != 0,
                IsVisible = true,
                Index = _markersDownloaded + 1
            };
            _pins.Add(pin);
            _markersDownloaded++;


            //SI SE HAN CARGADO TODOS LOS PINES, SE MUESTRAN EN EL MAPA
            if (_markersDownloaded >= _totalMarkers)
                ShowPins();
            else
                DownloadIcon(_route.Markers[_markersDownloaded]);
        }

        /// <summary>
        /// INDICA SI LA RUTA ES DE UN ARCHIVO LOCAL
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsPathUri(string s)
        {
            return new Uri(s).IsFile;
        }

        /// <summary>
        /// DESCARGA EL ICONO CONTENIDO EN EL PARAMETRO DADO
        /// </summary>
        /// <param name="marker"></param>
        private void DownloadIcon(RouteMarker marker)
        {
            if (IsPathUri(marker.Icon))
            {
                CreatePin(marker);
            }
            else if (_cache.ContainsKey(marker.Icon))
            {
                //SE OBTIENE LA RUTA DEL ICONO GUARDADO EN CACHE
                string filePath;
                _cache.TryGetValue(marker.Icon, out filePath);
                marker.Icon = filePath;

                //SE UTILIZA UN LOCK PARA QUE SOLO UN HILO A LA VEZ EJECUTE LA FUNCION DE AGREGAR PIN
                lock (_locker)
                {
                    CreatePin(marker);
                }
            } 
            else
            {
                //SE CREA UNA TAREA PARA DESCARGAR EL RECURSO Y GUARDARLO EN LA MEMORIA CACHE
                FFImageLoading.Work.TaskParameter task = FFImageLoading.ImageService.Instance
                    .LoadUrl(marker.Icon)
                    .Success((s, r) =>
                    {
                        if (s.FilePath == null)
                        {
                            DownloadIcon(marker);
                        }
                        else
                        {
                            //SE GUARDA EN EL CACHE
                            if (!_cache.ContainsKey(marker.Icon))
                                _cache.Add(marker.Icon, s.FilePath);

                            //SE OBTIENE LA RUTA DEL ICONO GUARDADO EN CACHE
                            marker.Icon = s.FilePath;

                            //SE UTILIZA UN LOCK PARA QUE SOLO UN HILO A LA VEZ EJECUTE LA FUNCION DE AGREGAR PIN
                            lock (_locker)
                            {
                                CreatePin(marker);
                            }
                        }
                    })
                    .Error((e) =>
                    {
                        //TODO MOSTRAR MENSAJE DE ERROR
                        System.Diagnostics.Debug.WriteLine("Error al descargar icono");
                    });

                //SE EJECUTA LA TAREA
                FFImageLoading.TaskParameterExtensions.DownloadOnly(task);
            }
        }

        /// <summary>
        /// MUESTRA LOS PINES EN EL MAPA
        /// </summary>
        private void ShowPins()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                foreach (CustomPin pin in _pins)
                {
                    _map.CustomPins.Add(pin);
                }
            });
        }

        /// <summary>
        /// INICIA LA CONSULTA PARA OBTENER LOCALIZACIONES
        /// </summary>
        /// <returns></returns>
        private async void StartListeningLocations()
        {
            try
            {
                if (CrossGeolocator.Current.IsListening)
                    return;

                if (!await CrossGeolocator.Current.GetIsGeolocationEnabledAsync())
                    return; //TODO MENSAJE DE ERROR POR NO TENER ACTIVDADO EL GPS

                bool started = await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(10), 0);
                if (started)
                {
                    CrossGeolocator.Current.PositionChanged += PositionChanged;
                    CrossGeolocator.Current.PositionError += PositionError;
                    System.Diagnostics.Debug.WriteLine("Obtencion de localizaciones iniciada");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Error al iniciar obtencion de localizaciones");
                }
                return;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Hubo un error: " + e.Message);
            }
        }

        /// <summary>
        /// DETIENE LA CONSULTA DE LOCALIZACIONES
        /// </summary>
        /// <returns></returns>
        private async void StopListeningLocations()
        {
            if (!CrossGeolocator.Current.IsListening)
                return;

            bool stopped = await CrossGeolocator.Current.StopListeningAsync();
            if(stopped)
            {
                CrossGeolocator.Current.PositionChanged -= PositionChanged;
                CrossGeolocator.Current.PositionError -= PositionError;
            }
            return;
        }

        /// <summary>
        /// EVENTO CUANDO HAY UN ERROR AL OBTENER UNA LOCALIZACION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionError(object sender, Plugin.Geolocator.Abstractions.PositionErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Error);
        }

        /// <summary>
        /// EVENTO CUANDO SE OBTIENE UNA NUEVA LOCALIZACION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            //SE REVISA SI HAYA MARCADORES
            if (_route == null || _route.Markers == null || _route.Markers.Count == 0)
                return;

            //SE REVISA SI ESTA CERCA DE ALGUN MARCADOR
            foreach(RouteMarker marker in _route.Markers)
            {
                //SE REVISA SI YA SE VISITO EL LUGAR
                if (marker.Visited)
                    continue;

                //SE CREA LA POSICION
                Plugin.Geolocator.Abstractions.Position position =
                    new Plugin.Geolocator.Abstractions.Position(marker.Latitude, marker.Longitude);

                //SE OBTIENE LA DISTANCIA EN METROS ENTRE LOS DOS PUNTOS
                double distance = Plugin.Geolocator.Abstractions.GeolocatorUtils.CalculateDistance(e.Position, position, 
                    Plugin.Geolocator.Abstractions.GeolocatorUtils.DistanceUnits.Kilometers) * ONE_KM;

                System.Diagnostics.Debug.WriteLine("Distancia al marcador {0}: {1} metros", marker.Title, distance);

                //SE VERIFICA SI ESTA CERCA DEL MARCADOR O NO, PARA ESO SE CONVIERTE A METROS LA DISTANCIA
                if (distance < DISTANCE_METERS)
                {
                    System.Diagnostics.Debug.WriteLine("Cerca del marcador " + marker.Title);

                    //SE ESTABLECE COMO LUGAR VISITADO
                    marker.Visited = true;
                    _markersVisited++;

                    //SE OBTIENEN LAS CADENAS PARA EL DIALOGO
                    string title = BUGPublica.Resources.AppResources.RouteDialogTitle;
                    string msg = BUGPublica.Resources.AppResources.RouteDialogMessage + marker.Title;
                    string accept = BUGPublica.Resources.AppResources.OK;

                    //SE REVISA SI YA SE VISITARON TODOS LOS LUGARES
                    if (_markersVisited >= _totalMarkers)
                    {
                        msg = BUGPublica.Resources.AppResources.RouteDialogEndRoute;
                        SendEndRoute();
                    }
                    //SE MUESTRA EL DIALOGO
                    DisplayAlert(title, msg, accept);
                }
            }
        }

        /// <summary>
        /// ENVIA LA NOTIFICACION DE TERMINO DE LA RUTA
        /// </summary>
        private async void SendEndRoute()
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(AppConfig.Url.GetEndRouteUri(_route.Id, "3333333333"));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.StackTrace);
                return;
            }

            //SE VERIFICA QUE HAYA ESTADO CORRECTO
            if (response.IsSuccessStatusCode)
            {
                //TODO ¿NOTIFICAR SI SE ENVIO O NO LA RUTA?
            }
        }
    }
}