using Xamarin.Forms;
using BUGPublica.CustomRenders;
using BUGPublica.iOS.CustomRenders;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using UIKit;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace BUGPublica.iOS.CustomRenders
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                UIFont font = UIFont.FromName("Montserrat Light", 12);
                Control.Font = font;
            }
        }
    }
}