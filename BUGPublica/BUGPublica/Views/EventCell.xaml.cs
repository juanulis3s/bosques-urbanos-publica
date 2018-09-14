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
	public partial class EventCell : ViewCell
	{
		public EventCell ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), typeof(string), typeof(NewCell), "icon.png");

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(NewCell), null);

        public static readonly BindableProperty ScheduleProperty = BindableProperty.Create(
            nameof(Schedule), typeof(string), typeof(NewCell), null);

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

        public string Schedule
        {
            set { SetValue(ScheduleProperty, value); }
            get { return (string)GetValue(ScheduleProperty); }
        }
    }
}