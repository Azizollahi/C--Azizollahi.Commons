using System;
using System.Diagnostics;
using System.Threading;
using AzizollahiCommons.Services;
using AzizollahiCommons.Services.Exceptions;
using AzizollahiCommons.Services.Impl;
using NUnit.Framework;

namespace AzizollahiCommons.Test {
	[TestFixture]
	public class RepeatingServiceTest {

		[Test]
		public void RepeatingService_StartAndStop() {
			var sw = new Stopwatch();
			Agent<DateTime, DateTime> basicAgent = new BasicAgent<DateTime, DateTime>(() => {
				if (sw.IsRunning) {
					sw.Stop();
					throw new StopServiceException("");
				}

				sw.Start();
			});
			Service service = new RepeatingService<DateTime, DateTime>("repeatingService", basicAgent,null);
			
			service.Start();
			
			while(service.IsAlive());
			Assert.LessOrEqual(sw.Elapsed.TotalMilliseconds, 50);
		}
		
	}

}