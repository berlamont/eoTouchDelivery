using System;

namespace eoTouchDelivery.Core.Events
{
	public class IsValidMessageEventArgs : EventArgs
	{
		public IsValidMessageEventArgs(bool isValid)
		{
			IsValid = isValid;
		}

		public bool IsValid { get; }
	}
}
