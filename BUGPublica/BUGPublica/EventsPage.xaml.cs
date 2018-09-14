using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventsPage : ContentPage
	{
        ObservableCollection<EventItem> _events = new ObservableCollection<EventItem>();
        bool _eventsDownloaded = false;
        bool _isLoading = false;

        public EventsPage ()
		{
			InitializeComponent ();

            //SE ASIGNA LA LISTA QUE CONTENDRAN LAS NOTICIAS AL LISTVIEW
            listEvents.ItemsSource = _events;
            //SE AGREGA UN EVENTO PARA DETERMINAR CUANDO SE LLEGO AL FINAL DE LA LISTA
            listEvents.ItemAppearing += ListEvents_ItemAppearing; ;
        }

        /// <summary>
        /// SE EJECUTA CUANDO APARECE UN ELEMENTO EN LA LISTA DE EVENTOS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListEvents_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            //SI YA ESTA CARGANDO EVENTOS O NO HAY EVENTOS AUN, SE REGRESA
            if (_isLoading || _events.Count == 0)
                return;

            ////SI LLEGÓ AL FINAL DE LA LISTA
            int id = _events[_events.Count - 1].Id;
            if (((EventItem)e.Item).Id == id)
            {
                //CARGA MAS EVENTOS
                DownloadEvents(id); //TODO ES POR ID O POR TAMAÑO?
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //SI SE HAN DESCARGADO LAS NOTICIAS, NO SE DESCARGAN MAS
            if (_eventsDownloaded)
                return;

            //SE MUESTRA EL INDICADOR
            ToggleIndicator(true);
            //SE DESCARGAN LOS EVENTOS
            DownloadEvents(0);
        }

        /// <summary>
        /// CONSULTA AL SERVIDOR POR LOS EVENTOS
        /// </summary>
        private async void DownloadEvents(int lastItem)
        {
            _isLoading = true;

            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                string lang = Plugin.Multilingual.CrossMultilingual.Current.DeviceCultureInfo.TwoLetterISOLanguageName;
                response = await client.GetAsync(AppConfig.Url.GetEventsUri(lang, lastItem));
            }
            catch (HttpRequestException e)
            {
                _isLoading = false;
                Console.WriteLine(e.StackTrace);
                return;
            }

            //SE VERIFICA QUE HAYA ESTADO CORRECTO
            if (response.IsSuccessStatusCode)
            {
                //SE INDICA QUE SE HA DESCARGADO LOS PRIMERAS EVENTOS
                _eventsDownloaded = true;


                //SE OBTIENE EL CONTENIDO
                var content = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(content);
                JArray jArray = (JArray)jObject["events"];

                //SE OBTIENE LA LISTA DE EVENTOS
                List<EventItem> items = JsonConvert.DeserializeObject<List<EventItem>>(jArray.ToString());

                //SI YA NO HAY EVENTOS SE QUITA EL EVENTO DEL LISTADO
                if (items.Count == 0)
                    listEvents.ItemAppearing -= ListEvents_ItemAppearing;

                Device.BeginInvokeOnMainThread(() =>
                {
                    AddToListView(items);
                });
            }
        }

        /// <summary>
        /// AGREGA LA LISTA DE EVENTOS AL LISTVIEW
        /// </summary>
        private void AddToListView(List<EventItem> items)
        {
            //SE ESCONDE EL ACTIVITY INDICATOR
            ToggleIndicator(false);

            //SE AGREGA A LA COLECCION
            foreach (EventItem item in items)
                _events.Add(item);

            //SI NO ESTA VISIBLE MUESTRA LA LISTA DE NOTICIAS, SE MEUSTRA
            if (!listEvents.IsVisible)
                listEvents.IsVisible = true;

            //SE DECLARA QUE TERMINO DE CARGAR NOTICIAS
            _isLoading = false;
        }

        /// <summary>
        /// INICIA O DETIENE EL ACTIVITY INDICATOR
        /// </summary>
        /// <param name="running">SI ESTA CORRIENDO O NO EL INDICADOR</param>
        private void ToggleIndicator(bool running)
        {
            loader.IsRunning = running;
            loader.IsVisible = running;
        }

        /// <summary>
        /// EVENTO DE SELECCION DE UN ELEMENTO DE LA LISTA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ItemSelectedEvents(object sender, SelectedItemChangedEventArgs e)
        {
            ////SE REVISA SI HAY UN ITEM SELECCIONADO
            if (e.SelectedItem == null)
                return;
            listEvents.SelectedItem = null;
            await Navigation.PushAsync(new EventInfoPage((EventItem)e.SelectedItem));
        }
    }
}