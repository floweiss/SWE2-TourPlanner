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
        public List<Tour> GetTours()
        {
            try
            {
                ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
                return tourDal.GetTours();
            }
            catch (StackOverflowException e)
            {
                Console.WriteLine(e);
                return new List<Tour>();
            }
            
        }

        public void AddTour(Tour addedTour)
        {
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            try
            {
                tourDal.AddTour(addedTour);
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }

        public void DeleteTour(Tour deletedTour)
        {
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            try
            {
                tourDal.DeleteTour(deletedTour);
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }

        public void EditTour(Tour editedTour)
        {
            ITourDal tourDal = new TourDal(ConfigurationManager.AppSettings["connection_string"]);
            try
            {
                tourDal.EditTour(editedTour);
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }
    }
}
