using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Views
{
    public sealed partial class EventsPage : Page
    {
        public EventsViewModel ViewModel => DataContext as EventsViewModel;

        public EventsPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<EventsViewModel>();
        }
    }
}
