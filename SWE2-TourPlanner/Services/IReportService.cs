using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public interface IReportService
    {
        public void GenerateTourReport(Tour tour, List<Log> logs, string filename);
        public void GenerateTotalReport(List<Log> logs, string filename);
    }
}
