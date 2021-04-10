using System.Configuration;
using System.Windows;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public class HomeViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            HomeViewModel vm = new HomeViewModel();
            vm.ServiceLocator.RegisterService<IGreetService>(new GreetService());
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            vm.ServiceLocator.RegisterService<ITourService>(new TourService(tourDal));
            return vm;
        }
    }
}
