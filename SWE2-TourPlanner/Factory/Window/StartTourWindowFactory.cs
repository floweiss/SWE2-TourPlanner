using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class StartTourWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            StartTourView view = new StartTourView();
            return view;
        }
    }
}
