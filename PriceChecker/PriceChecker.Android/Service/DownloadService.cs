using Android.App;
using Android.App.Job;
using PriceChecker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PriceChecker.Droid.Service
{
    [Service(Name = "com.companyname.pricechecker.DownloadService",
            Permission = "android.permission.BIND_JOB_SERVICE")]
    public class DownloadService : JobService
    {
        public static MainActivity instance;
        public DownloadService()
        {
           
        }
        private bool JobCancel = false;
        private CancellationTokenSource cts = new CancellationTokenSource();
        public override bool OnStartJob(JobParameters @params)
        {
            DoBackGroundWork(@params);
            return true;
        }
        private async Task DoBackGroundWork(JobParameters @params)
        {
            var instans = new Notifications();
#pragma warning disable CS4014 
            Task.Run(async () =>
            {
                var ss = new StoredSearch();
                var liste = await ss.GetAll();
                if (liste.Count > 0)
                {
                    var ws = new ScraperFacade();
                    var vareListe = await ws.GetVareMultiSearch(liste,cts);
                    if(vareListe.Count > 0)
                    {
                        instans.SendNotification("Goods of interest found!", "Check out your Price Checker app " + vareListe.Count + " goods of interest");
                        vareListe.ForEach(async o =>
                        {
                            var cacheObj = new CachedVare { MaxPris = o.MaxPris, MinPris = o.MinPris, Navn = o.Navn, Pris = o.Pris, Url = o.Url };
                            if(!await cacheObj.DuplicateCheck())
                            {
                                await cacheObj.Save();
                            }
                        });
                    }
                }

                JobFinished(@params, false);
            }
            );
#pragma warning restore CS4014 

        }

        public override bool OnStopJob(JobParameters @params)
        {

            JobCancel = true;
            return false;
        }
    }
}
