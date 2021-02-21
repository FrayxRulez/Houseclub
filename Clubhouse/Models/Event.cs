using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class Event
	{
		[JsonPropertyName("channel")]
		public string channel { get; set; }

		[JsonPropertyName("is_expired")]
		public bool isExpired { get; set; }

		[JsonPropertyName("time_start")]
		public DateTime timeStart { get; set; }

		[JsonPropertyName("description")]
		public string description { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("club")]
		public Club Club { get; set; }

		[JsonPropertyName("event_id")]
		public int eventId { get; set; }

		[JsonPropertyName("is_member_only")]
		public bool isMemberOnly { get; set; }

		[JsonPropertyName("hosts")]
		public List<FullUser> hosts { get; set; }

		[JsonPropertyName("club_is_member")]
		public bool clubIsMember { get; set; }

		[JsonPropertyName("club_is_follower")]
		public bool clubIsFollower { get; set; }
	}
}