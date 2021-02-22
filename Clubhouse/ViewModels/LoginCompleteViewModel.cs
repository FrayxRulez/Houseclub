using Clubhouse.Common;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.ViewModels
{
    public class LoginCompleteViewModel : ViewModelBase
    {
        public LoginCompleteViewModel(ClubhouseAPIController dataService)
            : base(dataService)
        {
            SendCommand = new RelayCommand(SendExecute);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter is string phoneNumber)
            {
                _phoneNumber = phoneNumber;
            }

            return Task.CompletedTask;
        }

        private string _phoneNumber;

        private string _verificationCode;
        public string VerificationCode
        {
            get => _verificationCode;
            set => Set(ref _verificationCode, value);
        }

        public RelayCommand SendCommand { get; }
        private async void SendExecute()
        {
            var response = await DataService.SendAsync(new CompletePhoneNumberAuth(_phoneNumber, _verificationCode));
            if (response != null)
            {
                ClubhouseSession.userToken = response.AuthToken;
                ClubhouseSession.userID = response.UserProfile.Id;
                ClubhouseSession.isWaitlisted = response.IsWaitlisted;
                ClubhouseSession.write();

                NavigationService.Navigate(typeof(MainPage));
            }
        }
    }
}
