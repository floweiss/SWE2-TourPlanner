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
            IWindowFactory windowFactoryError = new ErrorWindowFactory();
            IWindowFactory windowFactoryImport = new ImportToursWindowFactory();
            TourListViewModel vm = new TourListViewModel(windowFactorySave, windowFactoryEdit, windowFactoryDelete, windowFactoryError, windowFactoryImport);
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            vm.ServiceLocator.RegisterService<ITourService>(new TourService(tourDal));
            ILogDal logDal = new LogDal(ConfigurationManager.AppSettings["connection_string"]);
            vm.ServiceLocator.RegisterService<ILogService>(new LogService(logDal));
            vm.ServiceLocator.RegisterService<IMapService>(new MapquestService());
            vm.ServiceLocator.RegisterService<IReportService>(new PdfReportService());
            ObserverSingleton.GetInstance.TourObservers.Add(vm);
            return vm;
        }
    }
}
