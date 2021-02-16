using System;

namespace SafeGuard
{
	public class RegisterInformationObject : ErrorResponse
	{
		public string Token { get; set; }

		public string Email { get; set; }
	}
}
