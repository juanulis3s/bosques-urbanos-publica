using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace BUGPublica.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();

            //LIBREARIAS
            Xamarin.FormsMaps.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            CarouselView.FormsPlugin.iOS.CarouselViewRenderer.Init();

            // CAMBIA EL COLOR DEL STATUS BAR
            UIView statusbar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if(statusbar != null)
            {
                statusbar.BackgroundColor = UIColor.Black;
                statusbar.TintColor = UIColor.White;
            }

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
