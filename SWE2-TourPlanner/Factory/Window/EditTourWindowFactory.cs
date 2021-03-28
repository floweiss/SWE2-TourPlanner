using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class EditTourWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            EditTourView view = new EditTourView();
            return view;
        }
    }
}
