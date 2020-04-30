using Android.App.Job;
using PriceChecker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceChecker.Services
{
    public class exJobService : JobService
    {
        private static string tag = "ExampleJobService";
        private bool JobCancel = false;
        public override bool OnStartJob(JobParameters @params)
        {
            DoBackGroundWork(@params);
            return true;
        }
        private async Task DoBackGroundWork(JobParameters @params)
        {
#pragma warning disable CS4014 
            Task.Run(async () =>
            {
                var ss = new StoredSearch();
                var liste = await ss.GetAll();
                if (liste.Count > 0)
                {

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
