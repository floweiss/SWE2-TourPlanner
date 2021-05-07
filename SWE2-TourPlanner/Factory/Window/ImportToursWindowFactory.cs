using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class ImportToursWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            ImportToursView view = new ImportToursView();
            return view;
        }
    }
}
