using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWE2_TourPlanner.ViewModels
{
    public class TourViewModel : BaseViewModel
    {
        public ICommand ShowTourRouteCommand => new RelayCommand(ShowTourRoute);
        public ICommand ShowTourDescriptionCommand => new RelayCommand(ShowTourDescription);

        public void ShowTourRoute(Object sender)
        {
            Debug.WriteLine("ShowTourRoute clicked");
        }

        public void ShowTourDescription(Object sender)
        {
            Debug.WriteLine("ShowTourDescription clicked");
        }
    }
}
