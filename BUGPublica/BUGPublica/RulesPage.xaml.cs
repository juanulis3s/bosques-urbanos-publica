using System.Collections.Generic;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RulesPage : ContentPage
	{
		public RulesPage ()
		{
			InitializeComponent ();

            DownloadRules();
		}

        /// <summary>
        /// CONSULTA AL SERVIDOR POR LAS REGLAS
        /// </summary>
        private async void DownloadRules()
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                response = await client.GetAsync(AppConfig.Url.GetRegulationUri(BugTabbedPage.Bug.Id, lang));
            }
            catch (HttpRequestException e)
            {
                return;
            }

            //SE VERIFICA QUE HAYA ESTADO CORRECTO
            if (response.IsSuccessStatusCode)
            {
                //SE OBTIENE EL CONTENIDO
                var content = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(content);
                JArray jArray = (JArray)jObject["regulations"];

                //SE OBTIENE LA LISTA DE NOTICIAS
                List<Rule> items = JsonConvert.DeserializeObject<List<Rule>>(jArray.ToString());

                Device.BeginInvokeOnMainThread(() =>
                {
                    AddToListView(items);
                });
            }
        }

        /// <summary>
        /// AGREGA LA LISTA DE NOTICIAS AL LISTVIEW
        /// </summary>
        private void AddToListView(List<Rule> items)
        {
            listRules.ItemsSource = items;
            loader.IsRunning = false;
            listRules.IsVisible = true;
        }

        /// <summary>
        /// EVENTO DE SELECCION DE UN ELEMENTO DE LA LISTA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ItemSelectedRules(object sender, SelectedItemChangedEventArgs e)
        {
            //SE REVISA SI HAY UN ITEM SELECCIONADO
            if (e.SelectedItem == null)
                return;
            listRules.SelectedItem = null;
            await Navigation.PushAsync(new RuleInfoPage((Rule)e.SelectedItem));
        }
    }
}