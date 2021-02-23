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
        public UserViewModel(IDataService dataService)
            : base(dataService)
        {
            ToggleFollowCommand = new RelayCommand(ToggleFollowExecute);
            NavigateCommand = new RelayCommand<object>(NavigateExecute);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter is User user)
            {
                User = user;
                IsFollowing = DataService.FollowingIds.Contains(user.Id);

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

        private bool? _isFollowing;
        public bool? IsFollowing
        {
            get => _isFollowing;
            set => Set(ref _isFollowing, value);
        }

        public RelayCommand ToggleFollowCommand { get; }
        private async void ToggleFollowExecute()
        {
            var user = _user;
            if (user == null)
            {
                return;
            }

            IsFollowing = null;

            if (DataService.FollowingIds.Contains(user.Id))
            {
                await DataService.SendAsync(new Unfollow(user.Id));
            }
            else
            {
                await DataService.SendAsync(new Follow(user.Id));
            }

            IsFollowing = DataService.FollowingIds.Contains(user.Id);
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
