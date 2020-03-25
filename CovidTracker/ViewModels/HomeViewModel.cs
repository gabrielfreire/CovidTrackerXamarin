using CovidTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Private members
        private CovidLocation _totalCases;
        private CovidLocation _ireland;
        private CovidLocation _uk;
        #endregion

        #region Public members

        public CovidLocation TotalCases { get => _totalCases; set => RaiseIfPropertyChanged(ref _totalCases, value); }
        public CovidLocation Ireland { get => _ireland; set => RaiseIfPropertyChanged(ref _ireland, value); }
        
        
        public CovidLocation Uk { get => _uk; set => RaiseIfPropertyChanged(ref _uk, value); }

        #endregion


        #region Default constructor
        public HomeViewModel() { }
        #endregion

        public async Task LoadCovidData()
        {
            try
            {
                SetBusy(true);
                var _covidLocations = await App.CovidService.GetLocationsAsync();
                TotalCases = App.CovidService.GetTotalCases(_covidLocations);
                Ireland = _covidLocations.FirstOrDefault(l => l.Country == "Ireland");
                Uk = _covidLocations.FirstOrDefault(l => l.Country == "United Kingdom");
            }
            catch (Exception ex)
            {
                App.MessageDialogService.Display("Error", ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }


    }
}
