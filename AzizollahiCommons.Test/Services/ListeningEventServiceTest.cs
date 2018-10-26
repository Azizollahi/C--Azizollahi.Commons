using System;
using System.Threading;
using System.Xml;
using AzizollahiCommons.Services;
using AzizollahiCommons.Services.Impl;
using NUnit.Framework;

namespace AzizollahiCommons.Test {
	[TestFixture]
	public class ListeningEventServiceTest {
		[Test]
		public void ListeningEventService_StartAnsStop() {
			var info = new EventTestInfo<string>();
			var listeningEventAgent = new ListeningEventAgent<string,string>(info);
			var ListeningEvent = new ListeningEvent<string>();
			Service service = new ListeningEventService<string, string>("listening event service", listeningEventAgent, ListeningEvent,null);
			service.Start();
			ListeningEvent.Notify("Hello");
			Thread.Sleep(10);
			Assert.AreEqual(info.Counter, "Hello") ;
		}
	}
}