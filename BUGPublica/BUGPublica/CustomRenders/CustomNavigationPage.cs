using Xamarin.Forms;

namespace BUGPublica.CustomRenders
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page root) : base(root)
        {
            BarBackgroundColor = Color.Black;
            BarTextColor = Color.White;
        }
    }
}
