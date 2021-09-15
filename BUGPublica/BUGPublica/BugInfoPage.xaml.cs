
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using BUGPublica.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using BUGPublica.Helpers;
using System.Diagnostics;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BugInfoPage : ContentPage
	{
		public BugInfoPage ()
		{
			InitializeComponent ();

            //SE ESTABLECE LA FOTO DEPENDIENDO EL BOSQUE
            SetImage();

            //SE DESCARGA LA INFORMACION DEL BOSQUE
            DownloadInfo();
		}

        /// <summary>
        /// AÑADE LA INFORMACION DEL BOSQUE A LA INTERFAZ
        /// </summary>
        /// <param name="info"></param>
        private void BindData(BugInfo info)
        {

            //SE AÑADE LA INFORMACION DE CADA CATEGORIA
            lblNameBug.Text = BugTabbedPage.Bug.Name.ToUpper();
            lblBugInfo.Text = info.Content;
            lblAddress.Text = info.Address;
            lblPhoneValue.Text = info.Phone;
            lblSchedule.Text = info.Schedule;
        }

        private void SetImage()
        {
            string image;
            switch (BugTabbedPage.Bug.Id)
            {
                case BUG.ID_BUG_COLOMOS: image = "BUGPublica.Images.colomos.png"; break;
                case BUG.ID_BUG_AGUA_AZUL: image = "BUGPublica.Images.agua_azul.png"; break;
                case BUG.ID_BUG_ALCALDE: image = "BUGPublica.Images.alcalde.png"; break;
                case BUG.ID_BUG_ARBOLEDAS_SUR: image = "BUGPublica.Images.arboledas_sur.png"; break;
                case BUG.ID_BUG_AVILA_CAMACHO: image = "BUGPublica.Images.avila_camacho.png"; break;
                case BUG.ID_BUG_DEAN: image = "BUGPublica.Images.el_dean.png"; break;
                case BUG.ID_BUG_GONZALEZ_GALLO: image = "BUGPublica.Images.gonzalez_gallo.png"; break;
                case BUG.ID_BUG_MORELOS: image = "BUGPublica.Images.morelos.png"; break;
                case BUG.ID_BUG_NATURAL_HUENTITAN: image = "BUGPublica.Images.natural_huentitan.png"; break;
                case BUG.ID_BUG_MIRADOR_INDEPENDENCIA: image = "BUGPublica.Images.miradorhuentitan.png"; break;
                case BUG.ID_BUG_PUERTA_BARRANCA: image = "BUGPublica.Images.puerta_barranca.png"; break;
                default: image = null; break;
            }
            if(image != null)
                imgBug.Image = ImageSource.FromResource(image);
        }

        /// <summary>
        /// DESCARGA LA INFORMACION DEL BOSQUE
        /// </summary>
        private async void DownloadInfo()
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                response = await client.GetAsync(AppConfig.Url.GetInfoBugUri(BugTabbedPage.Bug.Id, lang));
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
                JObject jInfo = (JObject)jObject.GetValue("info");

                //SE OBTIENE LA INFORMACION
                try
                {
                    BugInfo info = JsonConvert.DeserializeObject<BugInfo>(jInfo.ToString());

                    //SE MUESTRA LA INFORMACION DEL BOSQUE
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        BindData(info);
                    });
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Debug.WriteLine(e.StackTrace);
                }
                
            }
        }

        /// <summary>
        /// EVENTO CLICK DE LA SECCION DE REGULACIONES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnRegulationTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RulesPage());
        }

        /// <summary>
        /// ENVETO CLICK DE LA SECCION DE SUGERENCIAS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnSuggestionTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MailPage());
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