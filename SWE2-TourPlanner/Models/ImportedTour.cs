using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Models
{
    public class ImportedTour
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public double Distance { get; set; }

        public ImportedTour(string name, string description, string start, string end, double distance)
        {
            Name = name;
            Description = description;
            Start = start;
            End = end;
            Distance = distance;
        }
    }
}
