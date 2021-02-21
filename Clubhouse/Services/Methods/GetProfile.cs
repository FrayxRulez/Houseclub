using Clubhouse.Models;
using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class GetProfile : ClubhouseAPIRequest<GetProfile.Response>
    {
        public GetProfile(int id)
            : base(HttpMethod.Post, "get_profile")
        {
            requestBody = new Body(id);
        }

        private class Body
        {
            public int userId { get; set; }

            public Body(int userId)
            {
                this.userId = userId;
            }
        }

        public class Response
        {
            public FullUser userProfile { get; set; }
        }
    }
}
