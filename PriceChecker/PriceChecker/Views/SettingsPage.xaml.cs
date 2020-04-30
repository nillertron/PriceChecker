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
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel ViewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SettingsViewModel(this.Navigation);
        }
    }
}