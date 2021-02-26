using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Views
{
    public sealed partial class InvitesPage : Page
    {
        public InvitesViewModel ViewModel => DataContext as InvitesViewModel;

        public InvitesPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<InvitesViewModel>();
        }
    }
}
