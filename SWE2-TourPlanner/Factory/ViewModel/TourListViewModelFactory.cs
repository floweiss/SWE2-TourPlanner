using System.Windows;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public class TourListViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory windowFactory = new AddTourWindowFactory();
            TourListViewModel vm = new TourListViewModel(windowFactory);
            return vm;
        }
    }
}
