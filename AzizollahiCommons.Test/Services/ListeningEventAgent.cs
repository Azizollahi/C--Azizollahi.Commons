using AzizollahiCommons.Services;

namespace AzizollahiCommons.Test {
	public class ListeningEventAgent<I, O>  : Agent<I,O> {
		private EventTestInfo<I> info;
		public ListeningEventAgent(EventTestInfo<I> info) {
			this.info = info;
		}
		public O Run(I data) {
			info.Counter = data;
			return default(O);
		}
	}
}