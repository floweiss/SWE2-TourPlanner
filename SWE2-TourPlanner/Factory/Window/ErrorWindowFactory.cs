using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class ErrorWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            ErrorView view = new ErrorView();
            return view;
        }
    }
}
