using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Services
{
    public interface ITourService
    {
        public List<string> GetTours();

        public List<string> InitializeTours();
    }
}
