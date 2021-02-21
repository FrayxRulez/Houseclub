using System;

namespace Clubhouse.Models
{
    public class FullUser : User
	{
		public string dsplayname { get; set; }
		public string bio { get; set; }
		public string twitter { get; set; }
		public string instagram { get; set; }
		public int numFollowers { get; set; }
		public int numFollowing { get; set; }
		public bool followsMe { get; set; }
		public bool isBlockedByNetwork { get; set; }
		public DateTime timeCreated { get; set; }
		public User invitedByUserProfile;
		// null = not following
		// 2 = following
		// other values = ?
		public int notificationType;

		public bool isFollowed()
		{
			return notificationType == 2;
		}
	}
}
