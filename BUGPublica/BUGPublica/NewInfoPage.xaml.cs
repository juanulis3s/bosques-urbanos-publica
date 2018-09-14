using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Models;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewInfoPage : ContentPage
	{
		public NewInfoPage (NewItem newItem)
		{
			InitializeComponent ();
            BindData(newItem);
		}

        private void BindData(NewItem newItem)
        {
            Title = newItem.Title;
            imgNew.Image = newItem.Image;
            lblDate.Text = newItem.Publication_Date;
            lblCategory.Text = newItem.Category;
            lblIntro.Text = newItem.Intro;
            lblContent.Text = newItem.Content;
        }

    }
}