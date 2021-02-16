using System;
using System.ComponentModel;

namespace SafeGuard
{
	internal class Enums
	{
		internal enum Messaging
		{
			[Description("There was an error with your login")]
			LoginError,
			[Description("SafeGuard will be back up shortly")]
			SafeGuardApiError,
			[Description("Adding time failed")]
			AddTimeError,
			[Description("There was an error registering")]
			RegisterError,
			[Description("Unable to send attack")]
			AttackError,
			[Description("Value is null")]
			NullError,
			[Description("Please download the newest update")]
			AutoUpdateInstruction,
			[Description("SafeGuardAuth.us")]
			AutoUpdateTitle
		}
	}
}
