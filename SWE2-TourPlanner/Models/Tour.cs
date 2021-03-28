using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Models
{
    public class Tour
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public Tour(Guid id, string name, string description, string start, string end)
        {
            Id = id;
            Name = name;
            Description = description;
            Start = start;
            End = end;
        }
    }
}
