using Xamarin.Forms;
using BUGPublica.CustomRenders;

namespace BUGPublica
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //SE CREAN LOS ESTILOS PARA LAS VISTAS PERSONALIZADAS
            CreateCustomStyles();

            //ESTABLECE LA CULTURA
            BUGPublica.Resources.AppResources.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;

            //SE ESTABÑECE EÑ NAVIGATION PAGE Y SE CAMBIA EL COLOR PARA QUE SEA IGUAL QUE EL SPLASH
            MainPage = new CustomNavigationPage(new HomePage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
            // Handle when your app sleeps
		}

		protected override void OnResume ()
		{
            // Handle when your app resumes          
        }

        private void CreateCustomStyles()
        {
            //SE OBTIENE EL COLOR OBSCURO DE LA APP
            var darkColor = Resources["GreenColor"];
            var mainColor = Resources["MainColor"];
            var font = Resources["FontApp"];

            //SE CREA EL ESTILO PARA LOS BOTONES DE LA APP
            var buttonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property=Button.BackgroundColorProperty, Value=darkColor},
                    new Setter {Property=Button.BorderColorProperty, Value=darkColor},
                    new Setter {Property=Button.BorderWidthProperty, Value=0},
                    new Setter {Property=Button.CornerRadiusProperty, Value=30},
                    new Setter {Property=Button.TextColorProperty, Value=Styles.Colors.TextDarkPrimary},
                    new Setter {Property=Button.HeightRequestProperty, Value=60},
                    new Setter {Property=Button.FontFamilyProperty, Value=font}
                },
            };

            Resources.Add("buttonStyle", buttonStyle);

        }

	}
}
