using Android.Content;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using Xamarin.Forms.Platform.Android;
using BUGPublica.Droid.CustomRenders;
using BUGPublica.CustomRenders;
using Android.Util;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CircleButtonRender))]
namespace BUGPublica.Droid.CustomRenders
{
    class CircleButtonRender : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        GradientDrawable _gradientDrawable;

        public CircleButtonRender(Context context) : base(context){}

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var button = (CustomButton)Element;
            if (button == null) return;

            Paint(button);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == CustomButton.CustomBorderColorProperty.PropertyName ||
                e.PropertyName == CustomButton.CustomBorderWidthProperty.PropertyName ||
                e.PropertyName == CustomButton.CustomRadiusProperty.PropertyName ||
                e.PropertyName == Button.BackgroundColorProperty.PropertyName)
            {
                if (Element != null)
                    Paint((CustomButton)Element);
            }
        }

        void Paint(CustomButton button)
        {
            _gradientDrawable = new GradientDrawable();
            _gradientDrawable.SetShape(ShapeType.Rectangle);
            _gradientDrawable.SetColor(button.CustomBackgroundColor.ToAndroid());
            _gradientDrawable.SetStroke(button.CustomBorderWidth, button.CustomBorderColor.ToAndroid());
            _gradientDrawable.SetCornerRadius(DpToPixels(Context, button.CustomRadius));
            Control.SetBackground(_gradientDrawable);
            Control.SetAllCaps(false);
        }

        public static float DpToPixels(Context context, float dp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, metrics);
        }
    }
}