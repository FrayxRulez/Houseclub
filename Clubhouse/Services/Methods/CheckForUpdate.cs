using System.Collections.Generic;
using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class CheckForUpdate : ClubhouseAPIRequest<BaseResponse>
    {
        public CheckForUpdate()
            : base(HttpMethod.Get, "check_for_upate")
        {
            queryParams = new Dictionary<string, string>();
            queryParams.Add("is_testflight", "0");
        }
    }
}
