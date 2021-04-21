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

        public List<Log> GetLogs()
        {
            return _logDal.GetLogs();
        }

        public void AddLog(Log addedLog)
        {
            _logDal.AddLog(addedLog);
        }

        public void DeleteLog(Log deletedLog)
        {
            throw new NotImplementedException();
        }

        public void EditLog(Log editedLog)
        {
            throw new NotImplementedException();
        }
    }
}
