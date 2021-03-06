﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public class TourService : ITourService
    {
        private ITourDal _tourDal;
        public TourService(ITourDal tourDal)
        {
            _tourDal = tourDal;
        }

        public List<IElement> GetTours()
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

        public void ExportTours(string directory, string filename)
        {
            Directory.CreateDirectory(directory);
            List<Tour> tours = new List<Tour>();
            _tourDal.GetTours().ForEach((tour) =>
            {
                tours.Add((Tour)tour);
            });
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(tours, options);
            File.WriteAllText(directory + filename, json);
        }
    }
}
