using PriceChecker.Models;
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
    class SearchViewModel:BaseViewModel
    {
        public ObservableCollection<Valuta> ValutaList { get; set; }
        private string _SearchWord = string.Empty;
        public string SearchWord { get => _SearchWord; set { _SearchWord = value; OnPropertyChanged("SearchWord"); } }
        private double _MaxPrice;
        public double MaxPrice { get => _MaxPrice; set { _MaxPrice = value; OnPropertyChanged("MaxPrice"); } }
        private double _MinPrice;
        public double MinPrice { get => _MinPrice; set { _MinPrice = value; OnPropertyChanged("MinPrice"); } }

        public ICommand Btn1Click { get; set; }
        public INavigation Navigation;
        public SearchViewModel(INavigation nav)
        {
            Navigation = nav;
            ValutaList = new ObservableCollection<Valuta> { new Valuta { valuta = "DKK", Kurs = 1 } };
            Btn1Click = new Command(async () => await Btn1());

        }
        private async Task Btn1()
        {
            if (SearchWord.Length > 0)
            {
               await Navigation.PushAsync(new SearchResultPage(SearchWord, MaxPrice, MinPrice));
            }

        }

    }


    
}
