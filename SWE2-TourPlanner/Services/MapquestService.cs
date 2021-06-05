using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using log4net;
using Newtonsoft.Json.Linq;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public class MapquestService : IMapService
    {
        private WebClient _webClient;
        private string _baseDirectory;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MapquestService(string baseDirectory)
        {
            _webClient = new WebClient();
            _baseDirectory = baseDirectory;
            Directory.CreateDirectory(_baseDirectory);
            log4net.Config.XmlConfigurator.Configure();
        }

        public void CreateMap(Tour tour, string key)
        {
            _webClient.DownloadFile($"https://www.mapquestapi.com/staticmap/v5/map?start={HttpUtility.UrlEncode(tour.Start)}&end={HttpUtility.UrlEncode(tour.End)}&size=800,600&key=" + key,
                $"{_baseDirectory}{tour.Id}.jpg");
            _log.Info("Map received from Mapquest API");
        }

        public void CopyMap(Tour tour, Tour copiedTour)
        {
            File.Copy($"{ConfigurationManager.AppSettings["base_directory"]}{tour.Id}.jpg", $"{ConfigurationManager.AppSettings["base_directory"]}{copiedTour.Id}.jpg");
            _log.Info("Map copied");
        }

        /*public void DeleteMap(Tour deletedTour)
        {
            File.Delete($"{ConfigurationManager.AppSettings["base_directory"]}{deletedTour.Id}.jpg");
        }*/

        public void DeleteUnusedMaps(List<IElement> currentTours)
        {
            string[] fileNames = Directory.GetFiles(_baseDirectory);
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
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (IOException e)
                    {
                        _log.Error("could not delete file, still in use");
                    }
                }
            }
            _log.Info("Unused maps deleted");
        }

        public List<Maneuver> GetManeuvers(Tour actualTour, string key)
        {
            string result = _webClient.DownloadString($"https://www.mapquestapi.com/directions/v2/route?from={HttpUtility.UrlEncode(actualTour.Start)}&to={HttpUtility.UrlEncode(actualTour.End)}&key=" + key);
            dynamic json = JObject.Parse(result)["route"]["legs"][0]["maneuvers"];
            List<Maneuver> maneuvers = new List<Maneuver>();
            foreach (var maneuver in json)
            {
                maneuvers.Add(new Maneuver(maneuver["iconUrl"].ToString(), maneuver["narrative"].ToString()));
            }
            _log.Info("Maneuvers received from Mapquest API");
            return maneuvers;
        }
    }
}
