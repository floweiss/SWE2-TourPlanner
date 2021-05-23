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
    public class AddLogViewModel : BaseViewModel, ISubject
    {
        private string _name;
        private string _description;
        private string _report;
        private string _vehicle;
        private DateTime _dateTime;
        private string _tourId;
        private double _distance;
        private double _totalTime;
        private Rating _rating;
        private IWindowFactory _errorWindowFactory;
        private List<IObserver> _observers = new List<IObserver>();
        private ILog _log;

        public AddLogViewModel(IWindowFactory errorWindowFactory, ILog log)
        {
            DateTime = DateTime.Now;
            ObserverSingleton.GetInstance.LogObservers.ForEach(Attach); // attach when created because all observers are already created
            _errorWindowFactory = errorWindowFactory;
            _log = log;
            log4net.Config.XmlConfigurator.Configure();
        }

        public ICommand SaveLogCommand => new RelayCommand(SaveLog);

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
        public string TourId
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

        public void SaveLog(object sender)
        {
            try
            {
                if (_distance <= 0 || _totalTime <= 0)
                {
                    throw new DivideByZeroException();
                }

                if (String.IsNullOrWhiteSpace(_name) || String.IsNullOrWhiteSpace(_description) || String.IsNullOrWhiteSpace(_report) || String.IsNullOrWhiteSpace(_vehicle))
                {
                    throw new InvalidOperationException();
                }
                Log addedLog = new Log(Guid.NewGuid(), _name, _description, _report, _vehicle, _dateTime,
                    Guid.Parse(_tourId), "name", _distance, _totalTime, _rating);
                ServiceLocator.GetService<ILogService>().AddLog(addedLog);
                ((Window)sender).Close();
                Notify();
            }
            catch (InvalidOperationException e)
            {
                _log.Error("Not all parameters specified");
                ErrorSingleton.GetInstance.ErrorText = "You need to specify all parameters for the Log!";
                try
                {
                    _errorWindowFactory.GetWindow().Show();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            catch (ArgumentNullException e)
            {
                _log.Error("Not all parameters specified");
                ErrorSingleton.GetInstance.ErrorText = "You need to specify all parameters for the Log!";
                _errorWindowFactory.GetWindow().Show();
            }
            catch (DivideByZeroException e)
            {
                _log.Error("Distance or Time below or equal 0");
                ErrorSingleton.GetInstance.ErrorText = "The Distance and Total Time must be above 0!";
                try
                {
                    _errorWindowFactory.GetWindow().Show();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
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
