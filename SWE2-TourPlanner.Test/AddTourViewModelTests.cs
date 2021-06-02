using System;
using System.Collections.Generic;
using log4net;
using Moq;
using NUnit.Framework;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Test
{
    public class AddTourViewModelTests
    {
        private AddTourViewModel _addTourViewModel;
        private Mock<ILog> _loggerMock;
        private Mock<object> _senderMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILog>();
            _senderMock = new Mock<object>();
            _addTourViewModel = new AddTourViewModel(_loggerMock.Object);

            _addTourViewModel.Name = "Tour";
            _addTourViewModel.Description = "Description";
            _addTourViewModel.Start = "Vienna";
            _addTourViewModel.End = "Fugging";
            _addTourViewModel.Distance = 300;
        }

        [Test]
        public void Test_SaveTourWithEmptyName()
        {
            _addTourViewModel.Name = null;

            _addTourViewModel.SaveTour(_senderMock.Object);

            Assert.AreEqual("You need to specify all parameters for the Tour!\nDistance must be more than 0!", ErrorSingleton.GetInstance.ErrorText);
        }

        [Test]
        public void Test_SaveTourWithInvalidDistance()
        {
            _addTourViewModel.Distance = -100;

            _addTourViewModel.SaveTour(_senderMock.Object);

            Assert.AreEqual("You need to specify all parameters for the Tour!\nDistance must be more than 0!", ErrorSingleton.GetInstance.ErrorText);
        }
    }
}