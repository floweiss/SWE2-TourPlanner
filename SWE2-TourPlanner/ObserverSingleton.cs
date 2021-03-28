using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner
{
    public sealed class ObserverSingleton
    {
        private static readonly object _lock = new object();
        private static ObserverSingleton _instance = null;
        public List<IObserver> TourObservers;
        public List<IObserver> LogObservers;

        private ObserverSingleton()
        {
            TourObservers = new List<IObserver>();
            LogObservers = new List<IObserver>();
        }

        public static ObserverSingleton GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ObserverSingleton();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
