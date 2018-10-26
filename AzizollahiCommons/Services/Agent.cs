namespace AzizollahiCommons.Services {
	public interface Agent<I,O> {
		O Run(I data);
	}
}