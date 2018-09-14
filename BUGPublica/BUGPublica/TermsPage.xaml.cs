using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TermsPage : ContentPage
	{
		public TermsPage ()
		{
			InitializeComponent ();

            //SE DESCARGA LOS TERMINOS Y CONDICIONES
            DownloadTerms();
		}

        /// <summary>
        /// DESCARGA TERMINOS Y CONDICIONES DE LA APP
        /// </summary>
        private async void DownloadTerms()
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                string lang = Plugin.Multilingual.CrossMultilingual.Current.DeviceCultureInfo.TwoLetterISOLanguageName;
                response = await client.GetAsync(AppConfig.Url.GetTermsUri(lang));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.StackTrace);
                return;
            }

            //SE VERIFICA QUE HAYA ESTADO CORRECTO
            if (response.IsSuccessStatusCode)
            {
                //SE OBTIENE EL CONTENIDO
                var content = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(content);
                JObject jInfo = (JObject)jObject.GetValue("info");

                //SE OBTIENE LA INFORMACION
                string terms = (string)jInfo.Property("content").Value;

                //SE MUESTRA LA INFORMACION DEL BOSQUE
                Device.BeginInvokeOnMainThread(() =>
                {
                    scroll.Content = new CustomRenders.CustomHtmlLabel
                    { Text = terms, TextColor = Color.White};
                });
            }
        }
    }   
}