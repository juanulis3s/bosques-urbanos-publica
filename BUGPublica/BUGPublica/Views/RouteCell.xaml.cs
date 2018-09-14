using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RouteCell : ViewCell
	{
		public RouteCell ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), typeof(string), typeof(RouteCell), "icon.png");

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(RouteCell), null);

        public static readonly BindableProperty DurationProperty = BindableProperty.Create(
            nameof(Duration), typeof(string), typeof(RouteCell), null);

        public string Image
        {
            set { SetValue(ImageProperty, value); }
            get { return (string)GetValue(ImageProperty); }
        }

        public string Title
        {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }

        public string Duration
        {
            set { SetValue(DurationProperty, value); }
            get { return (string)GetValue(DurationProperty); }
        }
    }
}