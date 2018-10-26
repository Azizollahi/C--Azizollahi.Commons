using System.Threading;
using AzizollahiCommons.Services;
using NUnit.Framework;

namespace AzizollahiCommons.Test {
	[TestFixture]
	public class PublishingEventServiceTest {
		[Test]
		public void PublishingEventService_StartAndStop() {
			var listeningEventAgent = new PublishingEventAgent<string,string>("Hello");
			var ListeningEvent = new ListeningEvent<string>();
			Service service = new PublishingEventService<string, string>("publishing event service", listeningEventAgent, ListeningEvent,null);
			service.Start();
			var res = ListeningEvent.Wait();
			Assert.AreEqual(res, "Hello") ;
		}
	}
}