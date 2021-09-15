using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using BUGPublica.CustomRenders;
using BUGPublica.Droid.CustomRenders;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Content.Res;
using Android.Graphics;
using System.Threading.Tasks;
using Android.Support.V4.Content;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace BUGPublica.Droid.CustomRenders
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        //ESTA OBTENIDO EN https://github.com/xamarin/Xamarin.Forms/blob/master/Xamarin.Forms.Platform.Android/AppCompat/NavigationPageRenderer.cs#L52
        int TransitionDuration = 220;

        public CustomNavigationPageRenderer(Context context) : base(context) { }

        private AndroidX.AppCompat.Widget.Toolbar toolbar;

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            var result = base.OnPushAsync(view, animated);

            Activity activity = Xamarin.Essentials.Platform.CurrentActivity;
            AndroidX.AppCompat.Widget.Toolbar toolbar = activity.FindViewById<AndroidX.AppCompat.Widget.Toolbar>(
                Resource.Id.toolbar);

            if(toolbar != null)
            {
                if(toolbar.NavigationIcon != null)
                {
                    toolbar.NavigationIcon = ContextCompat.GetDrawable(
                        Context, Resource.Drawable.ic_back);
                }
            }
            return result;
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var result = base.OnPopViewAsync(page, animated);

            Device.StartTimer(TimeSpan.FromMilliseconds(TransitionDuration), () =>
            {
                Activity activity = Xamarin.Essentials.Platform.CurrentActivity;
                AndroidX.AppCompat.Widget.Toolbar toolbar = activity.FindViewById<AndroidX.AppCompat.Widget.Toolbar>(
                    Resource.Id.toolbar);

                if (toolbar != null)
                {
                    if (toolbar.NavigationIcon != null)
                    {
                        toolbar.NavigationIcon = ContextCompat.GetDrawable(
                            Context, Resource.Drawable.ic_back);
                    }
                }
                return false;
            });
           
            return result;
        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if(child.GetType() == typeof(AndroidX.AppCompat.Widget.Toolbar) || child.GetType() == typeof(Toolbar))
            {
                toolbar = (AndroidX.AppCompat.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child;
            if(view.GetType() == typeof(AndroidX.AppCompat.Widget.AppCompatTextView) || view.GetType() == typeof(TextView))
            {
                TextView textView = (TextView)view;
                Typeface fontFace = Typeface.CreateFromAsset(Context.Assets, "Montserrat_Light.ttf");
                textView.SetTypeface(fontFace, TypefaceStyle.Normal);
            }
        }

    }
}