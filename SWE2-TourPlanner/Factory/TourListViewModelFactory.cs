using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory
{
    public class TourListViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            TourListViewModel vm = new TourListViewModel();
            return vm;
        }
    }
}
