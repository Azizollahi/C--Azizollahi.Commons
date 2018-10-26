using System;
using AzizollahiCommons.Services;

namespace AzizollahiCommons.Pooling {
	public class ServiceEventMapper {
		public Service Service { get; set; }
		public Event<Action> ServiceEvent { get; set; }
	}
}