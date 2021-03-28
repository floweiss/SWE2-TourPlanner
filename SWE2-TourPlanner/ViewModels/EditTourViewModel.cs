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
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.ViewModels
{
    public class EditTourViewModel : BaseViewModel, ISubject
    {
        private Guid _id;
        private string _name;
        private string _description;
        private string _start;
        private string _end;

        private List<IObserver> _observers = new List<IObserver>();

        public EditTourViewModel(Tour actualTour)
        {
            _id = actualTour.Id;
            _name = actualTour.Name;
            _description = actualTour.Description;
            _start = actualTour.Start;
            _end = actualTour.End;
        }

        public ICommand SaveTourCommand => new RelayCommand(SaveTour);

        public string Name
        {
            get {
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

        public string Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                OnPropertyChanged(nameof(Start));
            }
        }

        public string End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                OnPropertyChanged(nameof(End));
            }
        }

        private void SaveTour(object sender)
        {
            Tour editedTour = new Tour(_id, _name, _description, _start, _end);
            try
            {
                if (String.IsNullOrWhiteSpace(_name) || String.IsNullOrWhiteSpace(_description) || String.IsNullOrWhiteSpace(_start) || String.IsNullOrWhiteSpace(_end))
                {
                    throw new InvalidOperationException();
                }
                ServiceLocator.GetService<ITourService>().EditTour(editedTour);
                ((Window)sender).Close();
                ObserverSingleton.GetInstance.TourObservers.ForEach(Attach);
                Notify();
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("Specify all params");
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
