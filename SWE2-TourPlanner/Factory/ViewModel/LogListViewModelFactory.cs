using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.ViewModels;
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public class LogListViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory windowFactoryAdd = new AddLogWindowFactory();
            IWindowFactory windowFactoryDeleteEdit = new EditDeleteLogWindowFactory();
            LogListViewModel vm = new LogListViewModel(windowFactoryAdd, windowFactoryDeleteEdit);
            ILogDal logDal = new LogDal(ConfigurationManager.AppSettings["connection_string"]);
            vm.ServiceLocator.RegisterService<ILogService>(new LogService(logDal));
            vm.ServiceLocator.RegisterService<IReportService>(new PdfReportService(ConfigurationManager.AppSettings["base_directory"], $"{ConfigurationManager.AppSettings["download_directory"]}Reports\\")); 
            ObserverSingleton.GetInstance.LogObservers.Add(vm);
            ObserverSingleton.GetInstance.TourObservers.Add(vm);
            return vm;
        }
    }
}
