using System;

namespace AzizollahiCommons.Services.Impl {
	public class BasicAgent<I,O> : Agent<I,O> {
		private Action _action;
		public BasicAgent(Action action) {
			_action = action;
		}

		public O Run(I data) {
			_action();
			return default(O);
		}
	}
}