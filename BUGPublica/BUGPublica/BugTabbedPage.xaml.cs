using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Models;

namespace BUGPublica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BugTabbedPage : CustomRenders.CustomTabbedPage
    {
        public static BUG Bug { get; private set; }

        public BugTabbedPage (BUG bug)
        {
            Bug = bug;

            InitializeComponent();
            
            Title = Bug.Name.ToUpper();
        }
    }
}