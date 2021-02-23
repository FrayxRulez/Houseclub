using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class UpdateBio : ClubhouseAPIRequest<BaseResponse>
    {
        public UpdateBio(string bio)
                : base(HttpMethod.Post, "update_bio")
        {
            Content = new Body(bio);
        }

        private class Body
        {
            public string bio { get; set; }

            public Body(string bio)
            {
                this.bio = bio;
            }
        }
    }
}
