using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Webscraper;

namespace PriceChecker.Models
{
    class Ebayvare : Vare
    {
        public override async Task<List<Vare>> Search(string SearchString,IProgress<ProgressReport> progress, CancellationTokenSource cts)
        {
            var scrap = new Scraper();
            var returnList = new List<Vare>();
            var list = await scrap.GetVareListe(SearchString, "ebay", cts);
            foreach (var l in list)
            {
                returnList.Add((Ebayvare)l);
            }
            return returnList;
        }
    }
}
