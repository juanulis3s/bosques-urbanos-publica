using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BUGPublica.Helpers;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoutePage : ContentPage
	{
        private ObservableCollection<RouteItem> _routes = new ObservableCollection<RouteItem>();
        private int _bugId;
        private bool _routesDownloaded = false;

        public RoutePage ()
		{
			InitializeComponent ();

            //SE ASIGNA EL BOSQUE
            _bugId = BugTabbedPage.Bug.Id;

            //SE ASIGNA LA LISTA QUE CONTENDRAN LAS RUTAS AL LISTVIEW
            listRoutes.ItemsSource = _routes;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //SI SE HAN DESCARGADO LAS NOTICIAS, NO SE DESCARGAN MAS
            if (_routesDownloaded)
                return;

            //SE MUESTRA EL INDICADOR
            ToggleIndicator(true);
            //SE DESCARGAN LAS RUTAS
            DownloadRoutes();
        }

        /// <summary>
        /// CONSULTA AL SERVIDOR POR LAS RUTAS
        /// </summary>
        private async void DownloadRoutes()
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                string lang = Plugin.Multilingual.CrossMultilingual.Current.DeviceCultureInfo.TwoLetterISOLanguageName;
                response = await client.GetAsync(AppConfig.Url.GetRoutesUri(_bugId ,lang));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.StackTrace);
                return;
            }

            //SE VERIFICA QUE HAYA ESTADO CORRECTO
            if (response.IsSuccessStatusCode)
            {
                //SE INDICA QUE SE HA DESCARGADO LAS RUTAS
                _routesDownloaded = true;

                //SE OBTIENE EL CONTENIDO
                var content = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(content);
                JArray jArray = (JArray)jObject["routes"];

                //SE OBTIENE LA LISTA DE RUTAS
                List<RouteItem> items = JsonConvert.DeserializeObject<List<RouteItem>>(jArray.ToString());

                //SE AGREGA A LA LISTA PRINCIPAL
                Device.BeginInvokeOnMainThread(() =>
                {
                    AddToListView(items);
                });
            }
        }

        /// <summary>
        /// AGREGA LA LISTA DE RUTAS AL LISTVIEW
        /// </summary>
        private void AddToListView(List<RouteItem> items)
        {
            //SE ESCONDE EL ACTIVITY INDICATOR
            ToggleIndicator(false);

            //SE AGREGA A LA COLECCION
            foreach (RouteItem item in items)
                _routes.Add(item);

            //SI NO ESTA VISIBLE MUESTRA LA LISTA DE NOTICIAS, SE MUESTRA
            if (!listRoutes.IsVisible)
                listRoutes.IsVisible = true;
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
        public async void ItemSelectedRoute(object sender, SelectedItemChangedEventArgs e)
        {
            //SE REVISA SI HAY UN ITEM SELECCIONADO
            if (e.SelectedItem == null)
                return;
            listRoutes.SelectedItem = null;
            await Navigation.PushAsync(new RouteInfoPage(_bugId, (RouteItem)e.SelectedItem));
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTON DE AUXILIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void FabHelpClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpConfirmationPage(BugTabbedPage.Bug.Id));
        }
    }
}