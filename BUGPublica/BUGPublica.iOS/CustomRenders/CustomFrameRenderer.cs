using System;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(FrameRenderer))]
namespace BUGPublica.iOS.CustomRenders
{
    public class CustomFrameRenderer : FrameRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);

                // If the Frame's position or size changes we need to reset the shadow
                /*if (e.PropertyName == "X" || e.PropertyName == "Y" || e.PropertyName == "Width" || e.PropertyName == "Height")
                {
                    SetFrameShadow();
                }*/
                if (e != null)
                    SetFrameShadow();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public override void Draw(CGRect rect)
        {
            try
            {
                base.Draw(rect);

                SetFrameShadow();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void SetFrameShadow()
        {
            if (Element != null && Element.HasShadow)
            {
                if (Superview != null && Superview.Subviews != null)
                {
                    foreach (var uiView in Superview.Subviews)
                    {
                        var name = uiView.ToString();

                        // Find the Xamarin.Forms ShadowView and customise the look and feel
                        if (uiView != this && uiView.Layer.ShadowRadius > 0 && name.Contains("_ShadowView"))
                        {
                            var shadowRadius = 2.5f;
                            uiView.Layer.ShadowRadius = shadowRadius;
                            uiView.Layer.ShadowColor = Color.FromHex("#e3e3e3").ToCGColor();
                            uiView.Layer.ShadowOffset = new CGSize(shadowRadius, shadowRadius);
                            uiView.Layer.ShadowOpacity = 0.8f;
                            uiView.Layer.MasksToBounds = false;
                            uiView.Layer.BorderWidth = 1;
                        }
                    }
                }
                Layer.MasksToBounds = true;
                Layer.BorderColor = Element.BorderColor.ToCGColor();
            }
        }
    }
}
