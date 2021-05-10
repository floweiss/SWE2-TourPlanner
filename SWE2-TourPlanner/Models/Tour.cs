using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Models
{
    public class Tour : IElement
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public double Distance { get; set; }

        public Tour(Guid id, string name, string description, string start, string end, double distance)
        {
            Id = id;
            Name = name;
            Description = description;
            Start = start;
            End = end;
            Distance = distance;
        }

        public IElement Copy()
        {
            Tour other = (Tour)this.MemberwiseClone();
            other.Id = Guid.NewGuid();
            other.Name = this.Name + " - Copy";
            return other;
        }
    }
}
