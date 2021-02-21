using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class Follow : ClubhouseAPIRequest<BaseResponse>
    {
        public Follow(int userID)
            : base(HttpMethod.Post, "follow")
        {
            requestBody = new Body(userID);
        }

        private class Body
        {
            public int userId { get; set; }
            public int source { get; set; } = 4;

            public Body(int userId)
            {
                this.userId = userId;
            }
        }
    }
}
