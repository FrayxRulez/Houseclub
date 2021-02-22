using Clubhouse.Common;
using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Clubhouse.Views
{
    public sealed partial class LoginCompletePage : Page
    {
        public LoginCompleteViewModel ViewModel => DataContext as LoginCompleteViewModel;

        public LoginCompletePage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<LoginCompleteViewModel>();

            Diagnostics.Text = Package.Current.GetVersion();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            PrimaryInput.Focus(FocusState.Keyboard);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Lottie.Play();
        }

        private void PhoneNumber_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ViewModel.SendCommand.Execute(null);
                e.Handled = true;
            }
        }
    }
}
