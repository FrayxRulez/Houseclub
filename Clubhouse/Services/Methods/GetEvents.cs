using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetEvents : ClubhouseAPIRequest<GetEvents.Response>
    {
        public GetEvents(int pageSize, int page)
            : base(HttpMethod.Get, "get_events")
        {
            QueryParameters = new Dictionary<string, string>
            {
                { "page_size", $"{pageSize}" },
                { "page", $"{page}" }
            };
        }

        public class Response : PagedResponse
        {
            [JsonPropertyName("events")]
            public List<Event> Events { get; set; }
        }
    }
}
