using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BUGPublica.ViewModels
{
    [ContentProperty("Source")]
    class ImageResourceExtension : Xamarin.Forms.Xaml.IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            ImageSource imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }
}
