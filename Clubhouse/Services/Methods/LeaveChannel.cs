using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class LeaveChannel : ClubhouseAPIRequest<BaseResponse>
    {
        public LeaveChannel(string channelName)
            : base(HttpMethod.Post, "leave_channel")
        {
            requestBody = new Body(channelName);
        }

        private class Body
        {
            public string channel { get; set; }
            public string channelId { get; set; }

            public Body(string channel)
            {
                this.channel = channel;
            }
        }
    }
}
