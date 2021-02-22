using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class OnlineUser : User
    {
        [JsonPropertyName("last_active_minutes")]
        public int LastActiveMinutes { get; set; }
    }
}
