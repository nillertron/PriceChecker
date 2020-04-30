using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PriceChecker.Services;
using PriceChecker.Views;
using PriceChecker.Models;
using Android.App.Job;
using System.Threading.Tasks;

namespace PriceChecker
{
    public partial class App : Application
    {

        public App(bool flag)
        {
            InitializeComponent();
            if(!flag)
            {
                DependencyService.Register<MockDataStore>();
                MainPage = new MainPage();
            }
            else
            {
                DependencyService.Register<MockDataStore>();
                MainPage = new MainPage("");
            }

            
        }
        public App()
        {
            InitializeComponent();

                DependencyService.Register<MockDataStore>();
                MainPage = new MainPage();



        }


        protected override void OnStart()
        {
            var dbm = new DBManager();
            //dbm.WipeTabelStoredSearchAsync();
            var searObjToCreateTable = new StoredSearch();
            var Cache = new CachedVare();
            var Settings = new Settings();
            CheckForFirstStartup(Settings);
            
        }

        private async Task CheckForFirstStartup(Settings settings)
        {
            await Task.Delay(1000);
            var list = await settings.GetAll();
            INotifications service = DependencyService.Get<INotifications>();
            if (list.Count==0)
            {
                settings.IntervalHours = 3;
                settings.ScraperActive = true;
                await settings.Save();
            }
            else
            {
                settings = list[0];
            }
            if(settings.ScraperActive)
            service.SaveSetting(settings.IntervalHours);
            else
            {
                try
                {
                    service.StopNotifications();
                }
                catch (Exception) { }
            }

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
