using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SWE2_TourPlanner.Services;

namespace SWE2_TourPlanner.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private string _message;
        private List<string> _tours;

        public string Message
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_message))
                {
                    LoadMessage();
                }

                return _message;
            }
            set
            {
                _message = value; OnPropertyChanged();
            }
        }

        public List<string> Tours
        {
            get
            {
                if (_tours == null || _tours.Count == 0)
                {
                    InitializeTours();
                }

                return _tours;
            }
            set
            {
                _tours = value; OnPropertyChanged();
            }
        }

        private void LoadMessage()
        {
            Message = GetService<IGreetService>().Greet();
        }

        private void InitializeTours()
        {
            Tours = GetService<ITourService>().InitializeTours();
        }

        public ICommand ChangeTextButtonClicked => new RelayCommand(ChangeText);

        private void ChangeText(object sender)
        {
            string newText = "New Text who dis";
            if (Message == newText)
            {
                LoadMessage();
            }
            else
            {
                Message = newText;
            }
        }
    }
}
