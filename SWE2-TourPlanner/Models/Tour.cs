using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Models
{
    public class Tour
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public Tour(string name, string description, string start, string end)
        {
            Name = name;
            Description = description;
            Start = start;
            End = end;
        }
    }
}
