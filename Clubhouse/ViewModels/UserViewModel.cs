using Clubhouse.Common;
using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using Clubhouse.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public UserViewModel(ClubhouseAPIController dataService)
            : base(dataService)
        {
            NavigateCommand = new RelayCommand<object>(NavigateExecute);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter is User user)
            {
                User = user;

                var response = await DataService.SendAsync(new GetProfile(user.Id));
                if (response != null)
                {
                    FullUser = response.UserProfile;
                }
            }
        }

        private User _user;
        public User User
        {
            get => _user;
            set => Set(ref _user, value);
        }

        private FullUser _fullUser;
        public FullUser FullUser
        {
            get => _fullUser;
            set => Set(ref _fullUser, value);
        }

        public RelayCommand<object> NavigateCommand { get; }
        private void NavigateExecute(object parameter)
        {
            if (parameter is User)
            {
                NavigationService.Navigate(typeof(UserPage), parameter);
            }
        }
    }
}
