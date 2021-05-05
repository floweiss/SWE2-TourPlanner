using System.Configuration;
using System.Windows;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public class TourListViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory windowFactorySave = new AddTourWindowFactory();
            IWindowFactory windowFactoryEdit = new EditTourWindowFactory();
            IWindowFactory windowFactoryDelete = new DeleteTourWindowFactory();
            TourListViewModel vm = new TourListViewModel(windowFactorySave, windowFactoryEdit, windowFactoryDelete);
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            vm.ServiceLocator.RegisterService<ITourService>(new TourService(tourDal));
            vm.ServiceLocator.RegisterService<IMapService>(new MapquestService());
            ObserverSingleton.GetInstance.TourObservers.Add(vm);
            return vm;
        }
    }
}
