using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class DeleteTourWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            DeleteTourView view = new DeleteTourView();
            return view;
        }
    }
}
