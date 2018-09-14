using Xamarin.Forms;

namespace BUGPublica.CustomRenders
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty BackgroundStyleProperty = BindableProperty.Create(
            nameof(BackgroundStyle), typeof(EntryStyle), typeof(CustomEntry), EntryStyle.NONE);

        public EntryStyle BackgroundStyle
        {
            set { SetValue(BackgroundStyleProperty, value); }
            get { return (EntryStyle) GetValue(BackgroundStyleProperty); }
        }
    }

    public enum EntryStyle
    {
        NONE, ROUNDRECT, LINE
    }
}
