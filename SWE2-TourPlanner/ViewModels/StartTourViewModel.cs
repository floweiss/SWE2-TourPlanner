using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using log4net;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;

namespace SWE2_TourPlanner.ViewModels
{
    public class StartTourViewModel : BaseViewModel
    {
        private List<Maneuver> _maneuvers;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public StartTourViewModel()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public ICommand CloseCommand => new RelayCommand(CloseWindow);

        public List<Maneuver> Maneuvers
        {
            get
            {
                if (_maneuvers == null)
                {
                    GetManeuvers();
                }

                return _maneuvers;
            }
            set
            {
                _maneuvers = value;
                OnPropertyChanged(nameof(Maneuvers));
            }
        }

        private void GetManeuvers()
        {
            try
            {
                Maneuvers = ServiceLocator.GetService<IMapService>().GetManeuvers(TourSingleton.GetInstance.ActualTour, ConfigurationManager.AppSettings["mapquest_key"]);
            }
            catch (System.Net.WebException e)
            {
                _log.Error("No internet connection or missing/invalid Maquest key");
                ErrorSingleton.GetInstance.ErrorText = "Please check your internet connection and the Mapquest API Key!";
                MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindow(object sender)
        {
            ((Window)sender).Close();
        }
    }
}
