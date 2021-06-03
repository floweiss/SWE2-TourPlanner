using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public interface IMapService
    {
        void CreateMap(Tour tour, string key);
        void CopyMap(Tour tour, Tour copiedTour);
        //void DeleteMap(Tour deletedTour);
        void DeleteUnusedMaps(List<IElement> currentTours);
        List<Maneuver> GetManeuvers(Tour actualTour, string key);
    }
}
