using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class AddTourWindowFactory : IWindowFactory
    {
        public void CreateWindow()
        {
            AddTourView view = new AddTourView();
            view.Show();
        }
    }
}
