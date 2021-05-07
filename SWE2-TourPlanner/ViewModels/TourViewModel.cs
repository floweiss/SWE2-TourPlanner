using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string _imageSource;

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
        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public TourViewModel()
        {
            TourImageVisibility = Visibility.Visible;
            TourDescriptionVisibility = Visibility.Hidden;
            TourTitle = "No Tour chosen!";
            TourContent = "Click SHOW to show Tour.";
            ImageSource = ConfigurationManager.AppSettings["placeholder_pic"];
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
            try
            {
                TourTitle = TourSingleton.GetInstance.ActualTour.Name;
                TourContent = $"Description:\t{TourSingleton.GetInstance.ActualTour.Description}\n" +
                              $"Start:\t\t{TourSingleton.GetInstance.ActualTour.Start}\n" +
                              $"End:\t\t{TourSingleton.GetInstance.ActualTour.End}";
                ImageSource = $"{ConfigurationManager.AppSettings["base_directory"]}{TourSingleton.GetInstance.ActualTour.Id}.jpg";
            }
            catch (NullReferenceException e)
            {
                TourTitle = "No Tour to display!";
                TourContent = "Chose another Tour to display.";
                ImageSource = ConfigurationManager.AppSettings["placeholder_pic"];
            }
        }
    }
}
