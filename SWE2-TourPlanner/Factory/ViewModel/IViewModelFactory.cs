using System.Windows;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public interface IViewModelFactory
    {
        object CreateViewModel(DependencyObject sender);
    }
}
