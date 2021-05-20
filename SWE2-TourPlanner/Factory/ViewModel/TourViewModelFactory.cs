using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public class TourViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory windowFactoryStart = new StartTourWindowFactory();
            IWindowFactory windowFactoryError= new ErrorWindowFactory();
            TourViewModel vm = new TourViewModel(windowFactoryStart, windowFactoryError);
            ObserverSingleton.GetInstance.TourObservers.Add(vm);
            return vm;
        }
    }
}
