using PriceChecker.Models;
using PriceChecker.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PriceChecker.ViewModels
{
    class StoredSearchDetailViewModel:BaseViewModel
    {
        public StoredSearch Item { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private INavigation Navigation;
        private StoredSearchObserver Observer;
        public StoredSearchDetailViewModel(Object search, INavigation nav)
        {
            Item = search as StoredSearch;
            Navigation = nav;
            Observer = new StoredSearchObserver();
            SaveCommand = new Command(async () => await SaveMethod());
            DeleteCommand = new Command(async () => await DeleteMethod());

        }

        private async Task SaveMethod()
        {
            await Item.Update();
            await Observer.Notify();
            await Application.Current.MainPage.DisplayAlert("Saved", "Succes", "Ok");
        }
        private async Task DeleteMethod()
        {
            await Item.Delete();
            await Observer.Notify();
            await Navigation.PopAsync();
        }
    }
}
