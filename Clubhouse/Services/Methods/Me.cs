using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using Windows.Globalization;

namespace Clubhouse.Services.Methods
{
    public class Me : ClubhouseAPIRequest<Me.Response>
    {
        public Me(bool return_blocked_ids, bool return_following_ids)
            : base(HttpMethod.Post, "me")
        {
            var calendar = new Calendar();
            var timezone_identifier = calendar.GetTimeZone();

            RequiresInitialization = false;
            Content = new Body(return_blocked_ids, timezone_identifier, return_following_ids);
        }

        private class Body
        {
            [JsonPropertyName("return_blocked_ids")]
            public bool ReturnBlockedIds { get; private set; }

            [JsonPropertyName("timezone_identifier")]
            public string TimeZoneIdentifier { get; private set; }

            [JsonPropertyName("return_following_ids")]
            public bool ReturnFollowingIds { get; private set; }

            public Body(bool return_blocked_ids, string timezone_identifier, bool return_following_ids)
            {
                ReturnBlockedIds = return_blocked_ids;
                TimeZoneIdentifier = timezone_identifier;
                ReturnFollowingIds = return_following_ids;
            }
        }

        public class Response : BaseResponse
        {
            [JsonPropertyName("has_unread_notifications")]
            public bool HasUnreadNotifications { get; set; }

            [JsonPropertyName("actionable_notifications_count")]
            public int ActionableNotificationsCount { get; set; }

            [JsonPropertyName("num_invites")]
            public int NumInvites { get; set; }

            [JsonPropertyName("auth_token")]
            public string AuthToken { get; set; }

            [JsonPropertyName("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }

            [JsonPropertyName("notifications_enabled")]
            public bool NotificationsEnabled { get; set; }

            [JsonPropertyName("user_profile")]
            public User UserProfile { get; set; }

            [JsonPropertyName("following_ids")]
            public List<ulong> FollowingIds { get; set; }

            [JsonPropertyName("blocked_ids")]
            public List<ulong> BlockedIds { get; set; }

            [JsonPropertyName("is_admin")]
            public bool IsAdmin { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("feature_flags")]
            public List<string> FeatureFlags { get; set; }
        }
    }
}
