using System;

using BUGPublica.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventInfoPage : ContentPage
	{
		public EventInfoPage (EventItem eventItem)
		{
			InitializeComponent ();

            BindData(eventItem);
		}

        private void BindData(EventItem eventItem)
        {
            Title = eventItem.Title;
            imgEvent.Image = eventItem.Image;
            lblDate.Text = eventItem.Schedule;
            lblContent.Text = eventItem.Content;
        }

    }
}