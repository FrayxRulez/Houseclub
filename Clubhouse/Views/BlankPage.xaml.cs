using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.Views
{
    public sealed partial class BlankPage : Page
    {
        public BlankPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();
            Frame.ForwardStack.Clear();
        }
    }
}
