using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWE2_TourPlanner.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ICommand SearchCommand => new RelayCommand(SearchTourRoute);

        public void SearchTourRoute(Object sender)
        {
            Debug.WriteLine("SearchTourRoute clicked");
        }
    }
}
