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
    public class PdfReportsTest
    {
        private PdfReportService _pdfReportService;
        private MapquestService _mapquestService;
        private Tour _tour;
        private List<Log> _logList;
        private string _baseDirectory;
        private string _downloadDirectory;

        [SetUp]
        public void Setup()
        {
            _tour = new Tour(Guid.Parse("7b92e3aa-bf59-4ad4-b690-22ae1e7873fc"), "Tour", "Desc", "New York", "New Jersey", 100);
            _logList = new List<Log>
            {
                new Log(Guid.NewGuid(), "Log1", "Description", "Report", "Car", DateTime.Now, Guid.Parse("7b92e3aa-bf59-4ad4-b690-22ae1e7873fc"), "Tour", 200, 2, Rating.Good),
                new Log(Guid.NewGuid(), "Log2", "desc", "rep", "bike", DateTime.Now, Guid.Parse("7b92e3aa-bf59-4ad4-b690-22ae1e7873fc"), "Tour", 60, 1, Rating.Average),
                new Log(Guid.NewGuid(), "Log3", "Christmas trip", "snowy", "Car", DateTime.Now, Guid.Parse("7b92e3aa-bf59-4ad4-b690-22ae1e7873fc"), "Tour", 200, 2.5, Rating.Awful)
            };
            _baseDirectory = "C:\\Tourplanner\\Images\\Test\\";
            _downloadDirectory = "C:\\Tourplanner\\Downloads\\Reports\\Test\\";
            _pdfReportService = new PdfReportService(_baseDirectory, _downloadDirectory);
            _mapquestService = new MapquestService(_baseDirectory);
        }

        [Test]
        public void Test_GenerateTotalReport()
        {
            _pdfReportService.GenerateTotalReport(_logList, "TotalReport_Test.pdf");

            Assert.IsTrue(File.Exists($"{_downloadDirectory}TotalReport_Test.pdf"));
        }

        [Test]
        public void Test_GenerateTourReport()
        {
            _mapquestService.CreateMap(_tour, "CgYKFQAs9XGwzQWrq4AW3DQypxf0Fd10");
            _pdfReportService.GenerateTourReport(_tour, _logList, "TourReport_Test.pdf");

            Assert.IsTrue(File.Exists($"{_downloadDirectory}TourReport_Test.pdf"));
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_baseDirectory))
            {
                Directory.Delete(_baseDirectory, true);
            }
            if (Directory.Exists(_downloadDirectory))
            {
                Directory.Delete(_downloadDirectory, true);
            }
        }
    }
}
