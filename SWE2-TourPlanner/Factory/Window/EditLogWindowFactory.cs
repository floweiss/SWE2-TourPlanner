using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Views;

namespace SWE2_TourPlanner.Factory.Window
{
    public class EditLogWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            EditLogView view = new EditLogView();
            return view;
        }
    }
}
