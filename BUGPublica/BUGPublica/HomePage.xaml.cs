using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

using BUGPublica.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        private bool _popupShown;

		public HomePage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //SE REVISA SI NO SE HA MOSTRADO UN POP UP
            if (_popupShown)
                return;

            //SE REVISA SI DEBE DE MOSTRAR EL POP UP DE REGISTRO
            if (ShouldShowRegisterPopup())
            {
                _popupShown = true;
                Application.Current.Properties[AppConfig.REGISTER_KEY] = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(800), () =>
                {
                    Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new LoginPage());
                    return false;
                });
            }
            else //MUESTRA POPUP DE ANUNCIO
            {
                _popupShown = true;
                ShowBanner();
            }
        }

        /// <summary>
        /// INDICA SI DEBE DE MOSTRAR LA VENTANA DE REGISTRO
        /// </summary>
        /// <returns></returns>
        private bool ShouldShowRegisterPopup()
        {
            if (!Application.Current.Properties.ContainsKey(AppConfig.REGISTER_KEY))
                return true;

            object showPopup;
            Application.Current.Properties.TryGetValue(AppConfig.REGISTER_KEY, out showPopup);
            return !(bool)showPopup;
        }

        /// <summary>
        /// MUESTRA UNA VENTADA DE ANUNCIO
        /// </summary>
        private async void ShowBanner()
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(AppConfig.Url.GetBannerUri(AppConfig.Url.TYPE_MAIN));
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

                //SE OBTIENE LA INFORMACION
                try
                {
                    JObject jObject = JObject.Parse(content);
                    JArray jArray = (JArray)jObject.GetValue("banners");
                    List<BannerModel> banner = JsonConvert.DeserializeObject<List<BannerModel>>(jArray.ToString());

                    //SE ABRE LA VENTANA DEL BANNER
                    if(banner.Count > 0)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(
                                new BannerPage(banner[0]));
                        });
                    }
                    
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                }
            }
        }

        /// <summary>
        /// ABRE LA PAGINA DE NOTICIAS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnNewsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewsPage());
        }

        /// <summary>
        /// ABRE LA PAGINA DE EVENTOS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnEventsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventsPage());
        }

        /// <summary>
        /// ABRE LA PAGINA DE TERMINOS Y CONDICIONES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnTermsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermsPage());
        }

        /// <summary>
        /// ABRE LA PAGINA DE BUZON DE SUGERENCIAS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnSuggestionMailboxTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MailPage());
        }

        /// <summary>
        /// ABRE LA PAGINA DE PERFIL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnProfileTapped(object sender, EventArgs e)
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new LoginPage());
        }
    }
}