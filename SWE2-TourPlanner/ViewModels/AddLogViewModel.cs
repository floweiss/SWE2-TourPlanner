﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.ViewModels
{
    public class AddLogViewModel : BaseViewModel
    {
        private string _name;
        private string _description;
        private string _report;
        private string _vehicle;
        private DateTime _dateTime;
        private string _tourId;
        private double _distance;
        private double _totalTime;
        private Rating _rating;

        public AddLogViewModel()
        {
            DateTime = DateTime.Now;
        }

        public ICommand SaveLogCommand => new RelayCommand(SaveLog);

        public string Name
        {
            get
            {
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
        public string Report
        {
            get
            {
                return _report;
            }
            set
            {
                _report = value;
                OnPropertyChanged(nameof(Report));
            }
        }
        public string Vehicle
        {
            get
            {
                return _vehicle;
            }
            set
            {
                _vehicle = value;
                OnPropertyChanged(nameof(Vehicle));
            }
        }
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }
        public string TourId
        {
            get
            {
                return _tourId;
            }
            set
            {
                _tourId = value;
                OnPropertyChanged(nameof(TourId));
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
                if (_distance != value)
                {
                    _distance = value;
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }
        public double TotalTime
        {
            get
            {
                return _totalTime;
            }
            set
            {
                if (_totalTime != value)
                {
                    _totalTime = value;
                    OnPropertyChanged(nameof(TotalTime));
                }
            }
        }
        public Rating Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public IEnumerable<Rating> PossibleRatings
        {
            get
            {
                return Enum.GetValues(typeof(Rating))
                    .Cast<Rating>();
            }
        }

        public List<Tour> PossibleTours
        {
            get
            {
                return ServiceLocator.GetService<ITourService>().GetTours();
            }
        }

        private void SaveLog(object sender)
        {
            Log addedLog = new Log(Guid.NewGuid(), _name, _description, _report, _vehicle, _dateTime,
                Guid.Parse(_tourId), "name", _distance, _totalTime, _rating);
            try
            {
                ServiceLocator.GetService<ILogService>().AddLog(addedLog);
                ((Window)sender).Close();
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("Specify all params");
            }
        }
    }
}
