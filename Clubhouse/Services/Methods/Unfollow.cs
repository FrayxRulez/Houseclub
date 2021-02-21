using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class Unfollow : ClubhouseAPIRequest<BaseResponse>
    {
        public Unfollow(int userID)
            : base(HttpMethod.Post, "unfollow")
        {
            requestBody = new Body(userID);
        }

        private class Body
        {
            public int userId { get; set; }

            public Body(int userId)
            {
                this.userId = userId;
            }
        }
    }
}
