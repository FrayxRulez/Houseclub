using Clubhouse.Navigation.Services;
using Clubhouse.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.Navigation
{
    public abstract class ViewModelBase : BindableBase, INavigable
    {
        private readonly IDataService _dataService;

        public ViewModelBase(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IDataService DataService => _dataService;

        public virtual Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            return Task.CompletedTask;
        }

        public virtual void OnNavigatingFrom(NavigatingEventArgs args)
        {

        }

        public virtual INavigationService NavigationService { get; set; }

        public virtual IDispatcherContext Dispatcher { get; set; }

        public virtual IDictionary<string, object> SessionState { get; set; }
    }
}