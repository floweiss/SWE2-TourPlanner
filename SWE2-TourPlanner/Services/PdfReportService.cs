using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SWE2_TourPlanner.Models;

namespace SWE2_TourPlanner.Services
{
    public class PdfReportService : IReportService
    {
        private string _baseDirectory;
        private string _downloadDirectory;

        public PdfReportService(string baseDirectory, string downloadDirectory)
        {
            _baseDirectory = baseDirectory;
            _downloadDirectory = downloadDirectory;

            Directory.CreateDirectory(_baseDirectory);
            Directory.CreateDirectory(_downloadDirectory);
        }

        public void GenerateTourReport(Tour tour, List<Log> logs, string filename)
        {
            PdfWriter writer = new PdfWriter(_downloadDirectory + filename);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph header = new Paragraph($"{tour.Name} Report")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetMarginBottom(15);
            document.Add(header);

            Image map = new Image(ImageDataFactory
                    .Create($"{_baseDirectory}{tour.Id}.jpg"))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetMarginBottom(15);
            document.Add(map);

            Paragraph description =
                new Paragraph(
                        $"Description: {tour.Description}\nStart: {tour.Start}\nEnd: {tour.End}\nDistance: {tour.Distance} km")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(14)
                    .SetMarginBottom(15);
            document.Add(description);

            Paragraph logHeader = new Paragraph("Logs:")
                .SetFontSize(14)
                .SetMarginBottom(5);
            document.Add(logHeader);

            int counter = 1;
            Paragraph row;
            logs.ForEach((log) =>
            {
                row = new Paragraph($"{counter}.) {log.Name} ({log.DateTime}): {log.Distance} km in {log.TotalTime} hours ({log.Speed} km/h) with {log.Vehicle}. Rating: {log.Rating}");
                document.Add(row);
                counter++;
            });

            document.Close();
        }

        public void GenerateTotalReport(List<Log> logs, string filename)
        {
            PdfWriter writer = new PdfWriter(_downloadDirectory + filename);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph header = new Paragraph("Total Report")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetMarginBottom(15);
            document.Add(header);

            Dictionary<string, double> sums = new Dictionary<string, double>();
            sums["distance"] = 0;
            sums["time"] = 0;
            sums["count"] = 0;
            logs.ForEach((log) =>
            {
                sums["count"]++;
                sums["distance"] += log.Distance;
                sums["time"] += log.TotalTime;
            });

            Paragraph description =
                new Paragraph($"Log Count: {sums["count"]}\nTotal Distance: {sums["distance"]} km\nTotal Time: {sums["time"]} hours\nAverage Speed: {Math.Round(sums["distance"]/sums["time"], 2)} km/h")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(14)
                    .SetMarginBottom(15);
            document.Add(description);

            Paragraph logHeader = new Paragraph("Logs:")
                .SetFontSize(14)
                .SetMarginBottom(5);
            document.Add(logHeader);

            int counter = 1;
            Paragraph row;
            logs.ForEach((log) =>
            {
                row = new Paragraph($"{counter}.) {log.Name} ({log.DateTime}) of {log.TourName}: {log.Distance} km in {log.TotalTime} hours ({log.Speed} km/h) with {log.Vehicle}." +
                                    $"\nDescription: {log.Description}\nReport: {log.Report}\nRating: {log.Rating}");
                document.Add(row);
                counter++;
            });

            document.Close();
        }
    }
}
