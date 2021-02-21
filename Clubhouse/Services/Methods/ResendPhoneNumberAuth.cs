using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class ResendPhoneNumberAuth : ClubhouseAPIRequest<BaseResponse>
    {
        public ResendPhoneNumberAuth(string phoneNumber)
            : base(HttpMethod.Post, "resend_phone_number_auth")
        {
            requestBody = new Body(phoneNumber);
        }

        private class Body
        {
            public string phoneNumber { get; set; }

            public Body(string phoneNumber)
            {
                this.phoneNumber = phoneNumber;
            }
        }
    }
}
