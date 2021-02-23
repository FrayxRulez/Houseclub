using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetFollowListBase : ClubhouseAPIRequest<GetFollowListBase.Response>
    {
        public GetFollowListBase(string path, ulong userID, int pageSize, int page)
            : base(HttpMethod.Get, path)
        {
            queryParams = new Dictionary<string, string>
            {
                { "user_id", $"{userID}" },
                { "page_size", $"{pageSize}" },
                { "page", $"{page}" }
            };
        }

        public class Response : BaseResponse
        {
            [JsonPropertyName("clubs")]
            public List<Club> Clubs { get; set; }

            [JsonPropertyName("users")]
            public List<FullUser> Users { get; set; }

            [JsonPropertyName("count")]
            public int Count { get; set; }

            [JsonPropertyName("previous")]
            public int? Previous { get; set; }

            [JsonPropertyName("next")]
            public int? Next { get; set; }
        }
    }
}
