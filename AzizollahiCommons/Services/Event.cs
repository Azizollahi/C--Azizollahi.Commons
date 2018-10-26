namespace AzizollahiCommons.Services {
	public interface Event<T> {
		T Wait();
		void Notify(T input);
		bool IsWaiting();
	}
}