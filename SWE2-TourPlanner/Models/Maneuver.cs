using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Models
{
    public class Maneuver
    {
        public string IconUrl { get; set; }
        public string Narrative { get; set; }

        public Maneuver(string iconUrl, string narrative)
        {
            IconUrl = iconUrl;
            Narrative = narrative;
        }
    }
}
