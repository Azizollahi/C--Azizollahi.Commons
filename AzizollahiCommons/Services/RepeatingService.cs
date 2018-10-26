using System;
using AzizollahiCommons.Logging;

namespace AzizollahiCommons.Services {
	public class RepeatingService<I,O> : AService<I,O> {
		public RepeatingService(string name, Agent<I,O> agent, Logger logger = null) : base(name, agent, logger) {
		}
		protected override void OnStart() {
		}

		protected override I OnStartProcess() {
			return default(I);
		}

		protected override void OnEndProcess(O result) {
			if(result != null)
				Logger?.Trace($"Output is: {result.ToString()}");
		}

		protected override void OnStop() {
		}

		protected override void OnPause() {
		}

		protected override void OnResume() {
		}
	}
}