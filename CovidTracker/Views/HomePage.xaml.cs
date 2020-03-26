using CovidTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private HomeViewModel _viewModel;

        public HomePage()
        {
            BindingContext = _viewModel = new HomeViewModel();
            _ = _viewModel.LoadCovidData();
            InitializeComponent();
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            _ = _viewModel.LoadCovidData();
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.Search();
        }
    }
}