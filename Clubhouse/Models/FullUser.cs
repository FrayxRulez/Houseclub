using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class FullUser : User
	{
        [JsonPropertyName("displayname")]
        public string DisplayName { get; set; }

        [JsonPropertyName("bio")]
		public string Bio { get; set; }

		[JsonPropertyName("twitter")]
		public string Twitter { get; set; }

		[JsonPropertyName("instagram")]
		public string Instagram { get; set; }

		[JsonPropertyName("num_followers")]
		public int NumFollowers { get; set; }

		[JsonPropertyName("num_following")]
		public int NumFollowing { get; set; }

		[JsonPropertyName("mutual_follows_count")]
		public int MutualFollowsCount { get; set; }

		[JsonPropertyName("mutual_follows")]
		public List<User> MutualFollows { get; set; }

		[JsonPropertyName("clubs")]
		public List<Club> Clubs { get; set; }

		[JsonPropertyName("follows_me")]
		public bool FollowsMe { get; set; }

		[JsonPropertyName("is_blocked_by_network")]
		public bool IsBlockedByNetwork { get; set; }

		[JsonPropertyName("time_created")]
		public DateTime TimeCreated { get; set; }

		[JsonPropertyName("invited_by_user_profile")]
		public User InvitedByUserProfile { get; set; }
		// null = not following
		// 2 = following
		// other values = ?
		[JsonPropertyName("notification_type")]
		public int NotificationType;

		public bool isFollowed()
		{
			return NotificationType == 2;
		}
	}
}
