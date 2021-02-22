using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.ViewModels;
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
            ViewModel.JoinChannel(e.ClickedItem as Channel);
        }
    }
}
