using System;

namespace SafeGuard
{
	public class AccountGen : ErrorResponse
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public bool IsUsed { get; set; }

		public string UsedBy { get; set; }

		public long type { get; set; }

		public Guid Program { get; set; }

		public int Id { get; set; }
	}
}
