using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SWE2_TourPlanner.ViewModels
{
    public class TourViewModel : BaseViewModel, IObserver
    {
        private Visibility _tourImageVisibility;
        private Visibility _tourDescriptionVisibility;
        private string _tourTitle;
        private string _tourContent;

        public Visibility TourImageVisibility
        {
            get
            {
                return _tourImageVisibility;
            }
            set
            {
                _tourImageVisibility = value;
                OnPropertyChanged(nameof(TourImageVisibility));
            }
        }

        public Visibility TourDescriptionVisibility
        {
            get
            {
                return _tourDescriptionVisibility;
            }
            set
            {
                _tourDescriptionVisibility = value;
                OnPropertyChanged(nameof(TourDescriptionVisibility));
            }
        }

        public string TourTitle
        {
            get
            {
                return _tourTitle;
            }
            set
            {
                _tourTitle = value;
                OnPropertyChanged(nameof(TourTitle));
            }
        }

        public string TourContent
        {
            get
            {
                return _tourContent;
            }
            set
            {
                _tourContent = value;
                OnPropertyChanged(nameof(TourContent));
            }
        }

        public TourViewModel()
        {
            TourImageVisibility = Visibility.Hidden;
            TourDescriptionVisibility = Visibility.Visible;
            TourTitle = "No Tour chosen!";
            TourContent = "Click Show to show Tour";
        }

        public ICommand ShowTourRouteCommand => new RelayCommand(ShowTourRoute);
        public ICommand ShowTourDescriptionCommand => new RelayCommand(ShowTourDescription);

        public void ShowTourRoute(Object sender)
        {
            Debug.WriteLine("ShowTourRoute clicked");
            TourImageVisibility = Visibility.Visible;
            TourDescriptionVisibility = Visibility.Hidden;
        }

        public void ShowTourDescription(Object sender)
        {
            Debug.WriteLine("ShowTourDescription clicked");
            TourImageVisibility = Visibility.Hidden;
            TourDescriptionVisibility = Visibility.Visible;
        }

        public void Update(ISubject subject)
        {
            TourTitle = TourSingleton.GetInstance.ActualTour.Name;
            TourContent = $"Description: {TourSingleton.GetInstance.ActualTour.Description}\n" +
                          $"Start: {TourSingleton.GetInstance.ActualTour.Start}\n" +
                          $"End: {TourSingleton.GetInstance.ActualTour.End}";
        }
    }
}
