namespace KnowledgeManagement.Aggregator.Interface
{
	public interface IChallengeService
	{
		public Task<ResponseDTO> GetAllChallenges(int pageSize,int pageNumber,string searchTerm);
		public Task<ResponseDTO> GetChallengeById(Guid challengeId);
		public Task<ResponseDTO> GetAllRequestedChallenges(int pageSize,int pageNumber,int requestStage);
		public Task<ResponseDTO> GetRequestedChallengeById(Guid challengeId);

	}
}
