using System.ComponentModel;
using Android.Content;
using Android.Text;
using Android.Widget;
using BUGPublica.CustomRenders;
using BUGPublica.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomHtmlLabel), typeof(CustomHtmlLabelRenderer))]
namespace BUGPublica.Droid.CustomRenders
{
    public class CustomHtmlLabelRenderer : LabelRenderer
    {
        public CustomHtmlLabelRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var view = e.NewElement as CustomHtmlLabel;
            if (view == null || view.Text == null) return;
            FromHtmlOptions options = FromHtmlOptions.ModeLegacy;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
            {
                Control.SetText(Html.FromHtml(view.Text.ToString(), options), TextView.BufferType.Spannable);
            }
            else
            {
                Control.SetText(Html.FromHtml(view.Text.ToString()), TextView.BufferType.Spannable);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomHtmlLabel.TextProperty.PropertyName)
            {
                var view = Element as CustomHtmlLabel;
                if (view == null || view.Text == null) return;
                FromHtmlOptions options = FromHtmlOptions.ModeLegacy;
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
                {
                    Control.SetText(Html.FromHtml(view.Text.ToString(), options), TextView.BufferType.Spannable);
                }
                else
                {
                    Control.SetText(Html.FromHtml(view.Text.ToString()), TextView.BufferType.Spannable);
                } 
            }
        }
    }
}