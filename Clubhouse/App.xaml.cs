using Clubhouse.Common;
using Clubhouse.Navigation;
using Clubhouse.Navigation.Services;
using Clubhouse.Services;
using Clubhouse.Views;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Resources;

namespace Clubhouse
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : BootStrapper
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            CustomXamlResourceLoader.Current = new XamlResourceLoader();
            base.OnWindowCreated(args);
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            var navService = WindowContext.GetForCurrentView().NavigationServices.GetByFrameId($"0");
            if (navService != null)
            {
                ClubhouseSession.load();

                if (ClubhouseSession.isLoggedIn())
                {
                    navService.Navigate(typeof(MainPage));
                }
                else
                {
                    navService.Navigate(typeof(LoginPage));
                }
            }

            return Task.CompletedTask;
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var navigationFrame = new Frame();
            var navigationService = NavigationServiceFactory(BackButton.Ignore, ExistingContent.Include, navigationFrame, 0, $"0", true) as NavigationService;

            return navigationFrame;
        }

        public override UIElement CreateRootElement(INavigationService navigationService)
        {
            throw new NotImplementedException();
        }
    }
}
