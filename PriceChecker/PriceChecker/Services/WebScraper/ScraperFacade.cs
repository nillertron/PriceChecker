using PriceChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PriceChecker
{
    public class ScraperFacade
    {
        public async Task<List<Vare>> GetVareMultiSearch(List<StoredSearch> searchList, CancellationTokenSource cts, IProgress<ProgressReport> progress = null)
        {
            var taskList = new List<Task<List<Vare>>>();
            var vareListe = new List<Vare>();
            var dbaVare = new Dbavare();
            var gogVare = new GulOgGratisVare();


            foreach (var o in searchList)
            {
                var dbaTask = dbaVare.Search("https://www.dba.dk/soeg/?soeg=" + o.SearchWord, progress, cts);
                var gogTask = gogVare.Search("https://www.guloggratis.dk/s/q-" + o.SearchWord + "/", progress, cts);
                taskList.Add(dbaTask);
                taskList.Add(gogTask);


            }

            await Task.WhenAll(taskList);
            var counter = 0;
            taskList.ForEach(o => o.Result.ForEach(x => vareListe.Add(x)));
            for (int i = 0; i < vareListe.Count; i++)
            {
                if (vareListe[i].Navn.ToLower().Contains(searchList[counter].SearchWord.ToLower()))
                {
                    vareListe[i].MaxPris = searchList[counter].MaxPrice;
                    vareListe[i].MinPris = searchList[counter].MinPrice;

                }
                else
                {
                    if (counter < searchList.Count - 1)
                    {
                        counter++;
                        i--;
                    }

                }

            }
            return vareListe.Where(o => o.Pris <= o.MaxPris && o.Pris >= o.MinPris).ToList();
        }
    }
}
