using System.ComponentModel;

using Android.Content;

using BUGPublica.CustomRenders;
using BUGPublica.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace BUGPublica.Droid.CustomRenders
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                Draw(((CustomEntry)e.NewElement).BackgroundStyle);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == CustomEntry.BackgroundStyleProperty.PropertyName)
            {
                Draw(((CustomEntry)sender).BackgroundStyle);
            }
        }


        private void Draw(EntryStyle entryStyle)
        {
            switch (entryStyle)
            {
                case EntryStyle.NONE:
                    Control.Background = null;
                    break;
                case EntryStyle.ROUNDRECT:
                    break;
                case EntryStyle.LINE:
                    break;
            }
        }
    }
}