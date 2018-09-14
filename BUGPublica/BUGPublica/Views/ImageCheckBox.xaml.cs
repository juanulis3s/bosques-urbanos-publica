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
	public partial class ImageCheckBox : ContentView
	{

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text), typeof(string), typeof(ImageCheckBox), null,
            propertyChanged: (bindable, oldValue, newValue) => {
                ((ImageCheckBox)bindable).textLabel.Text = (string)newValue;
            });

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize), typeof(double), typeof(ImageCheckBox), Device.GetNamedSize(NamedSize.Default, typeof(Label)),
            propertyChanged: (bindable, oldValue, newValue) => {
                ImageCheckBox checkbox = (ImageCheckBox)bindable;

                checkbox.textLabel.FontSize = (double)newValue;
            });

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
            nameof(IsChecked), typeof(bool), typeof(ImageCheckBox), false,
            propertyChanged: (bindable, oldValue, newValue) => {
                //ESTABLECE EL ICONO DE MARCADO O NO                  
                ImageCheckBox checkbox = (ImageCheckBox)bindable;
                // CAMBIA EL RECURSO DEPENDIENDO SI ESTA SELECCIONADO O NO
                if ((bool)newValue)
                    checkbox.boxImage.Source = Xamarin.Forms.ImageSource.FromResource("BUGPublica.Images.icon_checked.png");
                else
                    checkbox.boxImage.Source = Xamarin.Forms.ImageSource.FromResource("BUGPublica.Images.icon_check.png");
                //DISPARA EL EVENTO
                ImageCheckBoxEventArgs args = new ImageCheckBoxEventArgs
                {
                    Id = checkbox.CID,
                    Checked = (bool)newValue
                };
                checkbox.CheckedChanged?.Invoke(checkbox, args);
            });

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(ImageSource), typeof(string), typeof(ImageCheckBox), null,
            propertyChanged: (bindable, oldValue, newValue) => {
                ((ImageCheckBox)bindable).image.Source = (string)newValue;
            });

        public static readonly BindableProperty ImageSizeProperty = BindableProperty.Create(
            nameof(ImageSize), typeof(int), typeof(ImageCheckBox), 20,
            propertyChanged: (bindable, oldValue, newValue) => {
                ((ImageCheckBox)bindable).image.WidthRequest = (int)newValue;
                ((ImageCheckBox)bindable).image.HeightRequest = (int)newValue;
            });

        public event EventHandler<ImageCheckBoxEventArgs> CheckedChanged;

        public ImageCheckBox()
        {
            InitializeComponent();
        }

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            set { SetValue(FontSizeProperty, value); }
            get { return (double)GetValue(FontSizeProperty); }
        }

        public bool IsChecked
        {
            set { SetValue(IsCheckedProperty, value); }
            get { return (bool)GetValue(IsCheckedProperty); }
        }

        public string ImageSource
        {
            set { SetValue(ImageProperty, value); }
            get { return (string)GetValue(ImageProperty); }
        }

        public int ImageSize
        {
            set { SetValue(ImageSizeProperty, value); }
            get { return (int)GetValue(ImageSizeProperty); }
        }

        public int CID { get; set; }

        // TapGestureRecognizer handler.         
        void OnCheckBoxTapped(object sender, EventArgs args)
        {
            IsChecked = !IsChecked;
        }
    }

    public class ImageCheckBoxEventArgs : EventArgs
    {
        public int Id { get; set; }
        public bool Checked { get; set; }
    }
}