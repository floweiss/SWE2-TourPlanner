using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.DAL
{
    public interface ITourDal
    {
        List<IElement> GetTours();
        void AddTour(Tour addedTour);
        void DeleteTour(Tour deletedTour);
        void EditTour(Tour editedTour);
    }
}
