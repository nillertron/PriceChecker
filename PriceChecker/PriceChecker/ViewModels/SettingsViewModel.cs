using PriceChecker.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PriceChecker.ViewModels
{
    class SettingsViewModel
    {
        public ICommand SettingsCommand { get; set; }
        private INavigation navigation;

        public SettingsViewModel(INavigation nav)
        {
            navigation = nav;
            SettingsCommand = new Command(async() => { await navigation.PushAsync(new BackgroundWorkerPage()); });
        }
    }
}
