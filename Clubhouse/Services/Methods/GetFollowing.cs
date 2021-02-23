namespace Clubhouse.Services.Methods
{
    public class GetFollowing : GetFollowListBase
    {
        public GetFollowing(ulong userID, int pageSize, int page)
            : base("get_following", userID, pageSize, page)
        {
        }
    }
}
