namespace KnowledgeManagement.Aggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize("ResourceOwnerPolicy")]
	public class QuestionCommentController:ControllerBase
	{
		private readonly IQuestionCommentService _questionService;

		public QuestionCommentController(IQuestionCommentService questionService)
		{
			_questionService = questionService;
		}
		[HttpGet("GetAllCommentsByQuestionId/{questionId}")]
		public async Task<ResponseDTO> GetAllCommentsByQuestionId(Guid questionId,int pageSize,int pageNumber)
		{
			return await _questionService.GetCommentByQuestionId(questionId,pageSize,pageNumber);
		}
	}
}
