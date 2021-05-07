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

namespace SWE2_TourPlanner.Factory.ViewModel
{
    public class ImportToursViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory errorWindowFactory = new ErrorWindowFactory();
            ImportToursViewModel vm = new ImportToursViewModel(errorWindowFactory);
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            vm.ServiceLocator.RegisterService<ITourService>(new TourService(tourDal));
            vm.ServiceLocator.RegisterService<IMapService>(new MapquestService());
            return vm;
        }
    }
}
