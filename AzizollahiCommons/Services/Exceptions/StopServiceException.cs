using System;

namespace AzizollahiCommons.Services.Exceptions {
	public class StopServiceException : Exception {
		public StopServiceException(string message) : base(message){}
	}
}