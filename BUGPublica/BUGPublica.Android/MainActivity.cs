using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BUGPublica.Styles;

namespace BUGPublica.Droid
{
    [Activity(Label = "Bosques AMG", Icon = "@drawable/ic_launcher", RoundIcon = "@drawable/ic_launcher_round", 
        Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var x = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
            var y = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            Layouts.DisplayYSizePX = x > y ? x : y;
            Layouts.DisplayXSizePX = x < y ? x : y;
            Layouts.DisplayScale = Resources.DisplayMetrics.Density;

            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            // REVISA EL PERMISO DE CAMARA
            if(CheckSelfPermission(Android.Manifest.Permission.Camera) != Permission.Granted)
                RequestPermissions(new string[] { Android.Manifest.Permission.Camera }, 100);

            //LIBREARIAS
            Xamarin.FormsMaps.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            CarouselView.FormsPlugin.Droid.CarouselViewRenderer.Init();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);  
        }
    }
}

