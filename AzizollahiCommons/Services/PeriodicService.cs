using System;
using System.Diagnostics;
using System.Threading;
using AzizollahiCommons.Logging;

namespace AzizollahiCommons.Services {
	public class PeriodicService<I,O> : AService<I,O> {
		private readonly int _periodicTime;
		private Stopwatch _stopwatch;
		public PeriodicService(string name, Agent<I,O> agent, int periodicTime, Logger logger) : base(name, agent, logger) {
			_periodicTime = periodicTime;
		}
		protected override void OnStart() {
			// ignored
		}

		protected override I OnStartProcess() {
			_stopwatch = new Stopwatch();
			_stopwatch.Start();
			return default(I);
		}

		protected override void OnEndProcess(O data) {
			_stopwatch.Stop();
			WaitForPeriodicTime();
			Logger?.Trace($"Process time: {_stopwatch.ElapsedMilliseconds}");
		}

		protected override void OnStop() {
		}

		protected override void OnPause() {
		}

		protected override void OnResume() {
		}
		
		private void WaitForPeriodicTime() {
			var totalTime = _periodicTime - _stopwatch.ElapsedMilliseconds;
			if(totalTime > 0)
				Thread.Sleep((int)totalTime);
		}
	}
}