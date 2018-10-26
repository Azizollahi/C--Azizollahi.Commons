using AzizollahiCommons.Logging;

namespace AzizollahiCommons.Services {
	public class ListeningEventService<I,O> : AService<I,O> {
		private readonly Event<I> _event;
		public ListeningEventService(string name, Agent<I,O> agent, Event<I> serviceEvent, Logger logger = null) : base(name, agent, logger) {
			_event = serviceEvent;
		}
		protected override void OnStart() {
		}

		protected override I OnStartProcess() {
			var result = _event.Wait();
			return result;

		}

		protected override void OnEndProcess(O data) {
			// ignored
		}

		protected override void OnStop() {
		}

		protected override void OnPause() {
		}

		protected override void OnResume() {
		}
	}
}