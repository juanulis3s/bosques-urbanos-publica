using Xamarin.Forms;

namespace BUGPublica.Behaviors
{
    public class EntryLengthValidatorBehavior : Behavior<Entry>
    {
        public int MaxLength { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            //SE REVISA SI EL TEXTO TIENE MAYOR LONGITUD DEL PERMITIDO
            if(entry.Text.Length > MaxLength)
            {
                string entryText = entry.Text;
                entryText = entryText.Remove(entryText.Length - 1);
                entry.Text = entryText;
            }
        }
    }
}
