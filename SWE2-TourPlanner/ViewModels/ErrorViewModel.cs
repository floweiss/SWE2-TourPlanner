using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SWE2_TourPlanner.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {
        private string _errorText;

        public ErrorViewModel(string errorText)
        {
            ErrorText = errorText;
        }

        public ICommand DismissCommand => new RelayCommand(CloseWindow);

        public string ErrorText
        {
            get
            {
                return _errorText;
            }
            set
            {
                _errorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
        }

        private void CloseWindow(object sender)
        {
            ((Window)sender).Close();
        }
    }
}
