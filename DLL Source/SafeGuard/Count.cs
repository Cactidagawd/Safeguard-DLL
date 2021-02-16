using System;

namespace SafeGuard
{
	public class Count : ErrorResponse
	{
		public int UserCount { get; set; }

		public int TokenCount { get; set; }

		public int BotnetCount { get; set; }

		public int AccountCount { get; set; }
	}
}
