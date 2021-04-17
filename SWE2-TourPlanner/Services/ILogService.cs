using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public interface ILogService
    {
        List<Log> GetLogs();
        void AddLog(Log addedLog);
        void DeleteLog(Log deletedLog);
        void EditLog(Log editedLog);
    }
}
