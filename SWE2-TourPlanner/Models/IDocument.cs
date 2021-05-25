using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;

namespace SWE2_TourPlanner.Models
{
    public interface IDocument
    {
        DocumentMetadata GetMetadata();
        void Compose(IContainer container);
    }
}
