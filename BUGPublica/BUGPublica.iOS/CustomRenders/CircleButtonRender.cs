using Xamarin.Forms;
using BUGPublica.CustomRenders;
using BUGPublica.iOS.CustomRenders;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CircleButtonRender))]
namespace BUGPublica.iOS.CustomRenders
{
    class CircleButtonRender : ButtonRenderer
    {
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

            if (e.PropertyName == CustomButton.CustomBorderColorProperty.PropertyName ||
                e.PropertyName == CustomButton.CustomBorderWidthProperty.PropertyName ||
                e.PropertyName == CustomButton.CustomRadiusProperty.PropertyName)
            {
                if (Element != null)
                    Paint((CustomButton)Element);
            }
        }

        void Paint(CustomButton button)
        {
            Layer.CornerRadius = (float)button.CustomRadius;
            Layer.BorderColor = button.CustomBorderColor.ToCGColor();
            Layer.BorderWidth = (float)button.CustomBorderWidth;
            Layer.BackgroundColor = button.CustomBackgroundColor.ToCGColor();
        }
    }
}