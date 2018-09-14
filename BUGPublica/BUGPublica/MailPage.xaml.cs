using System;
using System.Text.RegularExpressions;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Resources;
using BUGPublica.Models;
using Newtonsoft.Json.Linq;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MailPage : ContentPage
	{
        int _bug;

		public MailPage ()
		{
			InitializeComponent ();
            BindingContext = new ViewModels.BUGViewModel();
            pickerBug.SelectedIndexChanged += SelectedIndexChanged;
            pickerBug.SelectedItem = pickerBug.ItemsSource[0];
            //_bug = ((BUG)pickerBug.SelectedItem).Id;
		}

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            _bug = ((BUG)pickerBug.SelectedItem).Id;
        }

        public void OnSendButtonPressed(object sender, EventArgs e)
        {
            //REVISA SI LOS DATOS OIN VÁLIDOS
            if (CheckData())
            {
                //ENVÍA EL MENSAJE
                SendSuggestion();
            }
        }

        /// <summary>
        /// MUESTRA UN MENSAJE DE ERROR CON LA CADENA DADA
        /// </summary>
        /// <param name="msg"></param>
        void ShowErrorDialog(string msg)
        {
            DisplayAlert(BUGPublica.Resources.AppResources.ErrorData, msg, "Ok");
        }

        /// <summary>
        /// REVISA QUE LOS DATOS SEAN CORRECTOS
        /// </summary>
        /// <returns></returns>
        bool CheckData()
        {
            //VALIDA EL NOMBRE
            if (txtName.Text == null || txtName.Text.Length == 0)
            {
                ShowErrorDialog(AppResources.InvalidName);
                return false;
            }

            //VALIDA EL CONTENIDO DEL MENSAJE
            if (txtContent.Text == null || txtContent.Text.Length == 0)
            {
                ShowErrorDialog(AppResources.InvalidMessage);
                return false;
            }

            //VALIDA EL CORREO
            if(txtEmail.Text == null)
            {
                ShowErrorDialog(AppResources.InvalidEmail);
                return false;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(txtEmail.Text);
            if(!match.Success)
            {
                ShowErrorDialog(AppResources.InvalidEmail);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ENVIA LA SUGERENCIA
        /// </summary>
        async void SendSuggestion()
        {
            //CREA EL CLIENTE PARA LA CONEXION
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AppConfig.GetAuth();

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(AppConfig.Url.GetSuggestionMailboxUri(
                    _bug, txtName.Text, txtEmail.Text, txtContent.Text));
            }
            catch (HttpRequestException e)
            {
                ShowErrorDialog(AppResources.ErrorConnection);
                Console.WriteLine(e.StackTrace);
                return;
            }

            //SE VERIFICA QUE HAYA ESTADO CORRECTO
            if (response.IsSuccessStatusCode)
            {
                //SE VERIFICA SI SE GUARDO EL MENSAJE
                var content = await response.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(content);
                bool sent = (bool)jObject.Property("sent");
                //MUESTRA UN MENSAJE DE QUE HA SIDO ENVIADO LA SUGERENCIA
                if (sent)
                {
                    await DisplayAlert(AppResources.Success, AppResources.MailSent, "Ok");
                    await Navigation.PopAsync();
                }
                else
                    await DisplayAlert(AppResources.Error, AppResources.ErrorConnection, "Ok");
            }
            else
            {
                await DisplayAlert(AppResources.Error, AppResources.ErrorConnection, "Ok");
            }
        }
	}
}