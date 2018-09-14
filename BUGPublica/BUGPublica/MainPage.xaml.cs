using System;

using Xamarin.Forms;

namespace BUGPublica
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
		{
			InitializeComponent();

            //SE HACE UN ARREGLO CON TODAS LAS IMAGENES DE SPLASH DISPONIBLES
            String[] images = {
                "splash_1.png",
                "splash_2.png",
                "splash_3.png",
                "splash_4.png"
            };
            //SE OBTIENE LA IMAGEN A MOSTRAR
            String image = images[new Random().Next(0, images.Length)];
            //SE ESTABLECE LA IMAGEN DE FONDO
            BackgroundImage = image;

            //INICIA UN TIMER PARA MOSTRAR LA SIGUIENTE PANTALLA DE LOGIN
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                //SE INICIALIZA LA PAGINA DONDE SE VA A NAVEGAR
                Navigation.PushAsync(new HomePage());
                Navigation.RemovePage(this);
                return false;
            });
        }
    }
}
