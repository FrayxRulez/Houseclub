using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class ActivePing : ClubhouseAPIRequest<BaseResponse>
    {
        public ActivePing(string channel)
            : base(HttpMethod.Post, "active_ping")
        {
            requestBody = new Body(channel);
        }

        private class Body
        {
            public string channel { get; set; }

            public Body(string channel)
            {
                this.channel = channel;
            }
        }
    }
}
