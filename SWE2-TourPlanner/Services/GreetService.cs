using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Services
{
    public class GreetService : IGreetService
    {
        public string Greet()
        {
            return "Welcome To Tour Planner!";
        }
    }
}
