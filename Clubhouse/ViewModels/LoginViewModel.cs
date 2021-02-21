using Clubhouse.Common;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using Clubhouse.Views;

namespace Clubhouse.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(ClubhouseAPIController dataService)
            : base(dataService)
        {
            SendCommand = new RelayCommand(SendExecute);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }

        public RelayCommand SendCommand { get; }
        private async void SendExecute()
        {
            var response = await DataService.SendAsync(new StartPhoneNumberAuth(_phoneNumber));
            if (response != null && response.success)
            {
                NavigationService.Navigate(typeof(LoginCompletePage), _phoneNumber);
            }
        }
    }
}
