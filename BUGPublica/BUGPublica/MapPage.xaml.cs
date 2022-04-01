using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using BUGPublica.CustomRenders;
using BUGPublica.Models;
using BUGPublica.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BUGPublica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        static int PLACE_ID = 100;

        private CustomMap _map;
        private List<CustomPin> _pins = new List<CustomPin>();
        private List<MarkerPlace> _markerPlaces = new List<MarkerPlace>();
        private int _placesCount = 0;
        private int _placesDownloaded = 0;
        private bool _optionsFilterAdded = false;
        private bool _pinsAdded = false;
        private Object _locker = new Object();

        public MapPage()
        {
            InitializeComponent();

            //SE CREA EL MAPA
            _map = BugMap.GetCustomMap(BugTabbedPage.Bug.Id);
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(_map.Position, Distance.FromMeters(1000)));

            //SE AGREGA EL EVENTO DE OBTENER LOS MARCADORES CUANDO SE HAYA CARGADO EL MAPA
            //_map.MapReady += (sender, e) =>
            //{
            //    GetMarkers();
            //};

            //SE CREA EL EVENTO CLICK DEL BOTON FILTRAR
            btnFilter.Clicked += (sender, e) =>
            {
                ////MUESTRA LA VENTANA DE FILTRO
                filterView.IsVisible = true;

                //SE OCULTA EL BOTON DE FILTRO Y LA BARRA BUSCADORA
                btnFilter.IsVisible = false;
                frameSearch.IsVisible = false;
                fabHelp.IsVisible = false;

                ////AGREGA LAS OPCIONES A LA VENTANA DE FILTRO
                if (!_optionsFilterAdded)
                {
                    _optionsFilterAdded = true;
                    filterView.AddOptions(_markerPlaces);
                }
            };                    

            //SE CREA EL EVENTO DE FILTRADO
            filterView.Filtered += (sender, e) =>
            {
                //SE RECORRE LAS OPCIONES
                foreach (int key in e.Options.Keys)
                {
                    bool visible = e.Options[key];
                    foreach (CustomPin pin in _pins)
                    {
                        if (pin.PlaceId == key)
                        {
                            pin.IsVisible = visible;
                        }
                    }
                }

                //SE MEUSTRA EL BOTON DE FILTRO Y LA BARRA BUSCADORA
                btnFilter.IsVisible = true;
                frameSearch.IsVisible = true;
                fabHelp.IsVisible = true;
            };

            //EVENTO CUANDO CAMBIA EL TEXTO DE LA BARRA BUSCADORA
            txtSearch.TextChanged += (sender, e) => 
            {
                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    FilterIcons(e.NewTextValue);
                }
                else
                {
                    foreach (CustomPin pin in _pins)
                    {
                        pin.IsVisible = true;
                    }
                }
            };

            //SE AGREGA EL MAPA A LA INTERFAZ
            stack.Children.Add(_map);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_pinsAdded)
                return;
            
            GetMarkers();
        }

        /// <summary>
        /// DESCARGA EL ICONO CONTENIDO EN EL PARAMETRO DADO
        /// </summary>
        /// <param name="place">LUGAR CON LA RUTA DEL ICONO</param>
        private void DownloadIcon(MarkerPlace place)
        {
            //SE CREA UNA TAREA PARA DESCARGAR EL RECURSO Y GUARDARLO EN LA MEMORIA CACHE
            FFImageLoading.Work.TaskParameter task = FFImageLoading.ImageService.Instance
                .LoadUrl(place.Icon)
                .Success((s, r) =>
                {
                    if (s.FilePath == null)
                    {
                        DownloadIcon(place);
                    }
                    else
                    {
                        //SE OBTIENE LA RUTA DEL ICONO GUARDADO EN CACHE
                        place.Icon = s.FilePath;

                        //SE UTILIZA UN LOCK PARA QUE SOLO UN HILO A LA VEZ EJECUTE LA FUNCION DE AGREGAR PIN
                        lock (_locker)
                        {
                            AddPins(place);
                        }

                        //SI SE HAN CARGADO TODOS LOS PINES, SE MUESTRAN EN EL MAPA
                        if (_placesDownloaded >= _placesCount)
                            ShowPins();
                    }
                })
                .Error((e) =>
                {
                    _placesDownloaded++;
                    //SI SE HAN CARGADO TODOS LOS PINES, SE MUESTRAN EN EL MAPA
                    if (_placesDownloaded >= _placesCount)
                        ShowPins();
                });

            //SE EJECUTA LA TAREA
            FFImageLoading.TaskParameterExtensions.DownloadOnly(task);
        }

        /// <summary>
        /// AGREGA LOS MARCADORES A LA LISTA DE PINES
        /// </summary>
        /// <param name="place"></param>
        public void AddPins(MarkerPlace place)
        {
            //SE RECORRE LOS MARCADORES DEL LUGAR
            foreach(Marker marker in place.Markers)
            {
                //SE CREA UN PIN PARA EL MARCADOR
                var pin = new CustomPin()
                {
                    Type = PinType.Place,
                    Label = place.Name,
                    Position = new Position(marker.Latitude, marker.Longitude),
                    Icon = place.Icon,
                    Id = marker.Id,
                    PlaceId = place.Id,
                    HasContent = marker.Detail_Id != 0,
                    IsVisible = true
                };

                //SE AÑADE EL EVENTO AL HACER CLICK EN INFO DEL PIN
                pin.OnPinInfoClicked += OnPinInfoClicked;

                //SE AGREGA A LA LISTA DE _pins
                _pins.Add(pin);
            }

            _placesDownloaded++;
        }

        /// <summary>
        /// CONSULTA AL SERVIDOR POR LOS MARCADORES DE LOS LUGARES DEL BOSQUE
        /// </summary>
        private async void GetMarkers()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                response = await client.GetAsync(AppConfig.Url.GetPinsUri(BugTabbedPage.Bug.Id, lang: lang));
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.StackTrace);
                return;
            }
            
            if (response.IsSuccessStatusCode)
            {
                //SE OBTIENE LA RESPUESTA DEL SERVIDOR
                var content = await response.Content.ReadAsStringAsync();
                //SE DESERAILIZA AL LISTADO DE MARCADORES
                JObject jObject = JObject.Parse(content);
                JArray jArray = jObject["locations"] as JArray;
                List<MarkerPlace> places = JsonConvert.DeserializeObject<List<MarkerPlace>>(jArray.ToString());

                //SE AGREGA A LA LISTA
                _markerPlaces = places;
                _placesCount = _markerPlaces.Count;

                // SE DESCARGA LOS ICONOS
                foreach (MarkerPlace place in _markerPlaces)
                {
                    place.Id = PLACE_ID++;
                    DownloadIcon(place);
                }
                    
            }
            else
            {
                Console.WriteLine("Hubo un error en la peticion");
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
                        //System.Diagnostics.Debug.WriteLine("ID: " + pin.Id);
                        _map.CustomPins.Add(pin);
                    }

                    //ACTIVA EL BOTON DE FILTRO
                    btnFilter.IsEnabled = true;

                    //SE INDICA QUE SE HA CARGADO LOS PINES
                    _pinsAdded = true;
                });
        }

        /// <summary>
        /// OBTIENE LOS ICONOS CON SIMILITUD EN LOS CARACTERES DEL VALOR DADO
        /// </summary>
        /// <param name="value"></param>
        private void FilterIcons(string value)
        {
            foreach(CustomPin pin in _pins)
            {
                if (!pin.Label.ToLower().Contains(value.ToLower()))
                    pin.IsVisible = false;
                else
                    pin.IsVisible = true;
            }
        }

        /// <summary>
        /// SE LLAMA CUANDO SE DIO CLICK EN EL CUADRO DE INFORMACION DE UN PIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPinInfoClicked(object sender, EventArgs e)
        {
            //SE OBTIENE EL MODELO DEL LUGAR CORRESPONDIENTE AL PIN
            CustomPin pin = (CustomPin)sender;

            //SE VERIFICA SI ES UN PIN CON CONTENIDO
            if (!pin.HasContent)
                return;

            //SE BUSCA EL LUGAR DEL MARCADOR
            foreach (MarkerPlace place in _markerPlaces)
            {
                if(place.Id == pin.PlaceId)
                {
                    //SE ABRE LA PAGINA DEL CONTENIDO
                    await Navigation.PushAsync(new LocationContentPage(
                        place.Name, GetDetailId(place, pin.Id), BugTabbedPage.Bug.Id));
                    break;
                }
            }
        }

        /// <summary>
        /// OBTIENE EL DETAIL ID DE UN PIN
        /// </summary>
        /// <param name="place"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private int GetDetailId(MarkerPlace place, long id)
        {
            foreach(Marker marker in place.Markers)
            {
                if (marker.Id == id)
                    return marker.Detail_Id;
            }
            return 0;
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTON DE AUXILIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void FabHelpClick(object sender, EventArgs e)
        {
            //LocationHelper.ValidateLocation(this, BugTabbedPage.Bug.Id);
            await Navigation.PushAsync(new HelpConfirmationPage(BugTabbedPage.Bug.Id));
        }
    }

   
}