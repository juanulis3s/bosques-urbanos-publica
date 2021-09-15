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
	public partial class NewsPage : ContentPage
	{
        ObservableCollection<NewItem> _news = new ObservableCollection<NewItem>();
        bool _newsDownloaded = false;
        bool _isLoading = false;

        public NewsPage ()
		{
			InitializeComponent ();

            //SE ASIGNA LA LISTA QUE CONTENDRAN LAS NOTICIAS AL LISTVIEW
            listNews.ItemsSource = _news;
            //SE AGREGA UN EVENTO PARA DETERMINAR CUANDO SE LLEGO AL FINAL DE LA LISTA
            listNews.ItemAppearing += ListNews_ItemAppearing;
        }

        /// <summary>
        /// SE EJECUTA CUANDO APARECE UN ELEMENTO EN LA LISTA DE NOTICIAS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListNews_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            //SI YA ESTA CARGANDO NOTICIAS O NO HAY NOTICIAS AUN, SE REGRESA
            if (_isLoading || _news.Count == 0)
                return;

            ////SI LLEGÓ AL FINAL DE LA LISTA
            if (((NewItem)e.Item).Id == _news[_news.Count - 1].Id)
            {
                //CARGA MAS NOTICIAS
                DownloadNews(_news.Count);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //SI SE HAN DESCARGADO LAS NOTICIAS, NO SE DESCARGAN MAS
            if (_newsDownloaded)
                return;

            //SE MUESTRA EL INDICADOR
            ToggleIndicator(true);
            //SE DESCARGAN LAS NOTICIAS
            DownloadNews(0);
        }

        /// <summary>
        /// CONSULTA AL SERVIDOR POR LAS NOTICIAS
        /// </summary>
        private async void DownloadNews(int lasItem)
        {
            _isLoading = true;

            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();
           
            HttpResponseMessage response;
            try
            {
                string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                response = await client.GetAsync(AppConfig.Url.GetNewsUri(lang, lasItem));
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.StackTrace);
                _isLoading = false;
                return;
            }

            //SE VERIFICA QUE HAYA ESTADO CORRECTO
            if (response.IsSuccessStatusCode)
            {
                //SE INDICA QUE SE HA DESCARGADO LAS PRIMERAS NOTICIAS
                _newsDownloaded = true;

                //SE OBTIENE EL CONTENIDO
                var content = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(content);
                JArray jArray = (JArray)jObject["news"];

                //SE OBTIENE LA LISTA DE NOTICIAS
                List<NewItem> items = JsonConvert.DeserializeObject<List<NewItem>>(jArray.ToString());

                //SI YA NO HAY NOTICIAS SE QUITA EL EVENTO DEL LISTADO
                if(items.Count == 0)
                    listNews.ItemAppearing -= ListNews_ItemAppearing;

                Device.BeginInvokeOnMainThread(() =>
                {
                    AddToListView(items);
                });
            }
        }

        /// <summary>
        /// AGREGA LA LISTA DE NOTICIAS AL LISTVIEW
        /// </summary>
        private void AddToListView(List<NewItem> items)
        {
            //SE ESCONDE EL ACTIVITY INDICATOR
            ToggleIndicator(false);

            //SE AGREGA A LA COLECCION
            foreach (NewItem item in items)
                _news.Add(item);

            //SI NO ESTA VISIBLE MUESTRA LA LISTA DE NOTICIAS, SE MUESTRA
            if (!listNews.IsVisible)
                listNews.IsVisible = true;

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
        private async void ItemSelectedNews(object sender, SelectedItemChangedEventArgs e)
        {
            //SE REVISA SI HAY UN ITEM SELECCIONADO
            if (e.SelectedItem == null)
                return;
            listNews.SelectedItem = null;
            await Navigation.PushAsync(new NewInfoPage((NewItem)e.SelectedItem));
        }
    }
}