using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using BUGPublica.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationContentPage : ContentPage
	{
        System.Collections.ObjectModel.ObservableCollection<ImageContent> _images = 
            new System.Collections.ObjectModel.ObservableCollection<ImageContent>();

		public LocationContentPage (string name, int detailId, int bug)
		{
			InitializeComponent ();

            Title = name.ToUpper();

            carouselImages.ItemsSource = _images;

            //SE DESCARGA EL CONTENIDO DEL LUGAR
            DownloadContent(detailId, bug);
		}

        /// <summary>
        /// CONSULTA AL SERVIDOR POR EL CONTENIDO DEL LUGAR
        /// </summary>
        private async void DownloadContent(int detailId, int bug)
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                response = await client.GetAsync(AppConfig.Url.GetPinContentUri(detailId, bug, lang));
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
                JObject jContent = jObject["content"] as JObject;
                LocationContent locationContent = JsonConvert.DeserializeObject<LocationContent>(jContent.ToString());

                //SE ESTABLECE LA INFORMACION DEL LUGAR
                lblContent.Text = locationContent.Content;

                //SE OBTIENEN LAS IMAGENES
                _images.Add(new ImageContent { Image = ImageSource.FromUri(new Uri(locationContent.Image)) });
                foreach(string url in locationContent.Gallery)
                {
                    _images.Add(new ImageContent { Image = ImageSource.FromUri(new Uri(url)) });
                }
                //carouselImages.Image = ImageSource.FromUri(new Uri(locationContent.Image));
            }
        }


        /// <summary>
        /// MODELO PARA CONTENER LAS IMAGENES DEL LUGAR
        /// </summary>
        public class ImageContent
        {
            public ImageSource Image { set; get; }
        }
    }
}