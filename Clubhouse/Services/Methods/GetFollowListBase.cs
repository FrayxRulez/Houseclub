﻿using Clubhouse.Models;
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
            QueryParameters = new Dictionary<string, string>
            {
                { "user_id", $"{userID}" },
                { "page_size", $"{pageSize}" },
                { "page", $"{page}" }
            };
        }

        public class Response : PagedResponse
        {
            [JsonPropertyName("clubs")]
            public List<Club> Clubs { get; set; }

            [JsonPropertyName("users")]
            public List<FullUser> Users { get; set; }
        }
    }
}
