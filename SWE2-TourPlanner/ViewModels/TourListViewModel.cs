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
            Debug.WriteLine("Add Tour clicked");
            Window view = _windowFactorySave.GetWindow();
            view.Show();
        }

        private void DeleteTour(object sender)
        {
            Debug.WriteLine("Delete Tour clicked");
            ServiceLocator.GetService<ITourService>().DeleteTour((Tour) sender);
            Tours = ServiceLocator.GetService<ITourService>().GetTours();
            if (TourSingleton.GetInstance.ActualTour != null && ((Tour)sender).Id == TourSingleton.GetInstance.ActualTour.Id)
            {
                TourSingleton.GetInstance.ActualTour = null;
                ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach on the fly because not all observers are created
                Notify();
            }
        }

        private void GenerateTourReport(object sender)
        {
            Debug.WriteLine("Report Tour clicked");
        }

        private void EditTour(object sender)
        {
            Debug.WriteLine("Edit Tour clicked");
            TourSingleton.GetInstance.ActualTour = (Tour) sender;
            Window view = _windowFactoryEdit.GetWindow();
            view.Show();
        }

        private void ShowTour(object sender)
        {
            Debug.WriteLine("Show Tour clicked");
            TourSingleton.GetInstance.ActualTour = (Tour) sender;
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach on the fly because not all observers are created
            Notify();
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
