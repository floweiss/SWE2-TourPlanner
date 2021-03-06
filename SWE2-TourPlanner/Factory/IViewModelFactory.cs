using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SWE2_TourPlanner.Factory
{
    public interface IViewModelFactory
    {
        object CreateViewModel(DependencyObject sender);
    }
}
