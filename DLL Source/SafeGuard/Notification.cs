using System;

namespace SafeGuard
{
	public class Notification
	{
		public long NotificationId { get; set; }

		public DateTime CreateDate { get; set; }

		public string Message { get; set; }

		public string CreatedBy { get; set; }

		public bool IsActive { get; set; }
	}
}
