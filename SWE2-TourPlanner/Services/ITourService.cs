using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public interface ITourService
    {
        public List<Tour> GetTours();
        public void AddTour(Tour addedTour);
        public void DeleteTour(Tour deletedTour);
        public void EditTour(Tour editedTour);
    }
}
