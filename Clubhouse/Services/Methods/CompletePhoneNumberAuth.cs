using Clubhouse.Models;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class CompletePhoneNumberAuth : ClubhouseAPIRequest<CompletePhoneNumberAuth.Response>
    {

        public CompletePhoneNumberAuth(string phoneNumber, string code)
            : base(HttpMethod.Post, "complete_phone_number_auth")
        {
            requestBody = new Body(phoneNumber, code);
        }

        private class Body
        {
            [JsonPropertyName("phone_number")]
            public string PhoneNumber { get; set; }

            [JsonPropertyName("verification_code")]
            public string VerificationCode { get; set; }

            public Body(string phoneNumber, string verificationCode)
            {
                PhoneNumber = phoneNumber;
                VerificationCode = verificationCode;
            }
        }

        public class Response
        {
            [JsonPropertyName("auth_token")]
            public string AuthToken { get; set; }

            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }

            [JsonPropertyName("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonPropertyName("is_onboarding")]
            public bool IsOnboarding { get; set; }

            [JsonPropertyName("is_verified")]
            public bool IsVerified { get; set; }

            [JsonPropertyName("is_waitlisted")]
            public bool IsWaitlisted { get; set; }

            [JsonPropertyName("user_profile")]
            public User UserProfile { get; set; }
        }
    }
}
