using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Moq;
using NUnit.Framework;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Test
{
    public class TourTests
    {
        private TourService _tourService;
        private Mock<ITourDal> _tourDalMock;
        private List<IElement> _tourList;

        private string _directory = "C:\\Tourplanner\\Downloads\\Tours\\Test\\";
        private string _filename = $"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.json";

        [SetUp]
        public void Setup()
        {
            _tourDalMock = new Mock<ITourDal>();
            _tourService = new TourService(_tourDalMock.Object);
            _tourList = new List<IElement>
            {
                new Tour(Guid.NewGuid(), "Tour1", "Desc", "Start", "End", 100),
                new Tour(Guid.NewGuid(), "Tour2", "Description", "Hell", "Heaven", 666),
                new Tour(Guid.NewGuid(), "Tour3", "BESCHREIBUNG", "BEGINN", "ENDE", 300)
            };
        }

        [Test]
        public void Test_CopyTour()
        {
            IElement copiedTour = _tourList[0].Copy();

            Assert.AreEqual($"{_tourList[0].Name} - Copy", copiedTour.Name);
            Assert.AreNotEqual(_tourList[0].Id, copiedTour.Id);
        }

        [Test]
        public void Test_GetTours()
        {
            _tourDalMock.Setup(s => s.GetTours()).Returns(_tourList);

            List<IElement> tours = _tourService.GetTours();

            Assert.AreEqual(3, tours.Count);
        }

        [Test]
        public void Test_AddTour()
        {
            Tour tour = (Tour) _tourList[0];
            _tourDalMock.Setup(s => s.AddTour(tour));

            _tourService.AddTour(tour);

            _tourDalMock.Verify(s => s.AddTour(tour), Times.Once);
        }

        [Test]
        public void Test_EditTour()
        {
            Tour tour = (Tour)_tourList[0];
            _tourDalMock.Setup(s => s.EditTour(tour));

            _tourService.EditTour(tour);

            _tourDalMock.Verify(s => s.EditTour(tour), Times.Once);
        }

        [Test]
        public void Test_DeleteTour()
        {
            Tour tour = (Tour)_tourList[0];
            _tourDalMock.Setup(s => s.DeleteTour(tour));

            _tourService.DeleteTour(tour);

            _tourDalMock.Verify(s => s.DeleteTour(tour), Times.Once);
        }

        [Test]
        public void Test_ExportTours()
        {
            _tourDalMock.Setup(s => s.GetTours()).Returns(_tourList);

            _tourService.ExportTours(_directory, _filename);

            Assert.True(File.Exists(_directory + _filename));
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_directory))
            {
                Directory.Delete(_directory, true);
            }
        }
    }
}