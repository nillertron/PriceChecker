using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Webscraper;

namespace PriceChecker.Models
{
    class GulOgGratisVare : Vare
    {
        public override async Task<List<Vare>> Search(string SearchString, IProgress<ProgressReport> report,CancellationTokenSource cts)
        {

            if (report != null)
                ProgressReport.TotalVare ++;
            var scrap = new Scraper();
            var returnList = new List<Vare>();
            var list = await scrap.GetVareListe(SearchString, "guloggratis",cts);
            ReportProgress(report);
            foreach (var l in list)
            {
                returnList.Add((GulOgGratisVare)l);
            }
            return returnList;
        }
    }
}
