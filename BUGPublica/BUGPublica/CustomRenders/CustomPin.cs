using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BUGPublica.CustomRenders
{
    public class CustomPin : Pin
    {
        public string Icon { get; set; }
        public long Id { get; set; }
        public int PlaceId { get; set; }
        public bool HasContent { get; set; }
        public int Index { get; set; }

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
           nameof(IsVisible), typeof(bool), typeof(CustomPin), true);

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public event EventHandler OnPinInfoClicked;

        public void PinInfoClicked()
        {
            OnPinInfoClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
