using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;

namespace SWE2_TourPlanner.ViewModels
{
    public class EditDeleteLogViewModel : BaseViewModel, ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private IWindowFactory _windowFactoryError;
        private string _logId;

        public EditDeleteLogViewModel(IWindowFactory windowFactoryError)
        {
            _windowFactoryError = windowFactoryError;
            ObserverSingleton.GetInstance.LogObservers.ForEach(Attach);
        }

        public ICommand DeleteLogCommand => new RelayCommand(DeleteLog);

        public string LogId
        {
            get
            {
                return _logId;
            }
            set
            {
                _logId = value;
                OnPropertyChanged(nameof(LogId));
            }
        }

        public List<Log> PossibleLogs => ServiceLocator.GetService<ILogService>().GetLogs();

        private void DeleteLog(object sender)
        {
            try
            {
                ServiceLocator.GetService<ILogService>().DeleteLog(_logId);
                ((Window)sender).Close();
                Notify();
            }
            catch (InvalidOperationException e)
            {
                ErrorSingleton.GetInstance.ErrorText = "No Log chosen!";
                _windowFactoryError.GetWindow().Show();
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
