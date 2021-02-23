namespace Clubhouse.Services.Methods
{
    public class GetMutualFollows : GetFollowListBase
    {
        public GetMutualFollows(ulong userID, int pageSize, int page)
            : base("get_mutual_follows", userID, pageSize, page)
        {
        }
    }
}
