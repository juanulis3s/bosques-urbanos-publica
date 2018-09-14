using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QRTutorialPage : ContentPage
	{
		public QRTutorialPage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// REGRESA A LA PAGINA ANTERIOR
        /// </summary>
        public async void BackPage()
        {
            await Navigation.PopAsync();
        }

        public void OnDontShowAgainTapped()
        {
            //GUARDA LA SELECCION DE NO VOLVER A MOSTRAR EL TUTORIAL DE QR
            if (!Application.Current.Properties.ContainsKey(AppConfig.QR_TUTORIAL_KEY))
                Application.Current.Properties.Add(AppConfig.QR_TUTORIAL_KEY, true);

            //SE REGRESA A LA PAGINA ANTERIOR
            BackPage();
        }
    }
}