using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using BUGPublica.CustomRenders;
using BUGPublica.iOS.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace BUGPublica.iOS.CustomRenders
{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // CAMBIA EL COLOR DEL ICONO SELECCIONADO
            TabBar.SelectedImageTintColor = UIColor.FromRGB(64, 221, 155);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            foreach(var controller in ViewControllers)
            {
                controller.TabBarItem.SetTitleTextAttributes(StandardAttributes, UIControlState.Normal);
                controller.TabBarItem.SetTitleTextAttributes(SelectedAttributes, UIControlState.Selected);
            }
        }

        private static UITextAttributes StandardAttributes { get; } = 
            new UITextAttributes() { TextColor = UIColor.FromRGB(83, 83, 83), Font = UIFont.FromName("Montserrat-Light", 12) };
        
        private static UITextAttributes SelectedAttributes { get; } =
            new UITextAttributes() { TextColor = UIColor.White, Font = UIFont.FromName("Montserrat-Light", 12) };
    }
}