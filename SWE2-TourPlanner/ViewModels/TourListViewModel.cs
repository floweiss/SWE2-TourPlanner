using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWE2_TourPlanner.ViewModels
{
    public class TourListViewModel : BaseViewModel
    {
        public ICommand AddTourCommand => new RelayCommand(AddTour);

        private void AddTour(object sender)
        {
            Debug.WriteLine("Add Tour clicked");
        }
    }
}
