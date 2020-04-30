using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webscraper;
using System.Linq;
using System.Threading;

namespace PriceChecker.Models
{
    class Dbavare : Vare
    {
        public override async Task<List<Vare>> Search(string SearchString,IProgress<ProgressReport> report, CancellationTokenSource cts)
        {
            if (report != null)
                ProgressReport.TotalVare++;
            var scrap = new Scraper();
            var returnList = new List<Vare>();

            var list = await scrap.GetVareListe(SearchString, "dba", cts);
            ReportProgress(report);
            foreach (var l in list)
            {
                returnList.Add((Dbavare)l);
            }
            return returnList;
        }
    }
}
