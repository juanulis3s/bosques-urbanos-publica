using BUGPublica.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

using static BUGPublica.AppConfig;

namespace BUGPublica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScannerPage : ContentPage
    {
        private ZXingScannerView _zxScannerView;
        private IDictionary<string, object> _dictionary;
        private bool _tutorialShown = false;

        public bool ShouldRecreateScanner { get; set; }

		public QRScannerPage ()
		{
			InitializeComponent ();
            _dictionary = Application.Current.Properties;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //SE REVISA SI DEBE DE MOSTRAR LA VENTANA DE INFORMACION DEL QR
            if(_dictionary.ContainsKey(QR_TUTORIAL_KEY) 
                && (bool)_dictionary[QR_TUTORIAL_KEY])
            {
                //SE CREA EL ESCANER
                Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
                {
                    CreateScanner();
                    return false;
                });
            }
            else if(_tutorialShown)
            {
                //SE CREA EL ESCANER
                Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
                {
                    CreateScanner();
                    return false;
                });
            }
            else
            {
                //SE MUESTRA EL TUTORIAL
                _tutorialShown = true;
                ShowTutorial();
            }
        }

        protected override void OnDisappearing()
        {
            //SE LIBERA EL ESCANER
            ReleaseScanner();
            base.OnDisappearing();
        }

        /// <summary>
        /// CREA EL ESCANER Y LO MUESTRA EN LA INTERFAZ
        /// </summary>
        void CreateScanner()
        {
            ReleaseScanner();

            ZXing.Mobile.MobileBarcodeScanningOptions options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>()
                {
                    ZXing.BarcodeFormat.QR_CODE, ZXing.BarcodeFormat.DATA_MATRIX
                };

            _zxScannerView = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Options = options,
                IsScanning = true
            };
            _zxScannerView.OnScanResult += OnScanResult;

            AbsoluteLayout.SetLayoutBounds(_zxScannerView, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_zxScannerView, AbsoluteLayoutFlags.All);
            absLayout.Children.Add(_zxScannerView);
            absLayout.RaiseChild(absMasks);
            absLayout.RaiseChild(imgBorderQR);
            absLayout.RaiseChild(fabHelp);
        }

        /// <summary>
        /// LIBERA EL ESCANER Y LO QUITA DE LA INTERFAZ
        /// </summary>
        void ReleaseScanner()
        {
            if (_zxScannerView != null)
            {
                _zxScannerView.IsAnalyzing = false;
                _zxScannerView.IsScanning = false;
                _zxScannerView.IsVisible = false;
                _zxScannerView.OnScanResult -= OnScanResult;
                absLayout.Children.Remove(_zxScannerView);
                _zxScannerView = null;
            }
        }

        /// <summary>
        /// LLAMADA CUANDO SE OBTIENE UN RESULTADO AL ESCANEAR
        /// </summary>
        /// <param name="result"></param>
        void OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _zxScannerView.IsAnalyzing = false;
                Uri uri;
                if (Uri.TryCreate(result.Text, UriKind.Absolute, out uri))
                    Device.OpenUri(uri);
                else
                    _zxScannerView.IsAnalyzing = true;
            });
        }

        /// <summary>
        /// MUESTRA LA PAGINA DEL TUTORIAL DEL LECTOR QR
        /// </summary>
        private async void ShowTutorial()
        {
            await Navigation.PushAsync(new QRTutorialPage());
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