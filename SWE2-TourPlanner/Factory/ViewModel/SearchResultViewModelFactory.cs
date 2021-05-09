﻿using System;
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
    public class SearchResultViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory windowFactoryLog = new EditLogWindowFactory();
            SearchResultViewModel vm = new SearchResultViewModel(windowFactoryLog);
            return vm;
        }
    }
}
