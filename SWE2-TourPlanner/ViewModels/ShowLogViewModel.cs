using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using log4net;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.ViewModels
{
    public class ShowLogViewModel : BaseViewModel
    {
        private string _name;
        private string _description;
        private string _report;
        private string _vehicle;
        private DateTime _dateTime;
        private string _tourName;
        private double _distance;
        private double _totalTime;
        private Rating _rating;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ShowLogViewModel()
        {
            _name = TourSingleton.GetInstance.EditLog.Name;
            _description = TourSingleton.GetInstance.EditLog.Description;
            _report = TourSingleton.GetInstance.EditLog.Report;
            _vehicle = TourSingleton.GetInstance.EditLog.Vehicle;
            _dateTime = TourSingleton.GetInstance.EditLog.DateTime;
            _tourName = TourSingleton.GetInstance.EditLog.TourName;
            _distance = TourSingleton.GetInstance.EditLog.Distance;
            _totalTime = TourSingleton.GetInstance.EditLog.TotalTime;
            _rating = TourSingleton.GetInstance.EditLog.Rating;
            log4net.Config.XmlConfigurator.Configure();
        }

        public ICommand CloseWindowCommand => new RelayCommand((object sender) => ((Window)sender).Close());

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public string Report
        {
            get
            {
                return _report;
            }
            set
            {
                _report = value;
                OnPropertyChanged(nameof(Report));
            }
        }
        public string Vehicle
        {
            get
            {
                return _vehicle;
            }
            set
            {
                _vehicle = value;
                OnPropertyChanged(nameof(Vehicle));
            }
        }
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }
        public string TourName
        {
            get
            {
                return _tourName;
            }
            set
            {
                _tourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }
        public double Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                if (_distance != value)
                {
                    _distance = value;
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }
        public double TotalTime
        {
            get
            {
                return _totalTime;
            }
            set
            {
                if (_totalTime != value)
                {
                    _totalTime = value;
                    OnPropertyChanged(nameof(TotalTime));
                }
            }
        }
        public Rating Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }
    }
}
