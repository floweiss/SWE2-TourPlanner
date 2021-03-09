using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Services
{
    public class TourService : ITourService
    {
        public List<string> GetTours()
        {
            throw new NotImplementedException();
        }

        public List<string> InitializeTours()
        {
            List<string> newTours = new List<string>();
            newTours.Add("Tour2");
            return newTours;
        }
    }
}
