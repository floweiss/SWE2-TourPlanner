using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class AddTourWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            AddTourView view = new AddTourView();
            return view;
        }
    }
}
