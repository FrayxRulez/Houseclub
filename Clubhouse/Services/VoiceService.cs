using AgoraWinRT;
using Clubhouse.Common;
using Clubhouse.Logs;
using Clubhouse.Navigation;
using Clubhouse.Services.Methods;
using PubnubApi;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Clubhouse.Services
{
    public interface IVoiceService
    {
        Models.Channel Channel { get; }

        bool IsSpeaker { get; }
        bool IsHandRaised { get; }

        bool IsMuted { get; set; }

        void AddListener(IVoiceDelegate listener);
        void RemoveListener(IVoiceDelegate listener);

        void JoinChannel(Models.Channel channel);
        void LeaveChannel();

        void RejoinChannel();
    }

    public interface IVoiceDelegate
    {
        void ChannelUpdated(Models.Channel channel);

        void UserJoined(Models.Channel channel, Models.ChannelUser user);
        void UserLeft(Models.Channel channel, ulong userId);
        void UserMuteChanged(Models.Channel channel, ulong userId, bool muted);
        void SpeakingUsersChanged(Models.Channel channel, ulong[] speakingUsers);
        void SpeakingInviteReceived(Models.Channel channel, ulong userId, string userName);

        IDispatcherContext Dispatcher { get; }
    }

    public class VoiceService : SubscribeCallback, IVoiceService
    {
        private readonly IDataService _dataService;
        private readonly AgoraRtc _engine;

        private readonly Timer _pinger;

        private Pubnub _pubnub;

        private Models.Channel _channel;

        private bool _isSelfSpeaker = false;
        private bool _isSelfModerator = false;

        private bool _isMuted = true;

        protected readonly List<IVoiceDelegate> _listeners = new List<IVoiceDelegate>();

        public VoiceService(IDataService dataService)
        {
            _dataService = dataService;

            _engine = new AgoraRtc(ClubhouseAPIController.AGORA_KEY);
            _engine.RegisterRtcEngineEventHandler(new VoiceServiceRtcHandler(this));
            //_engine.setDefaultAudioRoutetoSpeakerphone(true);
            _engine.EnableAudioVolumeIndication(500, 3, false);
            _engine.MuteLocalAudioStream(true);

            _pinger = new Timer(OnPing);
        }

        private void OnPing(object state)
        {
            var channel = _channel?.channel;
            if (channel == null)
            {
                _pinger.Change(Timeout.Infinite, Timeout.Infinite);
                return;
            }

            _dataService.Send(new ActivePing(channel));
        }

        public Models.Channel Channel => _channel;

        public bool IsSpeaker => _isSelfSpeaker;
        public bool IsHandRaised => false;

        public bool IsMuted
        {
            get => _isMuted;
            set
            {
                _isMuted = value;
                _engine.MuteLocalAudioStream(value);
            }
        }

        public void AddListener(IVoiceDelegate listener)
        {
            if (_listeners.Contains(listener))
            {
                return;
            }

            _listeners.Add(listener);
        }

        public void RemoveListener(IVoiceDelegate listener)
        {
            _listeners.Remove(listener);
        }

        public void JoinChannel(Models.Channel channel)
        {
            UpdateChannel(channel);
            DoJoinChannel();
        }

        public void LeaveChannel()
        {
            var channel = _channel?.channel;
            if (channel == null)
            {
                return;
            }

            _channel = null;

            _engine.LeaveChannel();
            _dataService.Send(new LeaveChannel(channel));
            //new LeaveChannel(channel.channel)
            //        .exec();
            //stopSelf();
            //uiHandler.removeCallbacks(pinger);
            _pinger.Change(Timeout.Infinite, Timeout.Infinite);
            _pubnub.UnsubscribeAll<string>();
            _pubnub.Destroy();
        }

        public async void RejoinChannel()
        {
            var channel = _channel?.channel;
            if (channel == null)
            {
                return;
            }

            _engine.LeaveChannel();
            _pubnub.UnsubscribeAll<string>();

            var leave = await _dataService.SendAsync(new LeaveChannel(channel));
            if (leave.Success)
            {
                var response = await _dataService.SendAsync(new JoinChannel(channel));
                if (response != null)
                {
                    UpdateChannel(response);
                    DoJoinChannel();
                }
            }
        }

        private void UpdateChannel(Models.Channel channel)
        {
            _channel = channel;
            _isSelfModerator = false;
            _isSelfSpeaker = false;

            foreach (var user in channel.Users)
            {
                if (user.Id == ClubhouseSession.userID)
                {
                    _isSelfModerator = user.IsModerator;
                    _isSelfSpeaker = user.IsSpeaker;
                    break;
                }
            }
        }

        private void DoJoinChannel()
        {
            _engine.SetChannelProfile(_isSelfSpeaker ? CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION : CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING);
            _engine.JoinChannel(_channel.Token, _channel.channel, "", ClubhouseSession.userID);
            _pinger.Change(30000, 30000);

            foreach (var l in _listeners)
            {
                l.Dispatcher.Dispatch(() => l.ChannelUpdated(_channel));
            }

            PNConfiguration pnConf = new PNConfiguration();
            pnConf.SubscribeKey = ClubhouseAPIController.PUBNUB_SUB_KEY;
            pnConf.PublishKey = ClubhouseAPIController.PUBNUB_PUB_KEY;
            pnConf.AuthKey = _channel.PubnubToken;
            pnConf.Origin = "clubhouse.pubnub.com";
            pnConf.Uuid = $"{ClubhouseSession.userID}";
            pnConf.SetPresenceTimeoutWithCustomInterval(_channel.PubnubHeartbeatValue, _channel.PubnubHeartbeatInterval);

            _pubnub = new Pubnub(pnConf);
            _pubnub.AddListener(this);

            _pubnub.Subscribe<string>().Channels(new[] {
                "users." + ClubhouseSession.userID,
                "channel_user." + _channel.channel + "." + ClubhouseSession.userID,
                //				"channel_speakers."+channel.channel,
                "channel_all." + _channel.channel
            }).Execute();
        }

        public override void Status(Pubnub pubnub, PNStatus status)
        {
            Logger.Info("status() called with: pubnub = [" + pubnub + "], pnStatus = [" + status + "]");
        }

        public override void Message<T>(Pubnub pubnub, PNMessageResult<T> message)
        {
            Logger.Info("message() called with: pubnub = [" + pubnub + "], pnMessageResult = [" + message + "]");

            if (message.Message is string json)
            {
                var msg = JsonDocument.Parse(json);
                if (msg.RootElement.TryGetNamedString("action", out string action))
                {
                    var channel = msg.RootElement.GetNamedString("channel");
                    if (channel != _channel.channel)
                    {
                        return;
                    }

                    switch (action)
                    {
                        case "invite_speaker":
                            OnInviteSpeaker(msg.RootElement);
                            break;
                        case "uninvite_speaker":
                            OnUninviteSpeaker(msg.RootElement);
                            break;
                        case "make_moderator":
                            break;
                        case "join_channel":
                            OnJoinChannel(msg.RootElement);
                            break;
                        case "leave_channel":
                            OnLeaveChannel(msg.RootElement);
                            break;
                        case "change_handraise_settings":
                            break;
                    }
                }
            }
        }

        private void OnInviteSpeaker(JsonElement element)
        {
            if (element.TryGetNamedUInt64("from_user_id", out ulong userId)
                && element.TryGetNamedString("from_name", out string userName))
            {
                foreach (var l in _listeners)
                {
                    l.Dispatcher.Dispatch(() => l.SpeakingInviteReceived(_channel, userId, userName));
                }
            }
        }

        private void OnUninviteSpeaker(JsonElement element)
        {
            RejoinChannel();
        }

        private void OnJoinChannel(JsonElement element)
        {
            if (element.TryGetProperty("user_profile", out JsonElement child))
            {
                var user = JsonSerializer.Deserialize<Models.ChannelUser>(child.GetRawText());
                if (user == null)
                {
                    return;
                }

                _channel.Users.Add(user);

                foreach (var l in _listeners)
                {
                    l.Dispatcher.Dispatch(() => l.UserJoined(_channel, user));
                }
            }
        }

        private void OnLeaveChannel(JsonElement element)
        {
            if (element.TryGetNamedUInt64("user_id", out ulong id))
            {
                foreach (var user in _channel.Users)
                {
                    if (user.Id == id)
                    {
                        _channel.Users.Remove(user);
                        break;
                    }
                }

                foreach (var l in _listeners)
                {
                    l.Dispatcher.Dispatch(() => l.UserLeft(_channel, id));
                }
            }
        }

        public override void Presence(Pubnub pubnub, PNPresenceEventResult presence)
        {
            Logger.Info("presence() called with: pubnub = [" + pubnub + "], presence = [" + presence + "]");
        }

        public override void Signal<T>(Pubnub pubnub, PNSignalResult<T> signal)
        {
            Logger.Info("signal() called with: pubnub = [" + pubnub + "], pnSignalResult = [" + signal + "]");
        }

        public override void MessageAction(Pubnub pubnub, PNMessageActionEventResult messageAction)
        {
            Logger.Info("messageAction() called with: pubnub = [" + pubnub + "], messageAction = [" + messageAction + "]");
        }

        public override void File(Pubnub pubnub, PNFileEventResult fileEvent)
        {
            Logger.Info("file() called with: pubnub = [" + pubnub + "], fileEvent = [" + fileEvent + "]");
        }

        public override void ObjectEvent(Pubnub pubnub, PNObjectEventResult objectEvent)
        {
            Logger.Info("objectEvent() called with: pubnub = [" + pubnub + "], objectEvent = [" + objectEvent + "]");
        }

        private class VoiceServiceRtcHandler : AgoraRtcEventHandler
        {
            private readonly VoiceService _service;

            private HashSet<ulong> _lastUids;

            public VoiceServiceRtcHandler(VoiceService service)
            {
                _service = service;
            }

            public void OnJoinChannelSuccess(string channel, ulong uid, uint elapsed)
            {
                Logger.Info("onJoinChannelSuccess() called with: channel = [" + channel + "], uid = [" + uid + "], elapsed = [" + elapsed + "]");
            }

            public void OnError(long code, string msg)
            {
                Logger.Info("onError() called with: msg = [" + msg + "]");
            }

            public void OnAudioVolumeIndication(AudioVolumeInfo[] speakers, byte totalVolume)
            {
                var uids = speakers.Select(x => x.uid == 0 ? ClubhouseSession.userID : x.uid).ToHashSet();

                if (_lastUids != null && _lastUids.SetEquals(uids))
                {
                    return;
                }

                _lastUids = uids;

                var array = new ulong[uids.Count];
                uids.CopyTo(array);

                foreach (var l in _service._listeners)
                {
                    l.Dispatcher.Dispatch(() => l.SpeakingUsersChanged(_service.Channel, array));
                }
            }

            public void OnRemoteAudioStateChanged(ulong uid, REMOTE_AUDIO_STATE state, REMOTE_AUDIO_STATE_REASON reason, uint elapsed)
            {
                if (reason != REMOTE_AUDIO_STATE_REASON.REMOTE_AUDIO_REASON_LOCAL_MUTED &&
                    reason != REMOTE_AUDIO_STATE_REASON.REMOTE_AUDIO_REASON_LOCAL_UNMUTED &&
                    reason != REMOTE_AUDIO_STATE_REASON.REMOTE_AUDIO_REASON_REMOTE_MUTED &&
                    reason != REMOTE_AUDIO_STATE_REASON.REMOTE_AUDIO_REASON_REMOTE_UNMUTED)
                {
                    return;
                }

                var muted = reason == REMOTE_AUDIO_STATE_REASON.REMOTE_AUDIO_REASON_LOCAL_MUTED
                    || reason == REMOTE_AUDIO_STATE_REASON.REMOTE_AUDIO_REASON_REMOTE_MUTED;

                foreach (var user in _service.Channel.Users)
                {
                    if (user.Id == uid)
                    {
                        user.isMuted = muted;
                        break;
                    }
                }
                foreach (var l in _service._listeners)
                {
                    l.Dispatcher.Dispatch(() => l.UserMuteChanged(_service.Channel, uid, muted));
                }
            }

            #region Unused

            public void OnConnectionStateChanged(CONNECTION_STATE_TYPE type, CONNECTION_CHANGED_REASON_TYPE reason)
            {
                if (type == CONNECTION_STATE_TYPE.CONNECTION_STATE_CONNECTED)
                {
                    _service._engine.EnableAudioVolumeIndication(500, 3, false);
                }
            }

            public void OnRejoinChannelSuccess(string channel, ulong uid, uint elapsed)
            {
            }

            public void OnLeaveChannel(RtcStats stats)
            {
            }

            public void OnClientRoleChanged(CLIENT_ROLE_TYPE oldRole, CLIENT_ROLE_TYPE newRole)
            {
            }

            public void OnUserJoined(ulong uid, uint elapsed)
            {
            }

            public void OnUserOffline(ulong uid, USER_OFFLINE_REASON_TYPE reason)
            {
            }

            public void OnNetworkTypeChanged(NETWORK_TYPE type)
            {
            }

            public void OnConnectionLost()
            {
            }

            public void OnTokenPrivilegeWillExpire(string token)
            {
            }

            public void OnRequestToken()
            {
            }

            public void OnLocalAudioStateChanged(LOCAL_AUDIO_STREAM_STATE state, LOCAL_AUDIO_STREAM_ERROR error)
            {
            }

            public void OnLocalVideoStateChanged(LOCAL_VIDEO_STREAM_STATE state, LOCAL_VIDEO_STREAM_ERROR error)
            {
            }

            public void OnFirstLocalAudioFramePublished(uint elapsed)
            {
            }

            public void OnFirstLocalVideoFramePublished(uint elapsed)
            {
            }

            public void OnFirstLocalVideoFrame(uint width, uint height, uint elapsed)
            {
            }

            public void OnAudioPublishStateChanged(string channel, STREAM_PUBLISH_STATE oldState, STREAM_PUBLISH_STATE newState, uint elapsed)
            {
            }

            public void OnVideoPublishStateChanged(string channel, STREAM_PUBLISH_STATE oldState, STREAM_PUBLISH_STATE newState, uint elapsed)
            {
            }

            public void OnRemoteVideoStateChanged(ulong uid, REMOTE_VIDEO_STATE state, REMOTE_VIDEO_STATE_REASON reason, uint elapsed)
            {
            }

            public void OnFirstRemoteVideoFrame(ulong uid, uint width, uint height, uint elapsed)
            {
            }

            public void OnAudioSubscribeStateChanged(string channel, ulong uid, STREAM_SUBSCRIBE_STATE oldState, STREAM_SUBSCRIBE_STATE newState, uint elapsed)
            {
            }

            public void OnVideoSubscribeStateChanged(string channel, ulong uid, STREAM_SUBSCRIBE_STATE oldState, STREAM_SUBSCRIBE_STATE newState, uint elapsed)
            {
            }

            public void OnRtcStats(RtcStats stats)
            {
            }

            public void OnNetworkQuality(ulong uid, QUALITY_TYPE txQuality, QUALITY_TYPE rxQuality)
            {
            }

            public void OnLocalAudioStats(LocalAudioStats stats)
            {
            }

            public void OnLocalVideoStats(LocalVideoStats stats)
            {
            }

            public void OnRemoteAudioStats(RemoteAudioStats stats)
            {
            }

            public void OnRemoteVideoStats(RemoteVideoStats stats)
            {
            }

            public void OnAudioMixingStateChanged(AUDIO_MIXING_STATE_TYPE state, AUDIO_MIXING_ERROR_TYPE error)
            {
            }

            public void OnRemoteAudioMixingBegin()
            {
            }

            public void OnRemoteAudioMixingEnd()
            {
            }

            public void OnAudioEffectFinished(ulong soundId)
            {
            }

            public void OnRtmpStreamingStateChanged(string url, RTMP_STREAM_PUBLISH_STATE state, RTMP_STREAM_PUBLISH_ERROR error)
            {
            }

            public void OnRtmpStreamingEvent(string url, RTMP_STREAMING_EVENT code)
            {
            }

            public void OnTranscodingUpdated()
            {
            }

            public void OnChannelMediaRelayStateChanged(CHANNEL_MEDIA_RELAY_STATE state, CHANNEL_MEDIA_RELAY_ERROR error)
            {
            }

            public void OnChannelMediaRelayEvent(CHANNEL_MEDIA_RELAY_EVENT code)
            {
            }

            public void OnActiveSpeaker(ulong uid)
            {
            }

            public void OnLocalPublishFallbackToAudioOnly(bool isFallbackOrRecover)
            {
            }

            public void OnRemoteSubscribeFallbackToAudioOnly(ulong uid, bool isFallbackOrRecover)
            {
            }

            public void OnLastmileQuality(QUALITY_TYPE quality)
            {
            }

            public void OnLastmileProbeResult(LastmileProbeResult result)
            {
            }

            public void OnStreamInjectedStatus(string url, ulong uid, INJECT_STREAM_STATUS status)
            {
            }

            public void OnStreamMessage(ulong uid, long streamId, string data)
            {
            }

            public void OnStreamMessageError(ulong uid, long streamId, uint error, ushort missed, ushort cached)
            {
            }

            public void OnWarning(long code, string msg)
            {
            }

            public void OnApiCallExecuted(long code, string api, string result)
            {
            }

            public void OnAudioDeviceStateChanged(string id, MEDIA_DEVICE_TYPE type, MEDIA_DEVICE_STATE_TYPE state)
            {
            }

            public void OnAudioDeviceVolumeChanged(MEDIA_DEVICE_TYPE type, byte volume, bool muted)
            {
            }

            public void OnVideoDeviceStateChanged(string id, MEDIA_DEVICE_TYPE type, MEDIA_DEVICE_STATE_TYPE state)
            {
            }

            #endregion

        }
    }
}
