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
            set { _message = value; OnPropertyChanged(); }
        }

        private void LoadMessage()
        {
            Message = GetService<IGreetService>().Greet();
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
