﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SWE2_TourPlanner.Factory;
using SWE2_TourPlanner.Factory.Window;

namespace SWE2_TourPlanner.ViewModels
{
    public class TourListViewModel : BaseViewModel
    {
        private readonly IWindowFactory _windowFactory;
        public ICommand AddTourCommand => new RelayCommand(AddTour);

        public TourListViewModel(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        private void AddTour(object sender)
        {
            Debug.WriteLine("Add Tour clicked");
            _windowFactory.CreateWindow();
        }
    }
}
