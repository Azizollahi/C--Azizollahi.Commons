using System;
using System.Threading.Tasks;
using AzizollahiCommons.Services;
using AzizollahiCommons.Services.Impl;

namespace Test {
	class Program {
		static void Main(string[] args) {
			Event<string> aaa = new BasicEvent<string>();
			var count = 100;
			Task.Run(() => {
				while (count != 0) {
					count--;
					aaa.Notify($"{count}, ");
				}
				
			});
			while (count != 0) {
				Console.Write(aaa.Wait());
			}
			Console.Read();
		}
	}
}