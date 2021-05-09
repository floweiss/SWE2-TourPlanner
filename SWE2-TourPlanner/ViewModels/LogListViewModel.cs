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
    public class LogListViewModel : BaseViewModel, IObserver
    {
        private List<IElement> _logs;
        private readonly IWindowFactory _windowFactoryAdd;
        private readonly IWindowFactory _windowFactoryEditDelete;
        public ICommand AddLogCommand => new RelayCommand(AddLog);
        public ICommand EditDeleteLogCommand => new RelayCommand(EditDeleteLog);
        public ICommand ReportLogCommand => new RelayCommand(GenerateLogReport);

        public LogListViewModel(IWindowFactory windowFactoryAdd, IWindowFactory windowFactoryEditDelete)
        {
            _windowFactoryAdd = windowFactoryAdd;
            _windowFactoryEditDelete = windowFactoryEditDelete;
        }

        public List<IElement> Logs
        {
            get
            {
                if (_logs == null)
                {
                    GetLogs();
                }
                return _logs;
            }
            set
            {
                _logs = value; OnPropertyChanged(nameof(Logs));
            }
        }

        private void GetLogs()
        {
            Logs = ServiceLocator.GetService<ILogService>().GetLogs();
        }

        private void AddLog(object sender)
        {
            Debug.WriteLine("Add Log clicked");
            Window view = _windowFactoryAdd.GetWindow();
            view.Show();
        }

        private void EditDeleteLog(object sender)
        {
            _windowFactoryEditDelete.GetWindow().Show();
        }

        private void GenerateLogReport(object sender)
        {
            Debug.WriteLine("Report Log clicked");
        }

        public void Update(ISubject subject)
        {
            GetLogs();
        }
    }
}
