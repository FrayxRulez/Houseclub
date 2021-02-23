using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Views
{
    public sealed partial class NotificationsPage : Page
    {
        public NotificationsViewModel ViewModel => DataContext as NotificationsViewModel;

        public NotificationsPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<NotificationsViewModel>();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.NavigateCommand.Execute(e.ClickedItem);
        }
    }
}
