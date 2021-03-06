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
        private String _message;

        public String Message
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
            Message = "New Text who dis";
        }
    }
}
