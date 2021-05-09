using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class SearchResultWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            SearchResultView view = new SearchResultView();
            return view;
        }
    }
}
