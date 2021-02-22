using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IVoiceService _voiceService;

        public MainViewModel(ClubhouseAPIController dataService, IVoiceService voiceService)
            : base(dataService)
        {
            _voiceService = voiceService;

            Channels = new ObservableCollection<Channel>();
            Events = new ObservableCollection<Event>();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var response = await DataService.SendAsync(new GetChannels());
            if (response != null)
            {
                Channels.Clear();
                Events.Clear();

                foreach (var item in response.Channels)
                {
                    Channels.Add(item);
                }

                foreach (var item in response.Events)
                {
                    Events.Add(item);
                }
            }
        }

        public ObservableCollection<Channel> Channels { get; private set; }
        public ObservableCollection<Event> Events { get; private set; }

        public async void JoinChannel(Channel channel)
        {
            var response = await DataService.SendAsync(new JoinChannel(channel.channel));
            if (response != null)
            {
                _voiceService.JoinChannel(response);
            }
        }
    }
}
