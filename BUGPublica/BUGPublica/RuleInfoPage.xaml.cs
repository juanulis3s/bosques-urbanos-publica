
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Models;
using BUGPublica.CustomRenders;

namespace BUGPublica
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RuleInfoPage : ContentPage
	{
		public RuleInfoPage (Rule rule)
		{
			InitializeComponent ();

            //SE ESTABLECE EL TITULO DE LA PAGINA
            Title = rule.Name.ToUpper();

            //SE MUESTRA EL CONTENIDO DE LA REGLA
            scroll.Content = new CustomHtmlLabel
            {
                TextColor = Color.White,
                Text = rule.Content
            };
		}
	}
}