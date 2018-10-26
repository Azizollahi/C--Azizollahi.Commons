using System.Threading;
using AzizollahiCommons.Services;

namespace AzizollahiCommons.Test {
	public class ListeningEvent<T> : Event<T> {
		private T _info;
		private Semaphore _semaphore;
		public ListeningEvent() {
			_semaphore = new Semaphore(0,1);
		}

		public T Wait() {
			_semaphore.WaitOne();
			return _info;
		}

		public void Notify(T input) {
			_info = input;
			_semaphore.Release();
		}

		public bool IsWaiting() {
			return false;
		}
	}
}