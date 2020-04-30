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
    public partial class StoredSearchDetailPage : ContentPage
    {
        StoredSearchDetailViewModel ViewModel;
        public StoredSearchDetailPage(Object Search)
        {
            InitializeComponent();
            BindingContext = ViewModel = new StoredSearchDetailViewModel(Search, this.Navigation);
        }
    }
}