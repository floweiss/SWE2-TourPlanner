using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.DAL
{
    public interface ILogDal
    {
        List<IElement> GetLogs();
        List<Log> GetLogsForTour(Tour tour);
        Log GetLogById(string logId);
        void AddLog(Log addedLog);
        void DeleteLog(string logId);
        void EditLog(Log editedLog);
    }
}
