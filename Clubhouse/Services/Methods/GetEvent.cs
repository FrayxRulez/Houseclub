using Clubhouse.Models;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetEvent : ClubhouseAPIRequest<GetEvent.Response>
    {
        public GetEvent(string id)
            : base(HttpMethod.Post, "get_event")
        {
            Content = new Body(id);
        }

        private class Body
        {
            [JsonPropertyName("event_hashid")]
            public string EventHashId { get; set; }

            public Body(string eventHashid)
            {
                EventHashId = eventHashid;
            }
        }

        public class Response : BaseResponse
        {
            [JsonPropertyName("event")]
            public Event Event { get; set; }
        }
    }
}
