using BUGPublica.Models;
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
	public partial class BugCell : ContentView
	{
		public BugCell ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), typeof(ImageSource), typeof(BugCell), null);

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
           nameof(Text), typeof(string), typeof(BugCell), null);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private BUG _bug;

        public BUG Bug
        {
            get
            {
                return _bug;
            }
            set
            {
                _bug = value;
                Text = GetTextFormatted(_bug);
            }
        }

        /// <summary>
        /// ABRE LA PAGINA DEL BOSQUE
        /// </summary>
        public void OnTapped(object sender, EventArgs e)
        {
            if (Bug != null)
                Navigation.PushAsync(new BugTabbedPage(Bug));
        }


        private string GetTextFormatted(BUG bug)
        {
            string name = bug.Name.ToUpper();

            if (bug.Id == BUG.ID_BUG_MIRADOR_INDEPENDENCIA || bug.Id == BUG.ID_BUG_PUERTA_BARRANCA)
                return name;

            if (name.StartsWith("BOSQUES"))
            {
                name = name.Remove(7, 1);
                name = name.Insert(7, "\n");
            }
            else if (name.StartsWith("PARQUE"))
            {
                name = name.Remove(6, 1);
                name = name.Insert(6, "\n");
            }


            return name;
        }
    }
}