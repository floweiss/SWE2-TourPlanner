using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public class TourService : ITourService
    {
        private ITourDal _tourDal;
        public TourService()
        {
            _tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
        }

        public List<Tour> GetTours()
        {
            return _tourDal.GetTours();
        }

        public void AddTour(Tour addedTour)
        {
            try
            {
                _tourDal.AddTour(addedTour);
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }

        public void DeleteTour(Tour deletedTour)
        {
            _tourDal.DeleteTour(deletedTour);
        }

        public void EditTour(Tour editedTour)
        {
            try
            {
                _tourDal.EditTour(editedTour);
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }
    }
}
