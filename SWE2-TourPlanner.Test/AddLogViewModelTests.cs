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
    public class AddLogViewModelTests
    {
        private AddLogViewModel _addLogViewModel;
        private Mock<ILog> _loggerMock;
        private Mock<object> _senderMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILog>();
            _senderMock = new Mock<object>();
            _addLogViewModel = new AddLogViewModel(_loggerMock.Object);

            _addLogViewModel.Distance = 100;
            _addLogViewModel.TotalTime = 1;
        }

        [Test]
        public void Test_SaveLogWithNegativeDistance()
        {
            _addLogViewModel.Distance = -10;

            _addLogViewModel.SaveLog(_senderMock.Object);

            Assert.AreEqual("The Distance and Total Time must be above 0!", ErrorSingleton.GetInstance.ErrorText);
        }

        [Test]
        public void Test_SaveLogWithNothingSet()
        {
            _addLogViewModel.SaveLog(_senderMock.Object);

            Assert.AreEqual("You need to specify all parameters for the Log!", ErrorSingleton.GetInstance.ErrorText);
        }
    }
}