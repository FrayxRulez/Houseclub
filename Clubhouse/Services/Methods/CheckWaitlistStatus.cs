using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class CheckWaitlistStatus : ClubhouseAPIRequest<CheckWaitlistStatus.Response>
    {
        public CheckWaitlistStatus()
            : base(HttpMethod.Post, "check_waitlist_status")
        {
        }

        public class Response : BaseResponse
        {
            [JsonPropertyName("is_waitlisted")]
            public bool IsWaitlisted { get; set; }

            [JsonPropertyName("is_onboarding")]
            public bool IsOnboarding { get; set; }
        }
    }
}
