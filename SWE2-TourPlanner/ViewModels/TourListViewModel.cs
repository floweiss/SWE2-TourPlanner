using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SWE2_TourPlanner.Factory;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;

namespace SWE2_TourPlanner.ViewModels
{
    public class TourListViewModel : BaseViewModel, IObserver, ISubject
    {
        private readonly IWindowFactory _windowFactorySave;
        private readonly IWindowFactory _windowFactoryEdit;
        private readonly IWindowFactory _windowFactoryDelete;
        private readonly IWindowFactory _windowFactoryImport;
        private List<IElement> _tours;
        private List<IObserver> _observers = new List<IObserver>();

        public ICommand AddTourCommand => new RelayCommand(AddTour);
        public ICommand DeleteTourCommand => new RelayCommand(DeleteTour);
        public ICommand ReportTourCommand => new RelayCommand(GenerateTourReport);
        public ICommand EditTourCommand => new RelayCommand(EditTour);
        public ICommand ShowTourCommand => new RelayCommand(ShowTour);
        public ICommand CopyTourCommand => new RelayCommand(CopyTour);
        public ICommand ExportCommand => new RelayCommand(ExportTours);
        public ICommand ImportCommand => new RelayCommand(ImportTours);

        public TourListViewModel(IWindowFactory windowFactorySave, IWindowFactory windowFactoryEdit, IWindowFactory windowFactoryDelete, IWindowFactory windowFactoryImport)
        {
            _windowFactorySave = windowFactorySave;
            _windowFactoryEdit = windowFactoryEdit;
            _windowFactoryDelete = windowFactoryDelete;
            _windowFactoryImport = windowFactoryImport;
        }

        public List<IElement> Tours
        {
            get
            {
                if (_tours == null)
                {
                    GetTours();
                }

                return _tours;
            }
            set
            {
                _tours = value; OnPropertyChanged(nameof(Tours));
            }
        }

        private void GetTours()
        {
            Tours = ServiceLocator.GetService<ITourService>().GetTours();
        }

        private void AddTour(object sender)
        {
            _windowFactorySave.GetWindow().Show();
        }

        private void DeleteTour(object sender)
        {
            TourSingleton.GetInstance.EditTour = (Tour)sender;
            TourSingleton.GetInstance.ActualTour = null;
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach on the fly because not all observers are created
            Notify();
            ObserverSingleton.GetInstance.TourObservers.ForEach(Detach);
            Window view = _windowFactoryDelete.GetWindow();
            view.Show();
        }

        private void EditTour(object sender)
        {
            TourSingleton.GetInstance.EditTour = (Tour) sender;
            TourSingleton.GetInstance.ActualTour = null;
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach on the fly because not all observers are created
            Notify();
            ObserverSingleton.GetInstance.TourObservers.ForEach(Detach);
            Window view = _windowFactoryEdit.GetWindow();
            view.Show();
        }

        private void ShowTour(object sender)
        {
            TourSingleton.GetInstance.ActualTour = (Tour) sender;
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach on the fly because not all observers are created
            Notify();
            ObserverSingleton.GetInstance.TourObservers.ForEach(Detach);
        }

        private void CopyTour(object sender)
        {
            Tour copiedTour = ((Tour) ((Tour) sender).Copy()); 
            ServiceLocator.GetService<ITourService>().AddTour(copiedTour);
            ServiceLocator.GetService<IMapService>().CopyMap(((Tour)sender), copiedTour);
            TourSingleton.GetInstance.ActualTour = copiedTour;
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach on the fly because not all observers are created
            Notify();
            ObserverSingleton.GetInstance.TourObservers.ForEach(Detach);
        }

        private void GenerateTourReport(object sender)
        {
            List<Log> logs = ServiceLocator.GetService<ILogService>().GetLogsForTour((Tour) sender);
            string filename = $"{Regex.Replace(((Tour)sender).Name, @"\s+", "")}_Report_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.pdf";
            ServiceLocator.GetService<IReportService>().GenerateTourReport((Tour)sender, logs, filename);
            ErrorSingleton.GetInstance.ErrorText = $"Tour Report generated and saved to file:\n{ConfigurationManager.AppSettings["download_directory"]}Reports\\{filename}";
            MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportTours(object sender)
        {
            string directory = $"{ConfigurationManager.AppSettings["download_directory"]}Tours\\";
            string filename = $"Tours_Export_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.json";
            ServiceLocator.GetService<ITourService>().ExportTours(directory, filename);
            ErrorSingleton.GetInstance.ErrorText = $"All Tours exported and saved to file:\n{directory}{filename}";
            MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ImportTours(object sender)
        {
            _windowFactoryImport.GetWindow().Show();
        }

        public void Update(ISubject subject)
        {
            GetTours();
            ServiceLocator.GetService<IMapService>().DeleteUnusedMaps(Tours);
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
