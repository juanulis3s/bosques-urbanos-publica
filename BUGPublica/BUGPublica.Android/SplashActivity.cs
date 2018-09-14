using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BUGPublica.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/SplashTheme", NoHistory = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_splash);

            // SE CAMBIA LA FUENTE DEL TEXTVIEW
            FindViewById<TextView>(Resource.Id.tvTitle).Typeface =
                Android.Graphics.Typeface.CreateFromAsset(Assets, "Montserrat_Light.ttf");

            // SE HACE UN ARREGLO CON TODAS LAS IMAGENES DE SPLASH DISPONIBLES
            int[] images = {
                Resource.Drawable.splash_1,
                Resource.Drawable.splash_2,
                Resource.Drawable.splash_3,
                Resource.Drawable.splash_4
            };

            // SE ESTABLECE ALEATORAMIENTE UNA IMAGEN DE FONDO
            if (Window != null)
                Window.SetBackgroundDrawableResource(images[new Random().Next(0, images.Length)]);

            new Handler().PostDelayed(() =>
            {
                // SE ABRE LA ACTIVIDAD PRINCIPAL QUE CARGARÁ LOS COMPONENTES DE XAMARIN
                StartActivity(typeof(MainActivity));
            }, 1000);

        }
    }
}