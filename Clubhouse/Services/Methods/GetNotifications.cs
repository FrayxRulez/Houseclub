using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetNotifications : ClubhouseAPIRequest<GetNotifications.Response>
    {
        public GetNotifications(int pageSize, int page)
            : base(HttpMethod.Get, "get_notifications")
        {
            QueryParameters = new Dictionary<string, string>
            {
                { "page_size", $"{pageSize}" },
                { "page", $"{page}" }
            };
        }

        public class Response : BaseResponse
        {
            [JsonPropertyName("notifications")]
            public List<Notification> Notifications { get; set; }

            [JsonPropertyName("count")]
            public int Count { get; set; }

            [JsonPropertyName("next")]
            public int? Next { get; set; }

            [JsonPropertyName("previous")]
            public int? Previous { get; set; }
        }
    }
}
