using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class RefreshToken : ClubhouseAPIRequest<BaseResponse>
    {
        public RefreshToken(string token)
            : base(HttpMethod.Post, "refresh_token")
        {
            Content = new Body(token);
        }

        private class Body
        {
            [JsonPropertyName("refresh")]
            public string Refresh { get; set; }

            public Body(string refresh)
            {
                Refresh = refresh;
            }
        }
    }
}
