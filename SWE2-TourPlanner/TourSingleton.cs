using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner
{
    public sealed class TourSingleton
    {
        private static readonly object _lock = new object();
        private static TourSingleton _instance = null;
        public Tour ActualTour;
        public Tour EditTour;

        private TourSingleton()
        {
            ActualTour = null;
            EditTour = null;
        }

        public static TourSingleton GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TourSingleton();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
