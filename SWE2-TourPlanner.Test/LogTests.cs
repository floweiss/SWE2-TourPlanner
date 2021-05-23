using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Test
{
    public class LogTests
    {
        private LogService _logService;
        private Mock<ILogDal> _logDalMock;
        private List<IElement> _logList;
        private Log _logById;

        [SetUp]
        public void Setup()
        {
            _logDalMock = new Mock<ILogDal>();
            _logService = new LogService(_logDalMock.Object);
            _logList = new List<IElement>
            {
                new Log(Guid.NewGuid(), "Log1", "Description", "Report", "Car", DateTime.Now, Guid.NewGuid(), "Tour1", 200, 2, Rating.Good),
                new Log(Guid.NewGuid(), "Log2", "desc", "rep", "bike", DateTime.Now, Guid.NewGuid(), "Tour2", 60, 1, Rating.Average),
                new Log(Guid.NewGuid(), "Log3", "Christmas trip", "snowy", "Car", DateTime.Now, Guid.NewGuid(), "Tour1", 200, 2.5, Rating.Awful)
            };
            _logById = new Log(Guid.Parse("0b489c3c-93d6-4a95-9d85-84391052cc97"), "Log by ID", "id desc", "Report",
                "Car", DateTime.Now, Guid.NewGuid(), "Tour1", 500, 3, Rating.Perfect);
        }

        [Test]
        public void Test_GetLogs()
        {
            _logDalMock.Setup(s => s.GetLogs()).Returns(_logList);

            List<IElement> logs = _logService.GetLogs();

            Assert.AreEqual(3, logs.Count);
        }

        [Test]
        public void Test_GetLogsById()
        {
            _logDalMock.Setup(s => s.GetLogById("0b489c3c-93d6-4a95-9d85-84391052cc97")).Returns(_logById);

            Log log = _logService.GetLogById("0b489c3c-93d6-4a95-9d85-84391052cc97");

            Assert.AreEqual("0b489c3c-93d6-4a95-9d85-84391052cc97", log.Id.ToString());
        }
    }
}