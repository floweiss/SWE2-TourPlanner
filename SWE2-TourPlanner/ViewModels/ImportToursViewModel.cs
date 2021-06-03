using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using log4net;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;

namespace SWE2_TourPlanner.ViewModels
{
    public class ImportToursViewModel : BaseViewModel, ISubject
    {
        private string _tours;
        private bool _isNotImporting;
        private List<IObserver> _observers = new List<IObserver>();
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ImportToursViewModel()
        {
            log4net.Config.XmlConfigurator.Configure();
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach when created because all observers are already created
            _isNotImporting = true;
            _tours =
                "[\r\n  {\r\n    \"Name\": \"Kentucky to NY\",\r\n    \"Description\": \"Awesome Tour\",\r\n    \"Start\": \"Kentucky\",\r\n    \"End\": \"New York\",\r\n    \"Distance\": 200\r\n  },\r\n  {\r\n    \"Name\": \"Dallas to New Jersey\",\r\n    \"Description\": \"Super\",\r\n    \"Start\": \"Dallas\",\r\n    \"End\": \"New Jersey\",\r\n    \"Distance\": 400\r\n  }\r\n]\r\n";
        }

        public ICommand ImportToursCommand => new RelayCommand(ImportTours);

        public string Tours
        {
            get
            {
                return _tours;
            }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public bool IsNotImporting
        {
            get
            {
                return _isNotImporting;
            }
            set
            {
                _isNotImporting = value;
                OnPropertyChanged(nameof(IsNotImporting));
            }
        }

        private void ImportTours(object sender)
        {
            try
            {
                IsNotImporting = false;
                List<ImportedTour> importedTours = JsonSerializer.Deserialize<List<ImportedTour>>(_tours);
                List<Tour> tours = new List<Tour>();
                importedTours.ForEach(importedTour =>
                {
                    if (String.IsNullOrWhiteSpace(importedTour.Name) || String.IsNullOrWhiteSpace(importedTour.Description) || String.IsNullOrWhiteSpace(importedTour.Start) || String.IsNullOrWhiteSpace(importedTour.End))
                    {
                        throw new JsonException();
                    }
                    tours.Add(new Tour(Guid.NewGuid(), importedTour.Name, importedTour.Description, importedTour.Start, importedTour.End, importedTour.Distance));
                });
                tours.ForEach(tour =>
                {
                    ServiceLocator.GetService<ITourService>().AddTour(tour);
                    ServiceLocator.GetService<IMapService>().CreateMap(tour, ConfigurationManager.AppSettings["mapquest_key"]);
                });
                IsNotImporting = true;
                ((Window)sender).Close();
                Notify();
            }
            catch (JsonException e)
            {
                _log.Error("Invalid JSON for import");
                ErrorSingleton.GetInstance.ErrorText = "JSON needs to be an array of Tours and\nevery Tour must include Name, Description, Start and End";
                MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IsNotImporting = true;
            }
            catch (System.Net.WebException e)
            {
                _log.Error("No internet connection or missing/invalid Maquest key");
                ErrorSingleton.GetInstance.ErrorText = "Please check your internet connection and the Mapquest API Key!";
                MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IsNotImporting = true;
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
