using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public class LogService : ILogService
    {
        private ILogDal _logDal;

        public LogService(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public List<IElement> GetLogs()
        {
            return _logDal.GetLogs();
        }

        public Log GetLogById(string logId)
        {
            if (logId == null)
            {
                throw new InvalidOperationException();
            }
            return _logDal.GetLogById(logId);
        }

        public void AddLog(Log addedLog)
        {
            _logDal.AddLog(addedLog);
        }

        public void DeleteLog(string logId)
        {
            _logDal.DeleteLog(logId);
        }

        public void EditLog(Log editedLog)
        {
            _logDal.EditLog(editedLog);
        }
    }
}
