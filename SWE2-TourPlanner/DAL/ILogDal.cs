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
        List<Log> GetLogs();
    }
}
