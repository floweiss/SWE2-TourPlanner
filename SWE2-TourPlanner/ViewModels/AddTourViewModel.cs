using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.ViewModels
{
    public class AddTourViewModel : BaseViewModel
    {
        public ICommand SaveTourCommand => new RelayCommand(SaveTour);

        private void SaveTour(object sender)
        {
            Debug.WriteLine("Save Tour clicked");
        }
    }
}
