using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Clubhouse.Views;
using Windows.UI.Xaml.Controls;

namespace Clubhouse
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel => DataContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<MainViewModel>();
        }

        private void LayoutRoot_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (e.NewSize.Width == e.PreviousSize.Width)
            {
                return;
            }

            if (e.NewSize.Width <= 500 + Split.OpenPaneLength)
            {
                Split.DisplayMode = SplitViewDisplayMode.Overlay;
                Split.IsPaneOpen = false;
            }
            else
            {
                Split.IsPaneOpen = true;
                Split.DisplayMode = SplitViewDisplayMode.Inline;
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Channel channel)
            {
                ViewModel.JoinChannel(channel);
            }
            else if (e.ClickedItem is OnlineUser onlineUser)
            {
                ViewModel.NavigationService.Popup.Navigate(typeof(UserPage), onlineUser);
            }
        }

        private void Notifications_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.NavigationService.Popup.Navigate(typeof(NotificationsPage));
        }

        private void Invites_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.NavigationService.Popup.Navigate(typeof(InvitesPage));
        }

        private void Events_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(EventsPage));
        }
    }
}
