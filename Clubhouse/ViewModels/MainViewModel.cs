﻿using Clubhouse.Common;
using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using Clubhouse.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IVoiceService _voiceService;

        public MainViewModel(IDataService dataService, IVoiceService voiceService)
            : base(dataService)
        {
            _voiceService = voiceService;

            Channels = new ObservableCollection<Channel>();
            Events = new ObservableCollection<Event>();
            OnlineUsers = new ObservableCollection<OnlineUser>();

            LogoutCommand = new RelayCommand(LogoutExecute);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            await LoadSelfAsync();

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

            await LoadOnlineFriendsAsync();
        }

        private async Task LoadSelfAsync()
        {
            var response = await DataService.SendAsync(new GetProfile(ClubhouseSession.userID));
            if (response != null)
            {
                Self = response.UserProfile;
            }
        }

        private async Task LoadOnlineFriendsAsync()
        {
            var response = await DataService.SendAsync(new GetOnlineFriends());
            if (response != null)
            {
                OnlineUsers.Clear();

                foreach (var item in response.Users)
                {
                    OnlineUsers.Add(item);
                }
            }
        }

        public ObservableCollection<Channel> Channels { get; private set; }

        public ObservableCollection<Event> Events { get; private set; }

        public ObservableCollection<OnlineUser> OnlineUsers { get; private set; }

        private User _self;
        public User Self
        {
            get => _self;
            set => Set(ref _self, value);
        }

        public async void JoinChannel(Channel channel)
        {
            var response = await DataService.SendAsync(new JoinChannel(channel.channel));
            if (response != null)
            {
                _voiceService.JoinChannel(response);
                NavigationService.Navigate(typeof(RoomPage));
            }
        }

        public RelayCommand LogoutCommand { get; }
        private void LogoutExecute()
        {
            var self = _self;
            if (self == null)
            {
                return;
            }

            NavigationService.Popup.Navigate(typeof(UserPage), self);
        }
    }
}
