using AgoraWinRT;
using Clubhouse.Common;
using Clubhouse.Logs;
using PubnubApi;
using System;
using System.Text.Json;

namespace Clubhouse.Services
{
    public interface IVoiceService
    {
        void JoinChannel(Models.Channel channel);
    }

    public class VoiceService : SubscribeCallback, IVoiceService
    {
        private readonly AgoraRtc _engine;
        private Pubnub _pubnub;

        private Models.Channel _channel;
        private bool _isSelfSpeaker = false;

        public VoiceService()
        {
            _engine = new AgoraRtc(ClubhouseAPIController.AGORA_KEY);
            //_engine.setDefaultAudioRoutetoSpeakerphone(true);
            _engine.EnableAudioVolumeIndication(500, 3, false);
            _engine.MuteLocalAudioStream(true);
        }

        public void JoinChannel(Models.Channel channel)
        {
            _channel = channel;
            doJoinChannel();
        }

        private void doJoinChannel()
        {
            _engine.SetChannelProfile(_isSelfSpeaker ? CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION : CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING);
            _engine.JoinChannel(_channel.Token, _channel.channel, "", ulong.Parse(ClubhouseSession.userID));
            //uiHandler.postDelayed(pinger, 30000);
            //for (ChannelEventListener l:listeners)
            //	l.onChannelUpdated(channel);

            PNConfiguration pnConf = new PNConfiguration();
            pnConf.SubscribeKey = ClubhouseAPIController.PUBNUB_SUB_KEY;
            pnConf.PublishKey = ClubhouseAPIController.PUBNUB_PUB_KEY;
            //pnConf.setUuid(UUID.randomUUID().toString());
            pnConf.AuthKey = _channel.PubnubToken;
            pnConf.Origin = "clubhouse.pubnub.com";
            pnConf.Uuid = ClubhouseSession.userID;
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
                    switch (action)
                    {
                        case "invite_speaker":
                            break;
                        case "join_channel":
                            break;
                        case "leave_channel":
                            break;
                    }
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
    }
}
