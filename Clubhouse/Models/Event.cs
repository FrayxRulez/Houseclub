using System;
using System.Collections.Generic;

namespace Clubhouse.Models
{
	public class Event
	{
		public string channel { get; set; }
		public bool isExpired { get; set; }
		public DateTime timeStart { get; set; }
		public string description { get; set; }
		public string name { get; set; }
		public int eventId { get; set; }
		public bool isMemberOnly { get; set; }
		public List<FullUser> hosts { get; set; }
		public bool clubIsMember { get; set; }
		public bool clubIsFollower { get; set; }
	}
}