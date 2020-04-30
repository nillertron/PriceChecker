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
    public partial class SubscribedPage : ContentPage
    {
        SubscribedViewModel ViewModel;
        public SubscribedPage()
        {
            InitializeComponent();
            BindingContext = ViewModel =  new SubscribedViewModel(this.Navigation);
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new StoredSearchDetailPage(e.Item));
        }
    }
}