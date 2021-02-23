using Clubhouse.Models;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetProfile : ClubhouseAPIRequest<GetProfile.Response>
    {
        public GetProfile(ulong id)
            : base(HttpMethod.Post, "get_profile")
        {
            Content = new Body(id);
        }

        private class Body
        {
            [JsonPropertyName("user_id")]
            public ulong UserId { get; set; }

            public Body(ulong userId)
            {
                UserId = userId;
            }
        }

        public class Response : BaseResponse
        {
            [JsonPropertyName("user_profile")]
            public FullUser UserProfile { get; set; }
        }
    }
}
