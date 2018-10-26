using System;
using System.Diagnostics;
using System.Threading;
using AzizollahiCommons.Logging;
using AzizollahiCommons.Pooling;

namespace TestApp {
	class Program {
		static void Main(string[] args) {
			var pool = new AzizollahiServicePool("RequestPool",new ConsoleLogger("Test", LogLevel.Info));
			for (var i = 0; i < 1000; i++) {
				pool.Run(() => {
					var indx = i;
					Console.WriteLine($"Hello - {DateTime.Now: HH:mm:ss.ffff} - {indx}");
					Thread.Sleep(10);
				});
				if(i%10 == 0)
					Thread.Sleep(1000);
			}
			Console.ReadKey();
		}
	}
	public enum LogLevel{
		Trace,
		Debug,
		Info,
		Warn,
		Error,
		Fatal
	}
	public class ConsoleLogger : Logger {
		private string _name;
		private readonly LogLevel _logLevel;

		public ConsoleLogger(string name, LogLevel logLevel) {
			_name = name;
			_logLevel = logLevel;
		}
		public void Trace(string message) {
			if(_logLevel <= LogLevel.Trace)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Trace] {message}");
		}

		public void Debug(string message) {
			if(_logLevel <= LogLevel.Debug)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Debug] {message}");
		}

		public void Info(string message) {
			if(_logLevel <= LogLevel.Info)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Info] {message}");
		}

		public void Warn(string message) {
			if(_logLevel <= LogLevel.Warn)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Warn] {message}");
		}

		public void Error(string message) {
			if(_logLevel <= LogLevel.Error)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Error] {message}");
		}

		public void Fatal(string message) {
			if(_logLevel <= LogLevel.Fatal)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Fatal] {message}");
		}

		public void Trace(string message, Exception exception) {
			if(_logLevel <= LogLevel.Trace)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Trace] {message} {exception}");
		}

		public void Debug(string message, Exception exception) {
			if(_logLevel <= LogLevel.Debug)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Debug] {message} {exception}");
		}

		public void Info(string message, Exception exception) {
			if(_logLevel <= LogLevel.Info)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Info] {message} {exception}");
		}

		public void Warn(string message, Exception exception) {
			if(_logLevel <= LogLevel.Warn)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Warn] {message} {exception}");
		}

		public void Error(string message, Exception exception) {
			if(_logLevel <= LogLevel.Error)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Error] {message} {exception}");
		}

		public void Fatal(string message, Exception exception) {
			if(_logLevel <= LogLevel.Fatal)
				Console.WriteLine($"[{DateTime.Now}] [{_name}] [Fatal] {message} {exception}");
		}
	}
}