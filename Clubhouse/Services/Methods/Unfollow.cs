using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class Unfollow : ClubhouseAPIRequest<BaseResponse>
    {
        public ulong UserId { get; }

        public Unfollow(ulong userID)
            : base(HttpMethod.Post, "unfollow")
        {
            UserId = userID;
            Content = new Body(userID);
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
    }
}
