using Android.Content;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using BUGPublica.CustomRenders;
using BUGPublica.Droid.CustomRenders;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using AndroidX.AppCompat.Widget;
using Android.Content.Res;
using Android.Graphics;
using AndroidX.Core.Content;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace BUGPublica.Droid.CustomRenders
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        private bool _setup;

        public CustomTabbedPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var tabLayout = ViewGroup.GetChildAt(1) as Google.Android.Material.Tabs.TabLayout;
                if (tabLayout == null) return;

                ////ELIMINA LA BARRA INDICADORA DE TAB SELECCIONADO
                tabLayout.SetSelectedTabIndicatorHeight(0);

                //CAMBIA LA FUENTE DE LOS TEXTOS
                ViewGroup viewGroup = (ViewGroup)tabLayout.GetChildAt(0);
                for (int i = 0; i < viewGroup.ChildCount; i++)
                {
                    ViewGroup viewGroupChild = (ViewGroup)viewGroup.GetChildAt(i);
                    Typeface fontFace = Typeface.CreateFromAsset(Context.Assets, "Montserrat_Light.ttf");
                    for (int j = 0; j < viewGroupChild.ChildCount; j++)
                    {
                        Android.Views.View view = viewGroupChild.GetChildAt(j);
                        if (view.GetType() == typeof(AppCompatTextView) || view.GetType() == typeof(TextView))
                        {
                            TextView textView = (TextView)view;
                            //textView.TextSize = 14f;
                            textView.SetTypeface(fontFace, TypefaceStyle.Normal);
                        }
                    }
                }

                //CAMBIA EL COLOR DEL TEXTO
                //var color = ContextCompat.GetColorStateList(Context, Resource.Color.text_tab);
                //tabLayout.SetTabTextColors(Android.Graphics.Color.Rgb(83, 83, 83), Android.Graphics.Color.Rgb(255, 255, 255));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            //if (_setup)
            //    return;

            //if (e.PropertyName == "Renderer")
            //{
            //    var tabLayout = (Google.Android.Material.Tabs.TabLayout)ViewGroup.GetChildAt(1);
            //    _setup = true;

            //    //CAMBIA EL COLOR DE LOS ESTADOS DE SELECCIONADO Y DESELECCIONADO DE LOS ICONOS
            //    ColorStateList color;
            //    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            //    {
            //        color = Resources.GetColorStateList(Resource.Color.icon_tab, Context.Theme);
            //    }
            //    else
            //    {
            //        color = Resources.GetColorStateList(Resource.Color.icon_tab);
            //    }

            //    for(int i = 0; i < tabLayout.TabCount; i++)
            //    {
            //        var tab = tabLayout.GetTabAt(i); 
            //        var icon = tab.Icon;
            //        if(icon != null)
            //        {
            //            icon = AndroidX.Core.Graphics.Drawable.DrawableCompat.Wrap(icon);
            //            AndroidX.Core.Graphics.Drawable.DrawableCompat.SetTintList(icon, color);
            //        }
            //    }
            //}
        }
    }
}