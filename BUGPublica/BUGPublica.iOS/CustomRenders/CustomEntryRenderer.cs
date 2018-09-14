using UIKit;

using BUGPublica.CustomRenders;
using BUGPublica.iOS.CustomRenders;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.ComponentModel;
using System;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace BUGPublica.iOS.CustomRenders
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Draw(((CustomEntry)e.NewElement).BackgroundStyle);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomEntry.BackgroundStyleProperty.PropertyName)
            {
                Draw(((CustomEntry)sender).BackgroundStyle);
            }
        }

        private void Draw(EntryStyle entryStyle)
        {
            switch (entryStyle)
            {
                case EntryStyle.NONE:
                    Control.BorderStyle = UITextBorderStyle.None;
                    break;
                case EntryStyle.ROUNDRECT:
                    Control.BorderStyle = UITextBorderStyle.RoundedRect;
                    break;
                case EntryStyle.LINE:
                    Control.BorderStyle = UITextBorderStyle.None;
                    var border = new CoreAnimation.CALayer();
                    nfloat width = 2;
                    border.BorderColor = UIColor.White.CGColor;
                    border.Frame = new CoreGraphics.CGRect(0, Control.Frame.Size.Height - width, Control.Frame.Size.Width, width);
                    border.BorderWidth = width;
                    //Control.Layer.AddSublayer(border);
                    if (Control.Layer.Sublayers != null)
                    {
                        if (Control.Layer.Sublayers.Length > 0)
                        {
                            Control.Layer.InsertSublayer(border, Control.Layer.Sublayers.Length - 2);
                        }
                        else
                        {
                            Control.Layer.AddSublayer(border);
                        }

                    }
                    else
                    {
                        Control.Layer.AddSublayer(border);
                    }
                    Control.Layer.MasksToBounds = true;
                    break;
            }
        }
    }
}