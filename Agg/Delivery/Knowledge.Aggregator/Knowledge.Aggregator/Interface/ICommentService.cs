namespace KnowledgeManagement.Aggregator.Interface
{
	public interface ICommentService
	{
		public Task<ResponseDTO> GetAllCommentsByChallengeId(Guid challengeId,int pageSize, int pageIndex);
	}
}