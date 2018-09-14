using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using BUGPublica.Droid.CustomRenders;
using BUGPublica.CustomRenders;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace BUGPublica.Droid.CustomRenders
{
    class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                Typeface fontFace = Typeface.CreateFromAsset(Context.Assets, "Montserrat_Light.ttf");
                Control.SetTypeface(fontFace, TypefaceStyle.Bold);
            }
        }
    }
}