namespace KnowledgeManagement.Aggregator.ServerServices
{
	//[Authorize("ClientCredentialsPolicy")]
	public class BlogServerService : BlogProtoService.BlogProtoServiceBase
	{
		private readonly BlogProtoService.BlogProtoServiceClient _blogClient;

		public BlogServerService(BlogProtoService.BlogProtoServiceClient blogClient)
		{
			_blogClient = blogClient;
		}

		public async override Task<GetBlogControllerAndActionNameResponse> GetBlogControllerAndActionName(Empty request,ServerCallContext context)
		{
			var blogPermissions = await _blogClient.GetBlogControllerAndActionNameAsync(request);
			return await Task.FromResult(blogPermissions);
		}
	}
}
