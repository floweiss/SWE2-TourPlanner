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
    public class SearchResultViewModel : BaseViewModel, ISubject
    {
        private List<IElement> _searchResults;
        private Visibility _resultVisibility;
        private Visibility _emptyVisibility;
        private List<IObserver> _observers = new List<IObserver>();
        private IWindowFactory _windowFactoryLog;

        public SearchResultViewModel(IWindowFactory windowFactoryLog)
        {
            _windowFactoryLog = windowFactoryLog;
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach);
            _searchResults = TourSingleton.GetInstance.SearchResults;
            if (_searchResults.Count > 0)
            {
                _resultVisibility = Visibility.Visible;
                _emptyVisibility = Visibility.Hidden;
            }
            else
            {
                _resultVisibility = Visibility.Hidden;
                _emptyVisibility = Visibility.Visible;
            }
        }

        public ICommand CloseCommand => new RelayCommand(CloseWindow);
        public ICommand ShowCommand => new RelayCommand(ShowElement);

        public List<IElement> SearchResults
        {
            get
            {
                return _searchResults;
            }
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }
        public Visibility ResultVisibility
        {
            get
            {
                return _resultVisibility;
            }
            set
            {
                _resultVisibility = value;
                OnPropertyChanged(nameof(ResultVisibility));
            }
        }
        public Visibility EmptyVisibility
        {
            get
            {
                return _emptyVisibility;
            }
            set
            {
                _emptyVisibility = value;
                OnPropertyChanged(nameof(EmptyVisibility));
            }
        }

        private void CloseWindow(object sender)
        {
            ((Window) sender).Close();
        }

        private void ShowElement(object sender)
        {
            try
            {
                TourSingleton.GetInstance.ActualTour = (Tour)sender;
                Notify();
            }
            catch (InvalidCastException e)
            {
                TourSingleton.GetInstance.EditLog = (Log)sender; 
                _windowFactoryLog.GetWindow().Show();
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
