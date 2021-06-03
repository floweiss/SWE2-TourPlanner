using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    class DeleteTourViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            DeleteTourViewModel vm = new DeleteTourViewModel(TourSingleton.GetInstance.EditTour);
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            vm.ServiceLocator.RegisterService<ITourService>(new TourService(tourDal));
            vm.ServiceLocator.RegisterService<IMapService>(new MapquestService(ConfigurationManager.AppSettings["base_directory"]));
            return vm;
        }
    }
}
