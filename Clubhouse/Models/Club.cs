using System;
using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class Club
    {
        [JsonPropertyName("club_id")]
        public int ClubId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("photo_url")]
        public Uri PhotoUrl { get; set; }

        [JsonPropertyName("num_members")]
        public long NumMembers { get; set; }

        [JsonPropertyName("num_followers")]
        public long NumFollowers { get; set; }

        [JsonPropertyName("enable_private")]
        public bool EnablePrivate { get; set; }

        [JsonPropertyName("is_follow_allowed")]
        public bool IsFollowAllowed { get; set; }

        [JsonPropertyName("is_membership_private")]
        public bool IsMembershipPrivate { get; set; }

        [JsonPropertyName("is_community")]
        public bool IsCommunity { get; set; }

        //[JsonPropertyName("rules")]
        //public List<Rule> Rules { get; set; }

        [JsonPropertyName("num_online")]
        public long NumOnline { get; set; }
    }
}
