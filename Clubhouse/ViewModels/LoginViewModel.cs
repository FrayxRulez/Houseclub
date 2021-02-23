using Clubhouse.Common;
using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using Clubhouse.Views;
using System.Collections.Generic;
using System.Linq;
using Windows.System.UserProfile;

namespace Clubhouse.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IDataService dataService)
            : base(dataService)
        {
            SelectedCountry = Countries.FirstOrDefault(x => x.Code == GlobalizationPreferences.HomeGeographicRegion);

            if (SelectedCountry != null)
            {
                PhoneNumber = $"+{SelectedCountry.PhoneCode}";
            }

            SendCommand = new RelayCommand(SendExecute);
        }

        private Country _selectedCountry;
        public Country SelectedCountry
        {
            get => _selectedCountry;
            set => Set(ref _selectedCountry, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }

        public IList<Country> Countries { get; } = Country.Countries.OrderBy(x => x.DisplayName).ToList();

        public RelayCommand SendCommand { get; }
        private async void SendExecute()
        {
            var phoneNumber = _phoneNumber?.Replace(" ", string.Empty);
            if (phoneNumber == null)
            {
                return;
            }

            var response = await DataService.SendAsync(new StartPhoneNumberAuth(phoneNumber));
            if (response.Success)
            {
                NavigationService.Navigate(typeof(LoginCompletePage), phoneNumber);
            }
        }
    }
}
