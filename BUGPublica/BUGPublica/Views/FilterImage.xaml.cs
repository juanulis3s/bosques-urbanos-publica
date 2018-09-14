using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterImage : ContentView
	{
		public FilterImage ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), typeof(ImageSource), typeof(FilterImage), null);

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly BindableProperty FilterProperty = BindableProperty.Create(
            nameof(Filter), typeof(ImageSource), typeof(FilterImage), null);

        public ImageSource Filter
        {
            get { return (ImageSource)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }
    }
}