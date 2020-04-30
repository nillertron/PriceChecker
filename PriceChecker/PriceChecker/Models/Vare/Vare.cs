using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Webscraper;
using System.Threading;

namespace PriceChecker.Models
{
    public abstract class Vare
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public double Pris { get; set; } = 0;
        public string Url { get; set; }
        public double MaxPris { get; set; }
        public double MinPris { get; set; }
        public string DisplayName { get { if (Navn.Length >= 35) return $"{Navn.Substring(0, 35)}... - {Pris} Kr."; else return $"{Navn} - {Pris} Kr."; } }
        abstract public Task<List<Vare>> Search(string SearchString, IProgress<ProgressReport> progress, CancellationTokenSource cts);

        public void ReportProgress(IProgress<ProgressReport> report)
        {
            if (report != null)
            {
                ProgressReport.CurrentVare++;
                ProgressReport.Progress = (ProgressReport.CurrentVare / ProgressReport.TotalVare) * 100;
                report.Report(ProgressReport.GetInstans());
            }
        }


    }
}
