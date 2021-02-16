using System;

namespace SafeGuard
{
	public class AutoUpdateObject : ErrorResponse
	{
		public string Version { get; set; }

		public string Url { get; set; }

		public bool Enabled { get; set; }
	}
}
