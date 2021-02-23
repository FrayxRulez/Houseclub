using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class UpdateName : ClubhouseAPIRequest<BaseResponse>
    {
        public UpdateName(string name)
            : base(HttpMethod.Post, "update_name")
        {
            Content = new Body(name);
        }

        private class Body
        {
            public string name { get; set; }

            public Body(string name)
            {
                this.name = name;
            }
        }
    }
}
