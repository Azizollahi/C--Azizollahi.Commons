using System;
using AzizollahiCommons.Services;

namespace AzizollahiCommons.Pooling {
	public class PoolRunnerAgent : Agent<Action, string> {
		public string Run(Action action) {
			action();
			return string.Empty;
		}
	}
}