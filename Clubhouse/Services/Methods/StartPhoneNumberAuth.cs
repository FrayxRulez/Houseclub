using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class StartPhoneNumberAuth : ClubhouseAPIRequest<BaseResponse>
    {
        public StartPhoneNumberAuth(string phoneNumber)
                : base(HttpMethod.Post, "start_phone_number_auth")

        {
            Content = new Body(phoneNumber);
        }

        private class Body
        {
            [JsonPropertyName("phone_number")]
            public string PhoneNumber { get; set; }

            public Body(string phoneNumber)
            {
                PhoneNumber = phoneNumber;
            }
        }
    }
}
