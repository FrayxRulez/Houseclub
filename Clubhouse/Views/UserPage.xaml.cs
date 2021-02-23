using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Views
{
    public sealed partial class UserPage : Page
    {
        public UserViewModel ViewModel => DataContext as UserViewModel;

        public UserPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<UserViewModel>();
        }

        #region Binding

        private Visibility ConvertSelf(User user, bool negate)
        {
            if (negate)
            {
                return user.Id == ClubhouseSession.userID ? Visibility.Collapsed : Visibility.Visible;
            }

            return user.Id == ClubhouseSession.userID ? Visibility.Visible : Visibility.Collapsed;
        }

        private string ConvertUsername(User user)
        {
            return $"@{user.Username}";
        }

        private string ConvertInvitedBy(FullUser user)
        {
            if (user == null)
            {
                return string.Empty;
            }
            else if (user.InvitedByUserProfile == null)
            {
                return string.Format(Strings.Resources.UserJoined, user.TimeCreated.ToShortDateString());
            }

            return string.Format(Strings.Resources.UserInvitedBy, user.TimeCreated.ToShortDateString(), user.InvitedByUserProfile.Name);
        }

        #endregion

        private void Followers_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(FollowListPage), new Dictionary<string, object> { { "user_id", ViewModel.User.Id }, { "type", FollowListType.Followers } });
        }

        private void Following_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(FollowListPage), new Dictionary<string, object> { { "user_id", ViewModel.User.Id }, { "type", FollowListType.Following } });
        }

        private void MutualFollows_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(FollowListPage), new Dictionary<string, object> { { "user_id", ViewModel.User.Id }, { "type", FollowListType.MutualFollows } });
        }

        private async void Settings_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog();
            dialog.Title = "Wanna logout?";
            dialog.Content = "You super shur?";
            dialog.PrimaryButtonText = "OK";
            dialog.SecondaryButtonText = "Cancel";

            var confirm = await dialog.ShowAsync();
            if (confirm == ContentDialogResult.Primary)
            {
                ClubhouseSession.clear();
                ViewModel.NavigationService.Navigate(typeof(BlankPage));
                ViewModel.NavigationService.Master.Navigate(typeof(LoginPage));
            }
        }
    }
}
