using AzizollahiCommons.Services;

namespace AzizollahiCommons.Test {
	public class PublishingEventAgent<I, O>  : Agent<I,O> {
		private readonly O _output;
		public PublishingEventAgent(O output) {
			_output = output;
		}
		public O Run(I data) {
			return _output;
		}
	}
}