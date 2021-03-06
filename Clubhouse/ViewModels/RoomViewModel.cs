﻿using Clubhouse.Common;
using Clubhouse.Controls.Cells;
using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using Clubhouse.ViewModels.Delegates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.ViewModels
{
    public class RoomViewModel : ViewModelBase, IVoiceDelegate, IDelegable<IListViewDelegate>
    {
        private readonly IVoiceService _voiceService;

        private readonly HashSet<ulong> _speakingUsers = new HashSet<ulong>();
        private readonly HashSet<ulong> _mutedUsers = new HashSet<ulong>();

        private readonly RoomUsersCollection _speakers;
        private readonly RoomUsersCollection _followedBySpeakers;
        private readonly RoomUsersCollection _otherUsers;

        public IListViewDelegate Delegate { get; set; }

        public RoomViewModel(IDataService dataService, IVoiceService voiceService)
            : base(dataService)
        {
            _voiceService = voiceService;

            _speakers = new RoomUsersCollection(null);
            _followedBySpeakers = new RoomUsersCollection(Strings.Resources.RoomFollowedByTheSpeakers);
            _otherUsers = new RoomUsersCollection(Strings.Resources.RoomOthersInTheRoom);

            ChannelUpdated(_voiceService.Channel);

            Users = new List<RoomUsersCollection> { _speakers, _followedBySpeakers, _otherUsers };

            RaiseCommand = new RelayCommand(RaiseExecute);
            SpeakCommand = new RelayCommand(SpeakExecute);
            LeaveCommand = new RelayCommand(LeaveExecute);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _voiceService.AddListener(this);
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            _voiceService.RemoveListener(this);
            return base.OnNavigatedFromAsync(pageState, suspending);
        }

        private Channel _channel;
        public Channel Channel
        {
            get => _channel;
            set => Set(ref _channel, value);
        }

        private bool _canRaise;
        public bool CanRaise
        {
            get => _canRaise;
            set => Set(ref _canRaise, value);
        }

        private bool _canSpeak;
        public bool CanSpeak
        {
            get => _canSpeak;
            set => Set(ref _canSpeak, value);
        }

        public List<RoomUsersCollection> Users { get; private set; }

        public RelayCommand RaiseCommand { get; }
        private async void RaiseExecute()
        {
            var channel = _channel;
            if (channel == null)
            {
                return;
            }

            var response = await DataService.SendAsync(new AudienceReply(channel.channel, true));
        }

        public RelayCommand SpeakCommand { get; }
        private void SpeakExecute()
        {
            _voiceService.IsMuted = !_voiceService.IsMuted;
        }

        public RelayCommand LeaveCommand { get; }
        private void LeaveExecute()
        {
            _voiceService.LeaveChannel();
            NavigationService.GoBack();
        }

        public void ChannelUpdated(Channel channel)
        {
            Channel = channel;

            _speakers.Clear();
            _followedBySpeakers.Clear();
            _otherUsers.Clear();

            foreach (var user in channel.Users)
            {
                if (user.isMuted && !_mutedUsers.Contains(user.Id))
                {
                    _mutedUsers.Add(user.Id);
                }

                if (user.IsSpeaker)
                {
                    _speakers.Add(user);
                }
                else if (user.IsFollowedBySpeaker)
                {
                    _followedBySpeakers.Add(user);
                }
                else
                {
                    _otherUsers.Add(user);
                }
            }

            CanRaise = channel.IsHandraiseEnabled;
            CanSpeak = _voiceService.IsSpeaker;
        }

        public void UserJoined(Channel channel, ChannelUser user)
        {
            if (user.IsSpeaker)
            {
                _speakers.Add(user);
            }
            else if (user.IsFollowedBySpeaker)
            {
                _followedBySpeakers.Add(user);
            }
            else
            {
                _otherUsers.Add(user);
            }
        }

        public void UserLeft(Channel channel, ulong userId)
        {
            if (_channel.Id != channel.Id)
            {
                return;
            }

            foreach (var user in _speakers)
            {
                if (user.Id == userId)
                {
                    _speakers.Remove(user);
                    return;
                }
            }

            foreach (var user in _followedBySpeakers)
            {
                if (user.Id == userId)
                {
                    _followedBySpeakers.Remove(user);
                    return;
                }
            }

            foreach (var user in _otherUsers)
            {
                if (user.Id == userId)
                {
                    _otherUsers.Remove(user);
                    return;
                }
            }
        }

        public void UserMuteChanged(Channel channel, ulong userId, bool muted)
        {
            if (muted)
            {
                if (_mutedUsers.Contains(userId))
                {
                    _mutedUsers.Add(userId);
                }
            }
            else
            {
                _mutedUsers.Remove(userId);
            }

            foreach (var user in _speakers)
            {
                if (user.Id == userId)
                {
                    user.isMuted = muted;

                    var content = Delegate?.ElementFromItem<RoomUserCell>(user);
                    if (content != null)
                    {
                        content.IsMuted = muted;
                    }
                }
            }
        }

        public void SpeakingUsersChanged(Channel channel, ulong[] speakingUsers)
        {
            _speakingUsers.Clear();
            _speakingUsers.UnionWith(speakingUsers);

            foreach (var user in _speakers)
            {
                var content = Delegate?.ElementFromItem<RoomUserCell>(user);
                if (content != null)
                {
                    content.IsSpeaking = _speakingUsers.Contains(user.Id);
                }
            }
        }

        public async void SpeakingInviteReceived(Channel channel, ulong userId, string userName)
        {
            var dialog = new ContentDialog();
            dialog.Title = Strings.Resources.RoomJoinAsSpeakerTitle;
            dialog.Content = string.Format(Strings.Resources.RoomJoinAsSpeakerMessage, userName);
            dialog.PrimaryButtonText = Strings.Resources.Join;
            dialog.SecondaryButtonText = Strings.Resources.Cancel;

            var confirm = await dialog.ShowAsync();
            if (confirm != ContentDialogResult.Primary)
            {
                return;
            }

            var response = await DataService.SendAsync(new AcceptSpeakerInvite(channel.channel, userId));
            if (response.Success)
            {
                _voiceService.RejoinChannel();
            }
        }
    }
}
