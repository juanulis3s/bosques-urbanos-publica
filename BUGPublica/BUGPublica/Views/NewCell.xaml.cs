
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCell : ViewCell
	{
		public NewCell ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), typeof(string), typeof(NewCell), "icon.png");

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(NewCell), null);

        public static readonly BindableProperty Publication_DateProperty = BindableProperty.Create(
            nameof(Publication_Date), typeof(string), typeof(NewCell), null);

        public static readonly BindableProperty CategoryProperty = BindableProperty.Create(
            nameof(Category), typeof(string), typeof(NewCell), null);

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

        public string Publication_Date
        {
            set { SetValue(Publication_DateProperty, value); }
            get { return (string)GetValue(Publication_DateProperty); }
        }

        public string Category
        {
            set { SetValue(CategoryProperty, value); }
            get { return (string)GetValue(CategoryProperty); }
        }
    }
}