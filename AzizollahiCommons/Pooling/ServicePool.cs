using System;
using System.Collections.Generic;
using AzizollahiCommons.Services;

namespace AzizollahiCommons.Pooling {
	public interface ServicePool {
		void Run(Action action);
		Queue<ServiceEventMapper> GetEventsReadyForAction();
		string GetName();
		int GetMinimumServices();
		int GetMaximumServices();
		int GetIncrementStep();
		int GetDecrementStep();
		int GetIncrementPercentage();
		int GetDecrementPercentage();
	}
}