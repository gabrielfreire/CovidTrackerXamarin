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
        private CovidLocation _brazil;
        private CovidLocation _resultCountry;
        private IEnumerable<CovidLocation> _covidLocations = new List<CovidLocation>();
        private string _searchQuery;
        private bool _searchSuccess = false;
        #endregion

        #region Public members
        public bool SearchSuccess { get => _searchSuccess; set => RaiseIfPropertyChanged(ref _searchSuccess, value); }
        public IEnumerable<CovidLocation> CovidLocations { get => _covidLocations; set => RaiseIfPropertyChanged(ref _covidLocations, value); }
        public string SearchQuery { get => _searchQuery; set => RaiseIfPropertyChanged(ref _searchQuery, value); }
        public CovidLocation TotalCases { get => _totalCases; set => RaiseIfPropertyChanged(ref _totalCases, value); }
        public CovidLocation Ireland { get => _ireland; set => RaiseIfPropertyChanged(ref _ireland, value); }
        
        
        public CovidLocation Uk { get => _uk; set => RaiseIfPropertyChanged(ref _uk, value); }
        public CovidLocation Brazil { get => _brazil; set => RaiseIfPropertyChanged(ref _brazil, value); }
        public CovidLocation ResultCountry { get => _resultCountry; set => RaiseIfPropertyChanged(ref _resultCountry, value); }

        #endregion


        #region Default constructor
        public HomeViewModel() { }
        #endregion

        public async Task LoadCovidData()
        {
            try
            {
                SetBusy(true);
                CovidLocations = await App.CovidService.GetLocationsAsync();
                TotalCases = App.CovidService.GetTotalCases(CovidLocations);
                Ireland = CovidLocations.FirstOrDefault(l => l.Country == "Ireland");
                Uk = CovidLocations.FirstOrDefault(l => l.Country == "United Kingdom");
                Brazil = CovidLocations.FirstOrDefault(l => l.Country == "Brazil");
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

        public void Search()
        {
            try
            {

                SearchSuccess = false;
                SetBusy(true);
                if (string.IsNullOrEmpty(SearchQuery))
                {
                    App.MessageDialogService.Display("Error", "Please provide a search query");
                    return;
                }

                var _resultCountry = new CovidLocation() { 
                    Country = SearchQuery ,
                    Confirmed = 0,
                    Deaths = 0,
                    Recovered = 0
                };
                var _results = CovidLocations.Where(l => l.Country.ToLower() == SearchQuery.ToLower());
                if (_results == null || _results.Count() == 0)
                {
                    App.MessageDialogService.Display("Error", $"Country {SearchQuery} not found");
                    return;
                }

                foreach(var res in _results)
                {
                    _resultCountry.Confirmed += res.Confirmed;
                    _resultCountry.Deaths += res.Deaths;
                    _resultCountry.Recovered += res.Recovered;
                }
                ResultCountry = _resultCountry;
                SearchSuccess = true;
            }
            catch (Exception ex)
            {
                App.MessageDialogService.Display("Error", $"Something weird has happened: {ex.Message}");

            }
            finally
            {
                SetBusy(false);
            }

        }


    }
}
