using PriceChecker.Models;
using PriceChecker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultPage : ContentPage
    {
        SearchResultViewModel ViewModel;
        public SearchResultPage(string searchWord, double maxPrice, double minPrice)
        {
            InitializeComponent();
            ViewModel = new SearchResultViewModel(searchWord, maxPrice, minPrice);
            BindingContext = ViewModel;
        }
        public SearchResultPage(List<StoredSearch> liste)
        {
            InitializeComponent();
            ViewModel = new SearchResultViewModel(liste);
            BindingContext = ViewModel;


        }
        public SearchResultPage(bool GetCache)
        {
            InitializeComponent();
            ViewModel = new SearchResultViewModel(GetCache);
            BindingContext = ViewModel;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await ViewModel.OpenBrowser(e.Item);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error in URL", "Try another link, this doesn't work", "Ok");
            }
        }
    }
}