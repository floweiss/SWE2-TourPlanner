using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory
{
    public class HomeViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            HomeViewModel vm = new HomeViewModel();
            vm.ServiceLocator.RegisterService<IGreetService>(new GreetService());
            vm.ServiceLocator.RegisterService<ITourService>(new TourService());
            return vm;
        }
    }
}
