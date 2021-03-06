﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Factory.ViewModel
{
    class AddTourViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            AddTourViewModel vm = new AddTourViewModel(log);
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            vm.ServiceLocator.RegisterService<ITourService>(new TourService(tourDal));
            vm.ServiceLocator.RegisterService<IMapService>(new MapquestService(ConfigurationManager.AppSettings["base_directory"]));
            return vm;
        }
    }
}
