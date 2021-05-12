﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using log4net;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.ViewModels
{
    public class AddTourViewModel : BaseViewModel, ISubject
    {
        private string _name;
        private string _description;
        private string _start;
        private string _end;
        private double _distance;
        private IWindowFactory _errorWindowFactory;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<IObserver> _observers = new List<IObserver>();

        public AddTourViewModel(IWindowFactory errorWindowFactory)
        {
            ObserverSingleton.GetInstance.TourObservers.ForEach(Attach); // attach when created because all observers are already created
            _errorWindowFactory = errorWindowFactory;
            log4net.Config.XmlConfigurator.Configure();
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

        public double Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        private void SaveTour(object sender)
        {
            Tour addedTour = new Tour(Guid.NewGuid(), _name, _description, _start, _end, _distance);
            try
            {
                if (_distance <= 0)
                {
                    throw new InvalidOperationException();
                }
                ServiceLocator.GetService<ITourService>().AddTour(addedTour);
                ServiceLocator.GetService<IMapService>().CreateMap(addedTour);
                TourSingleton.GetInstance.ActualTour = addedTour;
                ((Window)sender).Close();
                Notify();
            }
            catch (System.Net.WebException e)
            {
                _log.Error("No internet connection or missing/invalid Maquest key");
                ErrorSingleton.GetInstance.ErrorText = "Please check your internet connection and the Mapquest API Key!";
                _errorWindowFactory.GetWindow().Show();
            }
            catch (InvalidOperationException e)
            {
                _log.Error("Not all parameters specified");
                ErrorSingleton.GetInstance.ErrorText = "You need to specify all parameters for the Tour!\nDistance must be more than 0!";
                _errorWindowFactory.GetWindow().Show();
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
