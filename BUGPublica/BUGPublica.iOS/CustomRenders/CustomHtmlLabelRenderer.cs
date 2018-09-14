
using Foundation;
using BUGPublica.CustomRenders;
using BUGPublica.iOS.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomHtmlLabel), typeof(CustomHtmlLabelRenderer))]
namespace BUGPublica.iOS.CustomRenders
{
    class CustomHtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var view = e.NewElement as CustomHtmlLabel;
            if (view == null || view.Text == null) return;

            var attr = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;
            attr.StringEncoding = NSStringEncoding.UTF8;
            Control.AttributedText = new NSAttributedString(view.Text, attr, ref nsError);
            Control.TextColor = view.TextColor.ToUIColor();
            Control.Font = UIKit.UIFont.FromName("Montserrat-Light", 14);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = Element as CustomHtmlLabel;
            if (view == null || view.Text == null) return;

            var attr = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;
            attr.StringEncoding = NSStringEncoding.UTF8;
            Control.AttributedText = new NSAttributedString(view.Text, attr, ref nsError);
            Control.TextColor = view.TextColor.ToUIColor();
            Control.Font = UIKit.UIFont.FromName("Montserrat-Light", 14);
        }
    }
}