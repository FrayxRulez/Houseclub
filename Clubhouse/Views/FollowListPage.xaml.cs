using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Views
{
    public sealed partial class FollowListPage : Page
    {
        public FollowListViewModel ViewModel => DataContext as FollowListViewModel;

        public FollowListPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<FollowListViewModel>();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(UserPage), e.ClickedItem);
        }
    }
}
