using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public ICommand TotalReportCommand => new RelayCommand(GenerateTotalReport);

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
            Window view = _windowFactoryAdd.GetWindow();
            view.Show();
        }

        private void EditDeleteLog(object sender)
        {
            _windowFactoryEditDelete.GetWindow().Show();
        }

        private void GenerateTotalReport(object sender)
        {
            List<Log> logs = new List<Log>();
            ServiceLocator.GetService<ILogService>().GetLogs().ForEach((element) => logs.Add((Log)element));
            string filename = $"{ConfigurationManager.AppSettings["download_directory"]}Reports\\TotalReport_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.pdf";
            ServiceLocator.GetService<IReportService>().GenerateTotalReport(logs, filename);
            ErrorSingleton.GetInstance.ErrorText = $"Total Report generated and saved to file:\n{filename}";
            MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void Update(ISubject subject)
        {
            GetLogs();
        }
    }
}
