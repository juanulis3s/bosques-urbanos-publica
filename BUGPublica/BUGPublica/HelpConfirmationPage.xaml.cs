using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Newtonsoft.Json.Linq;
using BUGPublica.Resources;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HelpConfirmationPage : ContentPage
	{
        private Position _position;
        private string _phone;
        private int _bugId;

		public HelpConfirmationPage (int bugId)
		{
			InitializeComponent ();

            _bugId = bugId;

            //SE OBTIENE EL NUMERO DE TELEFONO
            IDictionary<string, object> properties = Application.Current.Properties;
            if (properties.ContainsKey(AppConfig.PHONE_KEY))
                _phone = properties[AppConfig.PHONE_KEY] as string;
		}

        public async void AcceptClick(object sender, EventArgs e)
        {
            //SE REVISA SI ESTA DENTRO DEL BOSQUE
            _position = await Helpers.LocationHelper.ValidateLocation(this, _bugId);
            if (_position.Latitude == 0 || _position.Longitude == 0) return;

            //SE CREA EL CLIENTE DE LA CONEXIÓN CON LAS CREDENCIALES
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                //SE CONECTA A LA API Y ESPERA POR LA RESPUESTA
                response = await client.GetAsync(AppConfig.Url.GetHelpUri(
                    _bugId, _position.Latitude, _position.Longitude, _phone));
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.StackTrace);
                ShowErrorAlert();
                return;
            }

            //SE REVISA SI ES UN CODIGO CON EXITO
            if (response.IsSuccessStatusCode)
            {
                //SE OBTIENE EL CONTENIDO DE LA RESPUESTA
                var content = await response.Content.ReadAsStringAsync();
                //SE DA FORMATO EN JSON
                JObject jObject = JObject.Parse(content);
                //SE REVISA SI SE GUARDÓ LA INCIDENCIA
                if ((bool)jObject.GetValue("saved"))
                    ShowSuccessAlert();
                else
                    ShowErrorAlert();
            }
            else
            {
                ShowErrorAlert();
            }
        }

        public async void CancelClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        /// <summary>
        /// MUESTRA UN MENSAJE DE ERROR
        /// </summary>
        private async void ShowErrorAlert()
        {
            await DisplayAlert(AppResources.Warning, AppResources.HelpError, AppResources.Accept);
        }

        /// <summary>
        /// MUESTRA UN MENSAJE DE LA INCIDENCIA COMPLETADA
        /// </summary>
        private async void ShowSuccessAlert()
        {
            await DisplayAlert(AppResources.Warning, AppResources.HelpSuccess, AppResources.Accept);
            await Navigation.PopAsync();
        }
    }
} 