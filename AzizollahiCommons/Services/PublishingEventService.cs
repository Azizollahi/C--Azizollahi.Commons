using AzizollahiCommons.Logging;

namespace AzizollahiCommons.Services {
	public class PublishingEventService<I,O> : AService<I,O> {
		private readonly Event<O> _event;

		public PublishingEventService(string name, Agent<I,O> agent, Event<O> serviceEvent, Logger logger = null) :
			base(name, agent, logger) {
			_event = serviceEvent;
		}
		protected override void OnStart() {
		}

		protected override I OnStartProcess() {
			return default(I);
		}

		protected override void OnEndProcess(O data) {
			_event.Notify(data);
		}

		protected override void OnStop() {
		}

		protected override void OnPause() {
		}

		protected override void OnResume() {
		}
	}
}