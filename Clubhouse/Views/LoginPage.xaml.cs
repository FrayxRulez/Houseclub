using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel => DataContext as LoginViewModel;

        public LoginPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<LoginViewModel>();
        }
    }
}
