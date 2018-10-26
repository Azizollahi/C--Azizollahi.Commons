using System;
using System.Collections.Generic;
using System.Linq;
using AzizollahiCommons.Logging;
using AzizollahiCommons.Services;
using AzizollahiCommons.Services.Impl;

namespace AzizollahiCommons.Pooling {
	public class AzizollahiServicePool : ServicePool{
		private readonly int _minimumServices;
		private readonly int _maximumServices;
		private readonly int _increaseStep;
		private readonly int _decreaseStep;
		private readonly int _increasePercentage;
		private readonly int _decreasePercentage;
		private Service poolManager;
		private Queue<ServiceEventMapper> _services;
		private string _name;
		public AzizollahiServicePool(string name, Logger logger, int minimumServices = 10, int maximumServices = 100,
			int increaseStep = 2, int decreaseStep = 1, int increasePercentage = 10, int decreasePercentage = 85) {
			_name = name;
			_minimumServices = minimumServices;
			_maximumServices = maximumServices;
			_increaseStep = increaseStep;
			_decreaseStep = decreaseStep;
			_increasePercentage = increasePercentage;
			_decreasePercentage = decreasePercentage;
			_services = new Queue<ServiceEventMapper>();
			
			Initialize(logger);
		}

		private void Initialize(Logger logger) {
			for (var i = 0; i < _minimumServices; i++) {
				Event<Action> action = new BasicEvent<Action>();
				var serviceHandler = new ServiceEventMapper() {
					Service = new ListeningEventService<Action, string>($"{_name}#{ThreadIdGenerator.GetId()}", new PoolRunnerAgent(), action,
						logger),
					ServiceEvent = action
				};
				serviceHandler.Service.Start();
				_services.Enqueue(serviceHandler);
			}
			poolManager = new PeriodicService<string,string>("AzizollahiServicePoolManager", new PoolAgent(this,logger), periodicTime: 100, logger: logger);
			poolManager.Start();
		}

		public void Run(Action action) {
			ServiceEventMapper serviceEvent = null;
			lock (_services) {
				serviceEvent = _services.Dequeue();
			}
			serviceEvent.ServiceEvent.Notify(action);
			lock (_services) {
				_services.Enqueue(serviceEvent);
			}
		}

		public Queue<ServiceEventMapper> GetEventsReadyForAction() {
			lock (_services) {
				return _services;
			}
		}

		public string GetName() {
			return _name;
		}

		public int GetMinimumServices() {
			return _minimumServices;
		}

		public int GetMaximumServices() {
			return _maximumServices;
		}

		public int GetIncrementStep() {
			return _increaseStep;
		}

		public int GetDecrementStep() {
			return _decreaseStep;
		}

		public int GetIncrementPercentage() {
			return _increasePercentage;
		}

		public int GetDecrementPercentage() {
			return _decreasePercentage;
		}
	}
}