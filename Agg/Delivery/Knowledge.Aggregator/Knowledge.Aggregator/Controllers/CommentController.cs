namespace KnowledgeManagement.Aggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize("ResourceOwnerPolicy")]
	public class CommentController:ControllerBase
	{
		private readonly ICommentService _commentService;

		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}
		[HttpGet("GetAllCommentsByChallengeId/{challengeId}")]
		public async Task<ResponseDTO> GetAllCommentsByChallengeId(Guid challengeId,int pageSize,int pageNumber)
		{
			return await _commentService.GetAllCommentsByChallengeId(challengeId,pageSize,pageNumber);
		}
	}
}
