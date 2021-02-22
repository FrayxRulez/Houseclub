using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.Foundation.Collections;
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

            return string.Format(Strings.Resources.UserInvitedBy, user.TimeCreated.ToShortDateString(), user.InvitedByUserProfile.Name);
        }

        #endregion

        private void Followers_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(FollowListPage), new ValueSet { { "user_id", ViewModel.User.Id }, { "followers", true } });
        }

        private void Following_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(FollowListPage), new ValueSet { { "user_id", ViewModel.User.Id }, { "followers", false } });
        }
    }
}
