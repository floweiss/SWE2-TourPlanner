using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.ViewModels;
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public class LogListViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory windowFactory = new AddLogWindowFactory();
            LogListViewModel vm = new LogListViewModel(windowFactory);
            return vm;
        }
    }
}
