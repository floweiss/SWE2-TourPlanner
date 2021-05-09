using System;
using System.Collections.Generic;
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

        [SetUp]
        public void Setup()
        {
            _tourDalMock = new Mock<ITourDal>();
            _tourService = new TourService(_tourDalMock.Object);
            _tourList = new List<IElement>
            {
                new Tour(Guid.NewGuid(), "Tour1", "Desc", "Start", "End"),
                new Tour(Guid.NewGuid(), "Tour2", "Description", "Hell", "Heaven"),
                new Tour(Guid.NewGuid(), "Tour3", "BESCHREIBUNG", "BEGINN", "ENDE")
            };
        }

        [Test]
        public void Test_GetTours()
        {
            _tourDalMock.Setup(s => s.GetTours()).Returns(_tourList);

            List<IElement> tours = _tourService.GetTours();

            Assert.AreEqual(3, tours.Count);
        }

        [Test]
        public void Test_CopyTour()
        {
            IElement copiedTour = _tourList[0].Copy();

            Assert.AreEqual($"{_tourList[0].Name} - Copy", copiedTour.Name);
            Assert.AreNotEqual(_tourList[0].Id, copiedTour.Id);
        }
    }
}