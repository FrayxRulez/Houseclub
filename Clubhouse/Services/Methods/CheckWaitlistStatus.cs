using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class CheckWaitlistStatus : ClubhouseAPIRequest<CheckWaitlistStatus.Response>
    {
        public CheckWaitlistStatus()
            : base(HttpMethod.Post, "check_waitlist_status")
        {
        }

        public class Response
        {
            public bool isWaitlisted { get; set; }
            public bool isOnboarding { get; set; }
        }
    }
}
