using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        private List<Tour> _tours;
        private List<IObserver> _observers = new List<IObserver>();

        public ICommand AddTourCommand => new RelayCommand(AddTour);
        public ICommand DeleteTourCommand => new RelayCommand(DeleteTour);
        public ICommand ReportTourCommand => new RelayCommand(GenerateTourReport);
        public ICommand EditTourCommand => new RelayCommand(EditTour);
        public ICommand ShowTourCommand => new RelayCommand(ShowTour);
        public ICommand CopyTourCommand => new RelayCommand(CopyTour);

        public TourListViewModel(IWindowFactory windowFactorySave, IWindowFactory windowFactoryEdit)
        {
            _windowFactorySave = windowFactorySave;
            _windowFactoryEdit = windowFactoryEdit;
        }

        public List<Tour> Tours
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
            Window view = _windowFactorySave.GetWindow();
            view.Show();
        }

        private void DeleteTour(object sender)
        {
            ServiceLocator.GetService<ITourService>().DeleteTour((Tour) sender);
            GetTours();
            if (TourSingleton.GetInstance.ActualTour != null && ((Tour)sender).Id == TourSingleton.GetInstance.ActualTour.Id)
            {
                TourSingleton.GetInstance.ActualTour = null;
                ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach on the fly because not all observers are created
                Notify();
                ObserverSingleton.GetInstance.TourObservers.ForEach(Detach);
            }
        }

        private void GenerateTourReport(object sender)
        {
            Debug.WriteLine("Report Tour clicked");
        }

        private void EditTour(object sender)
        {
            TourSingleton.GetInstance.ActualTour = (Tour) sender;
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
            Tour copiedTour = ((Tour) sender).Copy(); 
            ServiceLocator.GetService<ITourService>().AddTour(copiedTour);
            TourSingleton.GetInstance.ActualTour = copiedTour;
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach on the fly because not all observers are created
            Notify();
            ObserverSingleton.GetInstance.TourObservers.ForEach(Detach);
        }

        public void Update(ISubject subject)
        {
            GetTours();
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
