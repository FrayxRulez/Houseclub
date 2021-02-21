using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(ClubhouseAPIController dataService)
            : base(dataService)
        {
            Items = new ObservableCollection<Channel>();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var response = await DataService.SendAsync(new GetChannels());
            if (response != null)
            {
                Items.Clear();

                foreach (var item in response.Channels)
                {
                    Items.Add(item);
                }
            }
        }

        public ObservableCollection<Channel> Items { get; private set; }
    }
}
