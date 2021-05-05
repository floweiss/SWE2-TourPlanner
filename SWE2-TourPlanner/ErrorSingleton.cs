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
    public sealed class ErrorSingleton
    {
        private static readonly object _lock = new object();
        private static ErrorSingleton _instance = null;
        public string ErrorText;

        private ErrorSingleton()
        {
            ErrorText = "Unknown Error";
        }

        public static ErrorSingleton GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ErrorSingleton();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
