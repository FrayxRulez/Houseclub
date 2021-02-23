namespace Clubhouse.Services.Methods
{
    public class GetFollowers : GetFollowListBase
    {
        public GetFollowers(ulong userID, int pageSize, int page)
            : base("get_followers", userID, pageSize, page)
        {
        }
    }
}
