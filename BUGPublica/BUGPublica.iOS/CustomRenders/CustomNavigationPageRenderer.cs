using BUGPublica.iOS.CustomRenders;
using BUGPublica.CustomRenders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace BUGPublica.iOS.CustomRenders
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                // CAMBIA LA FUENTE DEL NAVBAR
                UIFont font = UIFont.FromName("Montserrat-Light", 20f);
                UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { Font = font});
            }
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            var result = base.OnPushAsync(page, animated);

            // OBTIENE EL ULTIMO VIEW CONTROLLER
            if(ViewControllers.Length > 1)
            {
                UIViewController vc = ViewControllers[ViewControllers.Length - 1];

                // CAMBIA EL ICONO DEL BOTON
                vc.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(
                    UIImage.FromFile("ic_back.png"), UIBarButtonItemStyle.Plain, (sender, e) =>
                    {
                        ((NavigationPage)Element).PopAsync();
                    });
            }

            return result;
        }
    }
}
