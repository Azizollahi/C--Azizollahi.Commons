using System;
using System.Threading;
using AzizollahiCommons.Logging;
using AzizollahiCommons.Services.Exceptions;

namespace AzizollahiCommons.Services {
	public abstract class AService<I,O> : Service {
		private Thread _trd;
		protected readonly Logger Logger;
		private bool _runningState;
		private bool _paused;
		private readonly string _name;
		private readonly Agent<I,O> _agent;
		private readonly Semaphore _pausSemaphore;
		public AService(string name, Agent<I,O> agent, Logger logger = null) {
			_pausSemaphore = new Semaphore(0, 1);
			_name = name;
			_agent = agent;
			Logger = logger;
			_trd = new Thread(StartThread);
			_runningState = false;
			_paused = true;
		}
		public void Start() {
			Logger?.Debug("Service is going to start");
			OnStart();
			_runningState = true;
			_paused = false;
			_trd.Start();
		}

		public void Stop() {
			OnStop();
			_runningState = false;
			_trd.Interrupt();
			_paused = true;
			Logger?.Debug("Service is stopped");
		}

		public void Pause() {
			Logger?.Debug("Going to pause the Service");
			OnPause();
			_paused = true;
			Logger?.Debug("Service is resumed");
		}
		public void Resume() {
			OnResume();
			_paused = false;
			_pausSemaphore.Release();
		}

		public bool IsAlive() {
			return _runningState;
		}

		public bool IsPaused() {
			return _paused;
		}

		protected abstract void OnStart();
		protected abstract I OnStartProcess();
		protected abstract void OnEndProcess(O data);
		protected abstract void OnStop();
		protected abstract void OnPause();
		protected abstract void OnResume();

		private void StartThread() {
			while (_runningState) {
				try {
					Logger?.Trace("Going to process");
					var data = OnStartProcess();
					if (_paused) {
						_pausSemaphore.WaitOne();
						continue;
					}

					var result = _agent.Run(data);
					OnEndProcess(result);
					Logger?.Trace("Process is finished");
				}
				catch (StopServiceException stopService) {
					Logger?.Info("Agent requested to stop the service", stopService);
					Stop();
					break;
				}
				catch (ThreadInterruptedException) {
					break;
				}
				catch (Exception e) {
					Logger?.Error(_name,e);
				}
			}
		}
	}
}