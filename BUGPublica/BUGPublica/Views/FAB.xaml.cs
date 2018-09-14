using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FAB : ContentView
	{
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), typeof(string), typeof(FAB), null);

        public static readonly BindableProperty RadiusProperty = BindableProperty.Create(
            nameof(Radius), typeof(int), typeof(FAB), 0);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public event EventHandler Click;

        public void FABClick(object sender, EventArgs e)
        {
            Click?.Invoke(this, EventArgs.Empty);
        }


        public FAB ()
		{
			InitializeComponent ();
		}
	}
}