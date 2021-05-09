using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner.Models
{
    public interface IElement
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public IElement Copy();
    }
}
