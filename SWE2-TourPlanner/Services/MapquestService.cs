using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public class MapquestService : IMapService
    {
        public void CreateMap(Tour tour)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(ConfigurationManager.AppSettings["mapquest_base_url"] +
                                   $"staticmap/v5/map?start={HttpUtility.UrlEncode(tour.Start)}&end={HttpUtility.UrlEncode(tour.End)}&size=800,600&key=" +
                                   ConfigurationManager.AppSettings["mapquest_key"],
                $"{ConfigurationManager.AppSettings["base_directory"]}{tour.Id}.jpg");
        }

        public void CopyMap(Tour tour, Tour copiedTour)
        {
            File.Copy($"{ConfigurationManager.AppSettings["base_directory"]}{tour.Id}.jpg", $"{ConfigurationManager.AppSettings["base_directory"]}{copiedTour.Id}.jpg");
        }

        /*public void DeleteMap(Tour deletedTour)
        {
            File.Delete($"{ConfigurationManager.AppSettings["base_directory"]}{deletedTour.Id}.jpg");
        }*/

        public void DeleteUnusedMaps(List<Tour> currentTours)
        {
            string[] fileNames = Directory.GetFiles(ConfigurationManager.AppSettings["base_directory"]);
            bool deleteFile;
            foreach (string fileName in fileNames)
            {
                deleteFile = true;
                foreach (Tour currentTour in currentTours)
                {
                    if ($"{ConfigurationManager.AppSettings["base_directory"]}{currentTour.Id}.jpg" == fileName)
                    {
                        deleteFile = false;
                        break;
                    }
                }

                if (deleteFile)
                {
                    File.Delete(fileName);
                }
            }
        }
    }
}
