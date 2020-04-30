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
    public partial class SearchPage : ContentPage
    {
        SearchViewModel ViewModel;
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SearchViewModel(this.Navigation);
        }
    }
}