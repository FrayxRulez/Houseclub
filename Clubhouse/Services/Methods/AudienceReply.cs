using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class AudienceReply : ClubhouseAPIRequest<BaseResponse>
    {
        public AudienceReply(string channel, bool raise)
            : base(HttpMethod.Post, "audience_reply")
        {
            Content = new Body(channel, raise, !raise);
        }

        private class Body
        {
            [JsonPropertyName("channel")]
            public string Channel { get; set; }

            [JsonPropertyName("raise_hands")]
            public bool RaiseHands { get; set; }

            [JsonPropertyName("unraise_hands")]
            public bool UnraiseHands { get; set; }

            public Body(string channel, bool raiseHands, bool unraiseHands)
            {
                Channel = channel;
                RaiseHands = raiseHands;
                UnraiseHands = unraiseHands;
            }
        }
    }
}
