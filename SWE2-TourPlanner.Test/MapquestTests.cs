using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.Services;

namespace SWE2_TourPlanner.Test
{
    public class MapquestTests
    {
        private MapquestService _mapquestService;
        private Tour _tour;
        private string _baseDirectory;

        [SetUp]
        public void Setup()
        {
            _mapquestService = new MapquestService();
            _tour = new Tour(Guid.Parse("7d3b557e-81b4-420a-a47d-e3d2a3e92dae"), "Tour", "Desc", "New York", "New Jersey", 100);
            _baseDirectory = "C:\\Tourplanner\\Images\\Test\\";
        }

        [Test]
        public void Test_GetTourMap()
        {
            _mapquestService.CreateMap(_tour, "CgYKFQAs9XGwzQWrq4AW3DQypxf0Fd10", _baseDirectory);

            Assert.IsTrue(File.Exists($"{_baseDirectory}{_tour.Id}.jpg"));
        }

        [Test]
        public void Test_GetManeuvers()
        {
            List<Maneuver> maneuvers = _mapquestService.GetManeuvers(_tour, "CgYKFQAs9XGwzQWrq4AW3DQypxf0Fd10");

            Assert.AreEqual("Start out going southwest on Broadway toward Murray St.", maneuvers[0].Narrative);
            Assert.AreEqual(12, maneuvers.Count);
        }

        [Test]
        public void Test_DeleteUnusedMaps()
        {
            _mapquestService.DeleteUnusedMaps(new List<IElement>(), _baseDirectory);

            Assert.IsTrue(Directory.GetFiles(_baseDirectory).Length == 0);
        }
    }
}
