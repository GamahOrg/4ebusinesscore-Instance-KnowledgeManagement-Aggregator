namespace KnowledgeManagement.Aggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize("ResourceOwnerPolicy")]
	public class BlogController:ControllerBase
	{
		private readonly IBlogService _blogService;

		public BlogController(IBlogService blogService)
		{
			_blogService = blogService;
		}

		[HttpGet]
		public async Task<ResponseDTO> GetAllBlogs(int pageSize,int pageNumber,string searchTerm = "")
		{
			return await _blogService.GetAllBlogs(pageSize,pageNumber,searchTerm);
		}

		[HttpGet("{blogId}")]
		public async Task<ResponseDTO> GetBlogById(Guid blogId)
		{
			return await _blogService.GetBlogById(blogId);
		}
		
		[HttpGet("GetAllRequestedBlogs")]
		public async Task<ResponseDTO> GetAllRequestedBlogs(int pageSize,int pageNumber,int requestedStage = 0)
		{
			return await _blogService.GetAllRequestedBlogs(pageSize,pageNumber,requestedStage);
		}

		[HttpGet("GetRequestedBlogById/{blogId}")]
		public async Task<ResponseDTO> GetRequestedBlogById(Guid blogId)
		{
			return await _blogService.GetRequestedBlogById(blogId);
		}
	}
}
