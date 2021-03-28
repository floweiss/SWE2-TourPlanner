using System.Windows;
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
            TourListViewModel vm = new TourListViewModel(windowFactorySave, windowFactoryEdit);
            vm.ServiceLocator.RegisterService<ITourService>(new TourService());
            ObserverSingleton.GetInstance.TourObservers.Add(vm);
            return vm;
        }
    }
}
