using BUGPublica.Styles;
using Xamarin.Forms;

namespace BUGPublica.Controls
{
    public class Divider : BoxView
    {
        public Divider()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.Start;
            Color = Colors.DividerColor;
            Margin = 0;
            HeightRequest = 1;
        }
    }

    public class DividerVertical : BoxView
    {
        public DividerVertical()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.Start;
            Color = Colors.DividerColor;
            Margin = 0;
            WidthRequest = 1;
        }
    }
}
