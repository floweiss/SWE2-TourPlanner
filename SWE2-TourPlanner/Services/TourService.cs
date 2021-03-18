using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public class TourService : ITourService
    {
        public List<Tour> InitializeTours()
        {
            throw new NotImplementedException();
        }

        public List<Tour> GetTours()
        {
            List<Tour> newTours = new List<Tour>();
            newTours.Add(new Tour("Tour1", "This is Tour 1", "Wien", "Hollabrunn"));
            return newTours;
        }

        public void AddTour(Tour addedTour)
        {
            throw new NotImplementedException();
        }
    }
}
