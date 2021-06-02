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
    public class EditLogViewModel : BaseViewModel, ISubject
    {
        private Guid _id;
        private string _name;
        private string _description;
        private string _report;
        private string _vehicle;
        private DateTime _dateTime;
        private Guid _tourId;
        private double _distance;
        private double _totalTime;
        private Rating _rating;
        private List<IObserver> _observers = new List<IObserver>();
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EditLogViewModel()
        {
            _id = TourSingleton.GetInstance.EditLog.Id;
            _name = TourSingleton.GetInstance.EditLog.Name;
            _description = TourSingleton.GetInstance.EditLog.Description;
            _report = TourSingleton.GetInstance.EditLog.Report;
            _vehicle = TourSingleton.GetInstance.EditLog.Vehicle;
            _dateTime = TourSingleton.GetInstance.EditLog.DateTime;
            _tourId = TourSingleton.GetInstance.EditLog.TourId;
            _distance = TourSingleton.GetInstance.EditLog.Distance;
            _totalTime = TourSingleton.GetInstance.EditLog.TotalTime;
            _rating = TourSingleton.GetInstance.EditLog.Rating;
            ObserverSingleton.GetInstance.LogObservers.ForEach(Attach); // attach when created because all observers are already created
            log4net.Config.XmlConfigurator.Configure();
        }

        public ICommand SaveLogCommand => new RelayCommand(SaveLog);

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
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
        public Guid TourId
        {
            get
            {
                return _tourId;
            }
            set
            {
                _tourId = value;
                OnPropertyChanged(nameof(TourId));
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

        public IEnumerable<Rating> PossibleRatings =>
            Enum.GetValues(typeof(Rating))
                .Cast<Rating>();

        public List<IElement> PossibleTours => ServiceLocator.GetService<ITourService>().GetTours();

        private void SaveLog(object sender)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(_name) || String.IsNullOrWhiteSpace(_description) || String.IsNullOrWhiteSpace(_report) || String.IsNullOrWhiteSpace(_vehicle))
                {
                    throw new InvalidOperationException();
                }
                if (_distance <= 0 || _totalTime <= 0)
                {
                    throw new DivideByZeroException();
                }
                Log editedLog = new Log(_id, _name, _description, _report, _vehicle, _dateTime,
                    _tourId, "name", _distance, _totalTime, _rating);
                ServiceLocator.GetService<ILogService>().EditLog(editedLog);
                ((Window)sender).Close();
                Notify();
            }
            catch (InvalidOperationException e)
            {
                _log.Error("Not all parameters specified");
                ErrorSingleton.GetInstance.ErrorText = "You need to specify all parameters for the Log!";
                MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentNullException e)
            {
                _log.Error("Not all parameters specified");
                ErrorSingleton.GetInstance.ErrorText = "You need to specify all parameters for the Log!";
                MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o => o.Update(this));
        }
    }
}
