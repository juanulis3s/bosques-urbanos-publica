using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Resources;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : Rg.Plugins.Popup.Pages.PopupPage
	{
        private string _pattern = @"^([0-9]{10})$";
        private string _patternCountryCode = @"^([0-9]{1,4})$";

        public LoginPage ()
		{
			InitializeComponent ();

            //SE AGREGA EL EVENTO DE CERRAR LA PAGINA AL PULSAR EL BOTON DE CERRADO
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (sender, e) =>
            {
                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            };
            imgClose.GestureRecognizers.Add(tapGesture);

            //SE VERIFICA SI EXISTE UN NUMERO
            if (Application.Current.Properties.ContainsKey(AppConfig.PHONE_KEY))
                txtPhone.Text = (string)Application.Current.Properties[AppConfig.PHONE_KEY];
        }

        public async void LoginClick(object sender, EventArgs e)
        {
            if (IsValidCode() && IsValidPhone())
            {
                Register(txtPhone.Text);
            }
            else
            {
                await DisplayAlert(AppResources.Warning, AppResources.InvalidPhone, AppResources.Accept);
            }
        }

        /// <summary>
        /// VALIDA SI EL NUMERO DE TELEFONO ES CORRECTO
        /// </summary>
        /// <returns></returns>
        private bool IsValidPhone()
        {
            if (txtPhone.Text == null)
                return false;

            return System.Text.RegularExpressions.Regex.Match(txtPhone.Text, _pattern).Success;
        }

        /// <summary>
        /// VALIDA QUE EL CODIGO DE PAIS SEA CORRECTO
        /// </summary>
        /// <returns></returns>
        private bool IsValidCode()
        {
            if (txtPrefix.Text == null)
                return false;

            return System.Text.RegularExpressions.Regex.Match(txtPrefix.Text, _patternCountryCode).Success;
        }

        /// <summary>
        /// ENVIA EL NUMERO DE TELEFONO AL SERVIDOR
        /// </summary>
        /// <param name="phone"></param>
        private async void Register(string phone)
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(AppConfig.Url.GetRegisterUri(phone));
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

                    if ((bool)jObject["saved"])
                    {
                        //SE GUARDA EL NUMERO DE TELEFONO EN EL DICCIONARIO
                        if (Application.Current.Properties.ContainsKey(AppConfig.PHONE_KEY))
                            Application.Current.Properties.Remove(AppConfig.PHONE_KEY);
                        Application.Current.Properties.Add(AppConfig.PHONE_KEY, txtPhone.Text);

                        //MUESTRA DIALOGO DE EXITO
                        await DisplayAlert(AppResources.Success, AppResources.RegisterSuccess, AppResources.OK);

                        //SE CIERRA LA VENTANA
                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {
                        //MUESTRA DIALOGO DE ERROR
                        await DisplayAlert(AppResources.Error, AppResources.SomethingWrong, AppResources.OK);
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    //MUESTRA DIALOGO DE ERROR
                    await DisplayAlert(AppResources.Error, AppResources.SomethingWrong, AppResources.OK);
                }
            }
        }
    }
}