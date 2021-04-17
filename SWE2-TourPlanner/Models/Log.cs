using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Models
{
    public class Log
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public Guid TourId { get; set; }
        public string TourName { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double Speed { get; set; }
        public Rating Rating { get; set; }

        public Log(Guid id, string name, string description, DateTime dateTime, Guid tourId, string tourName, double distance, double duration, Rating rating)
        {
            Id = id;
            Name = name;
            Description = description;
            DateTime = dateTime;
            TourId = tourId;
            TourName = tourName;
            Distance = distance;
            Duration = duration;
            Speed = distance / duration;
            Rating = rating;
        }
    }
}
