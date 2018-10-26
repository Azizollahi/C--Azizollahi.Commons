using AzizollahiCommons.Logging;

namespace AzizollahiCommons.Services {
	public class DoubleDirectionEventService<I,O> : AService<I,O> {
		private Event<I> _inputEvent;
		private Event<O> _outputEvent;
		public DoubleDirectionEventService(string name, Agent<I, O> agent, Event<I> inputEvent, Event<O> outputEvent, Logger logger = null) : base(name, agent, logger) {
			_inputEvent = inputEvent;
			_outputEvent = outputEvent;
		}
		protected override void OnStart() {
		}

		protected override I OnStartProcess() {
			return _inputEvent.Wait();
		}

		protected override void OnEndProcess(O data) {
			_outputEvent.Notify(data);
		}

		protected override void OnStop() {
		}

		protected override void OnPause() {
		}

		protected override void OnResume() {
		}
	}
}