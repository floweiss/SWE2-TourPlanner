﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class AddLogWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            AddLogView view = new AddLogView();
            return view;
        }
    }
}
