using System;
using System.Collections.Generic;
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

namespace SWE2_TourPlanner.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private string _searchText;
        private IWindowFactory _windowFactoryResult;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SearchViewModel(IWindowFactory windowFactoryResult)
        {
            _windowFactoryResult = windowFactoryResult;
            _searchText = "";
            log4net.Config.XmlConfigurator.Configure();
        }

        public ICommand SearchCommand => new RelayCommand(SearchTourRoute);

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        private void SearchTourRoute(object sender)
        {
            if (String.IsNullOrWhiteSpace(_searchText))
            {
                _log.Error("Tried to look up empty text or null");
                ErrorSingleton.GetInstance.ErrorText = "Type in some text to search!";
                MessageBox.Show(ErrorSingleton.GetInstance.ErrorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                TourSingleton.GetInstance.SearchResults.Clear();
                ServiceLocator.GetService<ITourService>().GetTours()
                    .ForEach((tour) =>
                    {
                        if (tour.Name.ToLower().Contains(_searchText.ToLower()))
                        {
                            TourSingleton.GetInstance.SearchResults.Add(tour);
                        }
                    });
                ServiceLocator.GetService<ILogService>().GetLogs().ForEach((log) =>
                {
                    if (log.Name.ToLower().Contains(_searchText.ToLower()))
                    {
                        TourSingleton.GetInstance.SearchResults.Add(log);
                    }
                });
                _windowFactoryResult.GetWindow().Show();
            }
        }
    }
}
