using System.Collections.Generic;

namespace Clubhouse.Navigation.Services
{
    public class NavigationState : Dictionary<string, object>
    {
        public static NavigationState GetChatMember(long chatId, int userId)
        {
            return new NavigationState { { "chatId", chatId }, { "userId", userId } };
        }
    }
}
