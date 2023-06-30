using System;

namespace AGoldFish.Launchpad
{
	public class LaunchpadException : Exception
	{
		public LaunchpadException() : base() { }
		public LaunchpadException(string message) : base(message) { }
	}
}
