using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    class EditTourViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            EditTourViewModel vm = new EditTourViewModel(TourSingleton.GetInstance.ActualTour);
            vm.ServiceLocator.RegisterService<ITourService>(new TourService());
            return vm;
        }
    }
}
