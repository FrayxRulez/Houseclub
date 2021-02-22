using Clubhouse.Navigation;
using Clubhouse.ViewModels;
using Clubhouse.ViewModels.Delegates;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Clubhouse.Views
{
    public sealed partial class RoomPage : Page, IListViewDelegate
    {
        public RoomViewModel ViewModel => DataContext as RoomViewModel;

        public RoomPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.Resolve<RoomViewModel, IListViewDelegate>(this);
        }

        public T ElementFromItem<T>(object item) where T : class
        {
            var container = List.ContainerFromItem(item) as SelectorItem;
            if (container != null)
            {
                return container.ContentTemplateRoot as T;
            }

            return default;
        }
    }
}
