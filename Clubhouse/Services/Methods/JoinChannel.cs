using Clubhouse.Models;
using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class JoinChannel : ClubhouseAPIRequest<Channel>
    {
        public JoinChannel(string channelName)
            : base(HttpMethod.Post, "join_channel")
        {
            Content = new Body(channelName, "feed", "eyJpc19leHBsb3JlIjpmYWxzZSwicmFuayI6MX0=");
        }

        private class Body
        {
            public string channel { get; set; }
            public string attributionSource { get; set; }
            public string attributionDetails { get; set; }

            public Body(string channel, string attributionSource, string attributionDetails)
            {
                this.channel = channel;
                this.attributionSource = attributionSource;
                this.attributionDetails = attributionDetails;
            }
        }
    }
}
