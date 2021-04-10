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

        [SetUp]
        public void Setup()
        {
            _tourDalMock = new Mock<ITourDal>();
            _tourService = new TourService(_tourDalMock.Object);
        }

        [Test]
        public void Test_GetTours()
        {
            _tourDalMock.Setup(s => s.GetTours()).Returns(new List<Tour>
            {
                new Tour(Guid.NewGuid(), "Test", "Desc", "Start", "End")
            });

            List<Tour> tours = _tourService.GetTours();

            Assert.AreEqual("Test", tours[0].Name);
        }
    }
}