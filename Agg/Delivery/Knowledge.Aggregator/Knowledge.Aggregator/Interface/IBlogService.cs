namespace KnowledgeManagement.Aggregator.Interface
{
	public interface IBlogService
	{
		public Task<ResponseDTO> GetAllBlogs(int pageSize,int pageNumber,string searchTerm);
		public Task<ResponseDTO> GetBlogById(Guid blogId);
		public Task<ResponseDTO> GetAllRequestedBlogs(int pageSize,int pageNumber,int requestStage);
		public Task<ResponseDTO> GetRequestedBlogById(Guid blogId);
	}
}
