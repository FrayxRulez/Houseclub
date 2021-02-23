using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class ActionableNotification
    {
        [JsonPropertyName("actionable_notification_id")]
        public long ActionableNotificationId { get; set; }

        [JsonPropertyName("type")]
        public long Type { get; set; }

        [JsonPropertyName("time_created")]
        public string TimeCreated { get; set; }

        [JsonPropertyName("user_profile")]
        public User UserProfile { get; set; }
    }
}
