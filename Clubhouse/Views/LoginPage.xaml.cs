using Clubhouse.Common;
using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel => DataContext as LoginViewModel;

        public LoginPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<LoginViewModel>();

            Diagnostics.Text = Package.Current.GetVersion();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            PrimaryInput.Focus(FocusState.Keyboard);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Lottie.Play();
        }

        private void Countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems?.Count > 0)
            {
                PrimaryInput.Focus(FocusState.Keyboard);
            }
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
