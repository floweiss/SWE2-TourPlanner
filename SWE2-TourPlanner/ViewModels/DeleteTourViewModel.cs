using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;

namespace SWE2_TourPlanner.ViewModels
{
    public class DeleteTourViewModel : BaseViewModel, ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public DeleteTourViewModel(Tour actualTour)
        {
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach when created because all observers are already created
        }

        public ICommand DeleteTourCommand => new RelayCommand(DeleteTour);
        public ICommand CancelCommand => new RelayCommand(CloseWindow);

        private void DeleteTour(object sender)
        {
            Debug.WriteLine("Delete tour");
            ServiceLocator.GetService<ITourService>().DeleteTour(TourSingleton.GetInstance.EditTour);
            //ServiceLocator.GetService<IMapService>().DeleteMap(TourSingleton.GetInstance.EditTour); // map is deleted afterwards
            Notify();
            ((Window)sender).Close();
        }

        private void CloseWindow(object sender)
        {
            ((Window)sender).Close();
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
