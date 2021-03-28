﻿using System;
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
            TourDAL tourDal = new TourDAL(ConfigurationManager.AppSettings["connection_string"]);
            return tourDal.GetTours();
        }

        public void AddTour(Tour addedTour)
        {
            TourDAL tourDal = new TourDAL(ConfigurationManager.AppSettings["connection_string"]);
            try
            {
                tourDal.AddTour(addedTour);
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
        }

        public void EditTour(Tour editedTour)
        {
            TourDAL tourDal = new TourDAL(ConfigurationManager.AppSettings["connection_string"]);
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
