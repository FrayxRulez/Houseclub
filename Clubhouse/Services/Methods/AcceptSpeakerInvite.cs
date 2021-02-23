using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class AcceptSpeakerInvite : ClubhouseAPIRequest<BaseResponse>
    {
        public AcceptSpeakerInvite(string channel, ulong userID)
            : base(HttpMethod.Post, "accept_speaker_invite")
        {
            Content = new Body(channel, userID);
        }

        private class Body
        {
            [JsonPropertyName("channel")]
            public string Channel { get; set; }

            [JsonPropertyName("user_id")]
            public ulong UserId { get; set; }

            public Body(string channel, ulong userId)
            {
                Channel = channel;
                UserId = userId;
            }
        }
    }
}
