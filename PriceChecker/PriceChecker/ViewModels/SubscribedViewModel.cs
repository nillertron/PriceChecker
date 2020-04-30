using PriceChecker.Models;
using PriceChecker.Services;
using PriceChecker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PriceChecker.ViewModels
{
    class SubscribedViewModel:BaseViewModel, IObserver
    {
        private ObservableCollection<StoredSearch> _StoredSearchedList;

        public ObservableCollection<StoredSearch> StoredSearchList { get => _StoredSearchedList; set { _StoredSearchedList = value; OnPropertyChanged("StoredSearchList"); } }
        public ICommand RunSearch { get; set; }

        private INavigation navigation;
        StoredSearchObserver Sso = new StoredSearchObserver();



        public SubscribedViewModel(INavigation nav)
        {
            _StoredSearchedList = new ObservableCollection<StoredSearch>();
            Task.Run(async () => await StartUp());
            RunSearch = new Command(async () => await RunSearchMethod());
            navigation = nav;
            Sso.Attach(this);
        }
        private async Task StartUp()
        {
            StoredSearchList.Clear();
            var search = new StoredSearch();
            var phList = await search.GetAll();
            phList.ForEach(o => StoredSearchList.Add(o));


        }
        private async Task RunSearchMethod()
        {
            var list = new List<StoredSearch>();
            foreach (var o in StoredSearchList)
            {
                list.Add(o);
            }
            await navigation.PushAsync(new SearchResultPage(list));
        }

        public async Task Update()
        {
            await StartUp();
        }
    }
}
