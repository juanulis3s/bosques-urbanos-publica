using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RuleCell : ViewCell
	{
		public RuleCell ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty NameProperty = BindableProperty.Create(
            nameof(Name), typeof(string), typeof(RuleCell), null);

        public string Name
        {
            set { SetValue(NameProperty, value); }
            get { return (string)GetValue(NameProperty); }
        }
    }
}