namespace KnowledgeManagement.Aggregator.Interface
{
	public interface ITokenService
	{
		Task<CallOptions> AcquireToken();
		Task<CallOptions> GetAccessTokenAsync();
	}
}
