using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class Follow : ClubhouseAPIRequest<BaseResponse>
    {
        public ulong UserId { get; }

        public Follow(ulong userID)
            : base(HttpMethod.Post, "follow")
        {
            UserId = userID;
            Content = new Body(userID);
        }

        private class Body
        {
            [JsonPropertyName("user_id")]
            public ulong UserId { get; set; }

            [JsonPropertyName("source")]
            public int Source { get; set; } = 4;

            public Body(ulong userId)
            {
                UserId = userId;
            }
        }
    }
}
