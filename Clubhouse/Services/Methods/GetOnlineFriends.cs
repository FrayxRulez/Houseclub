using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetOnlineFriends : ClubhouseAPIRequest<GetOnlineFriends.Response>
    {
        public GetOnlineFriends()
            : base(HttpMethod.Get, "get_online_friends")
        {

        }

        public class Response : BaseResponse
        {
            [JsonPropertyName("clubs")]
            public List<Club> Clubs { get; set; }

            [JsonPropertyName("users")]
            public List<OnlineUser> Users { get; set; }
        }
    }
}
