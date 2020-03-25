using CovidTracker.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidTracker.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel _viewModel;

        public AboutPage()
        {
            BindingContext = _viewModel = new AboutViewModel();

            InitializeComponent();
        }
    }
}