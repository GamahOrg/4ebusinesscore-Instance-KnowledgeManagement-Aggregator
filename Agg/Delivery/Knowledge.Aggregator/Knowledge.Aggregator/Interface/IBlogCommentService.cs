namespace KnowledgeManagement.Aggregator.Interface
{
	public interface IBlogCommentService
	{
		public Task<ResponseDTO> GetCommentByBlogId(Guid blogId,int pageSize,int pageIndex);
	}
}
