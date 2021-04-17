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
    public class LogListViewModel : BaseViewModel
    {
        private List<Log> _logs;
        private readonly IWindowFactory _windowFactory;
        public ICommand AddLogCommand => new RelayCommand(AddLog);
        public ICommand DeleteLogCommand => new RelayCommand(DeleteLog);
        public ICommand ReportLogCommand => new RelayCommand(GenerateLogReport);

        public LogListViewModel(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public List<Log> Logs
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
            Window view = _windowFactory.GetWindow();
            view.Show();
        }

        private void DeleteLog(object sender)
        {
            Debug.WriteLine("Delete Log clicked");
        }

        private void GenerateLogReport(object sender)
        {
            Debug.WriteLine("Report Log clicked");
        }
    }
}
