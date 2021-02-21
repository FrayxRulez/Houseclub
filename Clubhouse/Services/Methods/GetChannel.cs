using Clubhouse.Models;
using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class GetChannel : ClubhouseAPIRequest<Channel>
    {
        public GetChannel(string name)
            : base(HttpMethod.Post, "get_channel")
        {
            requestBody = new Body(name);
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
