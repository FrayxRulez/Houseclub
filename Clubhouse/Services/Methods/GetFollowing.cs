using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class GetFollowing : ClubhouseAPIRequest<GetFollowing.Response>
    {
        public GetFollowing(int userID, int pageSize, int page)
            : base(HttpMethod.Get, "get_following")
        {
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userID + "");
            queryParams.Add("page_size", pageSize + "");
            queryParams.Add("page", page + "");
        }

        public class Response
        {
            public List<FullUser> users { get; set; }
            public int count { get; set; }
        }
    }
}
