using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class AudienceReply : ClubhouseAPIRequest<BaseResponse>
    {
        public AudienceReply(string channel, bool raise)
            : base(HttpMethod.Post, "audience_reply")
        {
            requestBody = new Body(channel, raise, !raise);
        }

        private class Body
        {
            public string channel { get; set; }
            public bool raiseHands { get; set; }
            public bool unraiseHands { get; set; }

            public Body(string channel, bool raiseHands, bool unraiseHands)
            {
                this.channel = channel;
                this.raiseHands = raiseHands;
                this.unraiseHands = unraiseHands;
            }
        }
    }
}
