using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetFollowers : ClubhouseAPIRequest<GetFollowers.Response>
    {
        public GetFollowers(ulong userID, int pageSize, int page)
            : base(HttpMethod.Get, "get_followers")
        {
            queryParams = new Dictionary<string, string>
            {
                { "user_id", $"{userID}" },
                { "page_size", $"{pageSize}" },
                { "page", $"{page}" }
            };
        }

        public class Response
        {
            [JsonPropertyName("users")]
            public List<FullUser> Users { get; set; }

            [JsonPropertyName("count")]
            public int Count { get; set; }
        }
    }
}
