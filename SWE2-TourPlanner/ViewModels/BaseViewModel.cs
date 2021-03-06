using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.ViewModels
{
    public class BaseViewModel : NotifyPropertyChangedBase
    {
        private ServiceLocator _serviceLocator = new ServiceLocator();
        public ServiceLocator ServiceLocator => _serviceLocator;
        public T GetService<T>()
        {
            return _serviceLocator.GetService<T>();
        }
    }
}
