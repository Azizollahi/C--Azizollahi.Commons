namespace AzizollahiCommons.Services {
	public interface EventAgent<T> {
		T Run(T input);
	}
}