using PriceChecker.Models;
using PriceChecker.Services;
using PriceChecker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PriceChecker.ViewModels
{
    class SearchResultViewModel : BaseViewModel
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        private ObservableCollection<Vare> _VareListe;

        StoredSearchObserver sso = new StoredSearchObserver();

        public ObservableCollection<Vare> VareListe { get => _VareListe; set { _VareListe = value; OnPropertyChanged("VareListe"); } }
        private double _Progress;
        public double Progress { get => _Progress; set { _Progress = value; OnPropertyChanged("Progress"); } }
        private bool _ShowProgress = true;
        public bool ShowProgress { get => _ShowProgress; set { _ShowProgress = value; OnPropertyChanged("ShowProgress"); } }
        private bool _ShowCancel = true;
        public bool ShowCancel { get => _ShowCancel; set { _ShowCancel = value; OnPropertyChanged("ShowCancel"); } }
        private Vare _SelectedItem;
        public Vare SelectedItem { get => _SelectedItem; set { _SelectedItem = value; if (_SelectedItem == null) return; Task.Run(async () => await OpenBrowser(_SelectedItem)); _SelectedItem = null; } }
        public ICommand CancelCommand { get; set; }
        public ICommand TapCommand { get; set; }
        public SearchResultViewModel(string searchWord, double maxPrice, double minPrice)
        {
            _VareListe = new ObservableCollection<Vare>();

            SaveSearch(searchWord, maxPrice, minPrice);
            OneSearch(searchWord, maxPrice, minPrice);

            CancelCommand = new Command(async () => await CancelCommandMethod());
            TapCommand = new Command(async(x) => await OpenBrowser(x));
        }
        public SearchResultViewModel(bool GetCache)
        {
            _VareListe = new ObservableCollection<Vare>();
            CancelCommand = new Command(async () => await CancelCommandMethod());
            TapCommand = new Command(async (x) => await OpenBrowser(x));
            if (GetCache)
            {
                CacheRoutine();
                ShowCancel = false;
                ShowProgress = false;
            }
        }

        public async Task CacheRoutine()
        {
            var obj = new CachedVare();
            var liste = await obj.GetAll();
            liste.ForEach(o => VareListe.Add(new Dbavare { Pris = o.Pris, MaxPris=o.MaxPris, MinPris=o.MinPris, Navn=o.Navn, Url=o.Url }));
            await obj.WipeAll();
        }
        private async Task CancelCommandMethod()
        {
            cts.Cancel();
        }
        public async Task OpenBrowser(Object o)
        {
            var ob = o as Vare;
            await Browser.OpenAsync(ob.Url, BrowserLaunchMode.SystemPreferred);
        }
        public SearchResultViewModel(List<StoredSearch> list)
        {
            _VareListe = new ObservableCollection<Vare>();
            MultiSearch(list);

        }
        private async Task SaveSearch(string searchWord, double maxPrice, double minPrice)
        {
            var search = new StoredSearch { MaxPrice = maxPrice, MinPrice = minPrice, SearchWord = searchWord };
            await search.Save();
            await sso.Notify();

        }
        private async Task OneSearch(string searchWord, double maxPrice, double minPrice)
        {
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += UpdateBar;
            var dbaVare = new Dbavare();
            var ebayVare = new Ebayvare();
            var gogVare = new GulOgGratisVare();

            var dbaTask = dbaVare.Search("https://www.dba.dk/soeg/?soeg=" + searchWord, progress, cts);
            var gogTask = gogVare.Search("https://www.guloggratis.dk/s/q-" + searchWord + "/", progress, cts);
            //var ebayTask = ebayVare.Search("https://www.ebay.com/sch/i.html?_nkw=" + searchWord + "&_in_kw=1&_ex_kw=&_sacat=0&LH_Complete=1&_udlo=&_udhi=&_samilow=&_samihi=&_sadis=15&_stpos=&_sargn=-1%26saslc%3D1&_salic=1&_sop=12&_dmd=1&_ipg=50&_fosrp=1");
            var taskList = new List<Task<List<Vare>>>();
            taskList.Add(gogTask);
            taskList.Add(dbaTask);
            await Task.WhenAll(taskList);
            var tempList = new List<Vare>();
            taskList.ForEach(o => o.Result.ForEach(x => tempList.Add(x)));
            tempList = tempList.Where(x => x.Pris <= maxPrice && x.Pris >= minPrice).OrderBy(o => o.Pris).ToList();
            await FyldListView(tempList);
        }

        private void UpdateBar(object sender, ProgressReport e)
        {
            Progress = (double)ProgressReport.Progress / 100;
            if (Progress == 1)
            {
                ShowCancel = false;
                ShowProgress = false;
            }
        }

        private async Task MultiSearch(List<StoredSearch> searchList)
        {
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += UpdateBar;
            var facade = new ScraperFacade();
            var vareListe = await facade.GetVareMultiSearch(searchList, cts, progress);
            await FyldListView(vareListe);
        }

        public async Task FyldListView(List<Vare> liste)
        {
            liste.ForEach(o => VareListe.Add(o));

        }
    }
}
