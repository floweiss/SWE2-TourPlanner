using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Models
{
    public class Log : IElement
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Report { get; set; }
        public string Vehicle { get; set; }
        public DateTime DateTime { get; set; }
        public Guid TourId { get; set; }
        public string TourName { get; set; }
        public double Distance { get; set; }
        public double TotalTime { get; set; }
        public double Speed { get; set; }
        public Rating Rating { get; set; }

        public Log(Guid id, string name, string description, string report, string vehicle, DateTime dateTime, Guid tourId, string tourName, double distance, double totalTime, Rating rating)
        {
            Id = id;
            Name = name;
            Description = description;
            Report = report;
            Vehicle = vehicle;
            DateTime = dateTime;
            TourId = tourId;
            TourName = tourName;
            Distance = distance;
            TotalTime = totalTime;
            Speed = Math.Round(distance / totalTime, 2);
            Rating = rating;
        }

        public IElement Copy()
        {
            throw new NotImplementedException();
        }
    }
}
