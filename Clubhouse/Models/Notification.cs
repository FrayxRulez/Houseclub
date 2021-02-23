using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class Notification
    {
        [JsonPropertyName("notification_id")]
        public long NotificationId { get; set; }

        [JsonPropertyName("type")]
        public long Type { get; set; }

        [JsonPropertyName("time_created")]
        public string TimeCreated { get; set; }

        [JsonPropertyName("is_unread")]
        public bool IsUnread { get; set; }

        [JsonPropertyName("user_profile")]
        public User UserProfile { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("event_id")]
        public long? EventId { get; set; }
    }
}
