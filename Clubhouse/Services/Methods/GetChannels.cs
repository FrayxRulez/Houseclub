using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetChannels : ClubhouseAPIRequest<GetChannels.Response>
    {
        public GetChannels()
            : base(HttpMethod.Get, "get_channels")
        {
        }

        public class Response : BaseResponse
        {
            [JsonPropertyName("channels")]
            public List<Channel> Channels { get; set; }

            [JsonPropertyName("events")]
            public List<Event> Events { get; set; }
        }
    }
}
