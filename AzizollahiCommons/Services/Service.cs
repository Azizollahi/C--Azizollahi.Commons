namespace AzizollahiCommons.Services {
	public interface Service {
		void Start();
		void Stop();
		void Pause();
		void Resume();
		bool IsAlive();
		bool IsPaused();
	}
}