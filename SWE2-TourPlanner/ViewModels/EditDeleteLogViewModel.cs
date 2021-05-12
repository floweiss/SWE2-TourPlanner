using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using log4net;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;

namespace SWE2_TourPlanner.ViewModels
{
    public class EditDeleteLogViewModel : BaseViewModel, ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private IWindowFactory _windowFactoryError;
        private IWindowFactory _windowFactoryEdit;
        private string _logId;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EditDeleteLogViewModel(IWindowFactory windowFactoryError, IWindowFactory windowFactoryEdit)
        {
            _windowFactoryError = windowFactoryError;
            _windowFactoryEdit = windowFactoryEdit;
            ObserverSingleton.GetInstance.LogObservers.ForEach(Attach);
            log4net.Config.XmlConfigurator.Configure();
        }

        public ICommand DeleteLogCommand => new RelayCommand(DeleteLog);
        public ICommand EditLogCommand => new RelayCommand(EditLog);

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

        public List<IElement> PossibleLogs => ServiceLocator.GetService<ILogService>().GetLogs();

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

        private void EditLog(object sender)
        {
            try
            {
                TourSingleton.GetInstance.EditLog = ServiceLocator.GetService<ILogService>().GetLogById(_logId);
                _windowFactoryEdit.GetWindow().Show();
                ((Window)sender).Close();
            }
            catch (InvalidOperationException e)
            {
                _log.Error("No log chosen to edit");
                ErrorSingleton.GetInstance.ErrorText = "No Log chosen!";
                _windowFactoryError.GetWindow().Show();
            }
            catch (DivideByZeroException e)
            {
                _log.Error("Invalid parameters");
                ErrorSingleton.GetInstance.ErrorText = "The Distance and Total Time must be above 0!";
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
