using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class AcceptSpeakerInvite : ClubhouseAPIRequest<BaseResponse>
    {
        public AcceptSpeakerInvite(string channel, int userID)
            : base(HttpMethod.Post, "accept_speaker_invite")
        {
            requestBody = new Body(channel, userID);
        }

        private class Body
        {
            public string channel { get; set; }
            public int userId { get; set; }

            public Body(string channel, int userId)
            {
                this.channel = channel;
                this.userId = userId;
            }
        }
    }
}
