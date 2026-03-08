namespace KnowledgeManagement.Aggregator.Interface
{
	public interface IQuestionCommentService
	{
		public Task<ResponseDTO> GetCommentByQuestionId(Guid questionId,int pageSize,int pageIndex);
	}
}
