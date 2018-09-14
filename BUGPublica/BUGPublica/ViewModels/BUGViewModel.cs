using BUGPublica.Models;
using System.Collections.Generic;

namespace BUGPublica.ViewModels
{
    public class BUGViewModel : ViewModelBase
    {
        public IList<BUG> Bugs { get { return BUG.ALL; } }

        BUG selectedBug;

        public BUG SelectedBug
        {
            get { return selectedBug; }
            set
            {
                if(selectedBug != value)
                {
                    selectedBug = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
