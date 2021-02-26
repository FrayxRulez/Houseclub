using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clubhouse.Models
{
    public class Event
	{
		[JsonPropertyName("channel")]
		public string Channel { get; set; }

		[JsonPropertyName("is_expired")]
		public bool IsExpired { get; set; }

		[JsonPropertyName("time_start")]
		public DateTime TimeStart { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("club")]
		public Club Club { get; set; }

		[JsonPropertyName("event_id")]
		public int EventId { get; set; }

		[JsonPropertyName("is_member_only")]
		public bool IsMemberOnly { get; set; }

		[JsonPropertyName("hosts")]
		public List<FullUser> Hosts { get; set; }

		//[JsonPropertyName("club_is_member")]
		//public bool ClubIsMember { get; set; }

		//[JsonPropertyName("club_is_follower")]
		//public bool ClubIsFollower { get; set; }
	}
}