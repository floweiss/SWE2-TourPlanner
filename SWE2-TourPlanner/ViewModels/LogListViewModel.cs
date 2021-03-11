﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SWE2_TourPlanner.Factory.Window;

namespace SWE2_TourPlanner.ViewModels
{
    public class LogListViewModel : BaseViewModel
    {
        private readonly IWindowFactory _windowFactory;
        public ICommand AddLogCommand => new RelayCommand(AddLog);

        public LogListViewModel(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        private void AddLog(object sender)
        {
            Debug.WriteLine("Add Log clicked");
            _windowFactory.CreateWindow();
        }
    }
}