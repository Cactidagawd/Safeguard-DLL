using System;
using System.Collections.Generic;

namespace SafeGuard
{
	public class LoginResponse : ErrorResponse
	{
		public long Id { get; set; }

		public string ProgramName { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public DateTime ExpirationDate { get; set; }

		public int Level { get; set; }

		public bool Banned { get; set; }

		public string FullName { get; set; }

		public string HID { get; set; }

		public List<Notification> Notifications { get; set; }
	}
}
