using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BUGPublica.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BannerPage : Rg.Plugins.Popup.Pages.PopupPage
	{
		public BannerPage (BannerModel banner)
		{
			InitializeComponent ();

            if(banner != null && banner.Banner != null && banner.Link != null)
            {
                TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += (sender, e) =>
                {
                    Device.OpenUri(new Uri(banner.Link));
                    Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                };
                imgBanner.Source = banner.Banner;
                imgBanner.GestureRecognizers.Add(tapGesture);

                TapGestureRecognizer CloseTapGesture = new TapGestureRecognizer();
                CloseTapGesture.Tapped += (sender, e) =>
                {
                    Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                };
                imgClose.GestureRecognizers.Add(CloseTapGesture);
            }
		}
	}
}