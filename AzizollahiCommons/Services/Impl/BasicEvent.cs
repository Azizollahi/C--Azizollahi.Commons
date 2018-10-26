using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace AzizollahiCommons.Services.Impl {
	public class BasicEvent<T> : Event<T> {
		private readonly Semaphore _dataSemaphore;
		private bool _isReady;
		private readonly Queue<T> _queue;
		private bool _isWaiting;
		public BasicEvent(int queueSize = 200) {
			_isWaiting = true;
			_queue = new Queue<T>();
			_dataSemaphore = new Semaphore(0,queueSize);
			_isReady = false;
		}

		public T Wait() {
			_isWaiting = true;
			if (_queue.Count > 0)
				return _queue.Dequeue();
			_isReady = true;
			_dataSemaphore.WaitOne();
			return _queue.Dequeue();
		}

		public void Notify(T input) {
			_queue.Enqueue(input);
			if(_isReady)
				_dataSemaphore.Release();
			_isReady = false;
			_isWaiting = false;
		}

		public bool IsWaiting() {
			return _isWaiting;
		}
	}
}