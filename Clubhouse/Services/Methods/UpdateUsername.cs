using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class UpdateUsername : ClubhouseAPIRequest<BaseResponse>
    {
        public UpdateUsername(string name)
            : base(HttpMethod.Post, "update_username")
        {
            requestBody = new Body(name);
        }

        private class Body
        {
            public string username { get; set; }

            public Body(string username)
            {
                this.username = username;
            }
        }
    }
}
