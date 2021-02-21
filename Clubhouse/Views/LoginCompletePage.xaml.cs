using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Views
{
    public sealed partial class LoginCompletePage : Page
    {
        public LoginCompleteViewModel ViewModel => DataContext as LoginCompleteViewModel;

        public LoginCompletePage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<LoginCompleteViewModel>();
        }
    }
}
