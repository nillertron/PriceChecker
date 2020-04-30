using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using PriceChecker.Models;

namespace PriceChecker.ViewModels
{
    class BackgroundWorkerViewModel:BaseViewModel
    {
        private bool _Active = true;
        public bool Active { get => _Active; set { _Active = value; OnPropertyChanged("Active"); } }
        private int _PkIndex = 0;
        public int PkIndex { get => _PkIndex; set { _PkIndex = value; OnPropertyChanged("PkIndex"); } }

        public List<int> Hours { get; } = new List<int> { 1, 3, 6, 10, 20, 24, 48 };

        public ICommand SaveCommand { get; set; }
        private Settings settings;
        public BackgroundWorkerViewModel()
        {
            settings = new Settings();
            GetSettings();
            SaveCommand = new Command(async () => await save());
        }

        private async Task GetSettings()
        {
            var list = await settings.GetAll();
            if(list.Count>0)
            {
                settings = list[0];
                Active = settings.ScraperActive;
                PkIndex = Hours.IndexOf(settings.IntervalHours);
            }
        }
        private async Task save()
        {
            INotifications service = DependencyService.Get<INotifications>();

            if(!Active)
            {
                settings.ScraperActive = false;
                await settings.Update();
                service.StopNotifications();
            }
            else
            {
                settings.ScraperActive = true;
                settings.IntervalHours = Hours[PkIndex];
                await settings.Update();
                service.SaveSetting(Hours[PkIndex]);
            }
            await Application.Current.MainPage.DisplayAlert("Saved", "Succes", "Ok");
        }
    }
}
