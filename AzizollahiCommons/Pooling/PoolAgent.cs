using System;
using System.Collections.Generic;
using System.Linq;
using AzizollahiCommons.Logging;
using AzizollahiCommons.Services;
using AzizollahiCommons.Services.Impl;

namespace AzizollahiCommons.Pooling {
	class PoolAgent: Agent<string, string> {
		private readonly ServicePool _servicePool;
		private readonly Logger _logger;
		private readonly Queue<ServiceEventMapper> _serviceHandlers;
		public PoolAgent(ServicePool servicePool, Logger logger) {
			_servicePool = servicePool;
			_logger = logger;
			_serviceHandlers = _servicePool.GetEventsReadyForAction();
		}
		
		public string Run(string data) {
			if (CheckIncrementPercentageProcess()) {
				for (var counter = 0; counter < _servicePool.GetIncrementStep(); counter++) {
					StartService();
				}
			}

			if (CheckDecrementPercentageProcess()) {
				for(var counter =0; counter < _servicePool.GetIncrementStep(); counter++)
					StopService();
			}
			return string.Empty;
		}
		
		private void StartService() {
			Event<Action> action = new BasicEvent<Action>();
			var serviceHandler = new ServiceEventMapper() {
				Service = new ListeningEventService<Action, string>($"{_servicePool.GetName()}#{ThreadIdGenerator.GetId()}", new PoolRunnerAgent(), action,
					_logger),
				ServiceEvent = action
			};
			serviceHandler.Service.Start();
			lock(_serviceHandlers)
				_serviceHandlers.Enqueue(serviceHandler);
		}

		private void StopService() {
			lock (_serviceHandlers) {
				int stepCounter = 0;
				for (int idx = 0; idx < _serviceHandlers.Count; idx++) {
					if(stepCounter == _servicePool.GetDecrementStep())
						break;
					var serviceHandler = _serviceHandlers.Dequeue();
					if (serviceHandler.Service.IsAlive() && serviceHandler.ServiceEvent.IsWaiting()) {
						serviceHandler.Service.Stop();
						stepCounter++;
					}
					else {
						_serviceHandlers.Enqueue(serviceHandler);
					}
						
				}
			}
			
		}

		private bool CheckIncrementPercentageProcess() {
			var currentAlive = 0;
			lock (_serviceHandlers) {
				currentAlive = _serviceHandlers.Count(x => x.Service.IsAlive());
			}
			if (WaitingPercentage() < _servicePool.GetIncrementPercentage() && _servicePool.GetMaximumServices() > currentAlive)
				return true;
			return false;
		}
		private bool CheckDecrementPercentageProcess() {
			var currentAlive = 0;
			lock (_serviceHandlers) {
				currentAlive = _serviceHandlers.Count(x => x.Service.IsAlive());
			}
			if (WaitingPercentage() >= _servicePool.GetDecrementPercentage() && _servicePool.GetMinimumServices() < currentAlive)
				return true;
			return false;
		}

		private int WaitingPercentage() {
			int waitingCount = 0;
			int total = 1;
			lock (_serviceHandlers) {
				waitingCount = _serviceHandlers.Count(x => x.Service.IsAlive() && x.ServiceEvent.IsWaiting());
				total = _serviceHandlers.Count(x => x.Service.IsAlive());
			}
			if (total == 0)
				return 0;
			return (waitingCount / total) * 100;
			
		}
	}
}