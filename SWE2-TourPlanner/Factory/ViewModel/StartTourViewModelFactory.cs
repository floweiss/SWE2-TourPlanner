using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public class StartTourViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory windowFactoryError = new ErrorWindowFactory();
            StartTourViewModel vm = new StartTourViewModel(windowFactoryError);
            vm.ServiceLocator.RegisterService<IMapService>(new MapquestService());
            return vm;
        }
    }
}
