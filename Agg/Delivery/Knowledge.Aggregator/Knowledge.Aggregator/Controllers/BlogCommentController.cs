namespace KnowledgeManagement.Aggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize("ResourceOwnerPolicy")]
	public class BlogCommentController:ControllerBase
	{
		private readonly IBlogCommentService _commentService;

		public BlogCommentController(IBlogCommentService commentService)
		{
			_commentService = commentService;
		}
		[HttpGet("GetAllCommentsByBlogId/{blogId}")]
		public async Task<ResponseDTO> GetAllCommentsByBlogId(Guid blogId,int pageSize,int pageNumber)
		{
			return await _commentService.GetCommentByBlogId(blogId,pageSize,pageNumber);
		}
	}
}
