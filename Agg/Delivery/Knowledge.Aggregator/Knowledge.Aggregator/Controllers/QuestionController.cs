namespace KnowledgeManagement.Aggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize("ResourceOwnerPolicy")]
	public class QuestionController:ControllerBase
	{
		private readonly IQuestionService _questionService;

		public QuestionController(IQuestionService questionService)
		{
			_questionService = questionService;
		}

		[HttpGet]
		public async Task<ResponseDTO> GetAllQuestions(int pageSize,int pageNumber,string searchTerm = "")
		{
			return await _questionService.GetAllQuestions(pageSize,pageNumber,searchTerm);
		}

		[HttpGet("{questionId}")]
		public async Task<ResponseDTO> GetQuestionById(Guid questionId)
		{
			return await _questionService.GetQuestionById(questionId);
		}
		
		[HttpGet("GetAllRequestedQuestions")]
		public async Task<ResponseDTO> GetAllRequestedQuestions(int pageSize,int pageNumber,int requestedStage = 0)
		{
			return await _questionService.GetAllRequestedQuestions(pageSize,pageNumber,requestedStage);
		}

		[HttpGet("GetRequestedQuestionById/{questionId}")]
		public async Task<ResponseDTO> GetRequestedQuestionById(Guid questionId)
		{
			return await _questionService.GetRequestedQuestionById(questionId);
		}


        [HttpGet("GetAllMyQuestions")]
        public async Task<ResponseDTO> GetAllMyQuestions(int pageSize, int pageNumber, string searchTerm = "")
        {
            return await _questionService.GetAllMyQuestions(pageSize, pageNumber, searchTerm);
        }


        [HttpGet("MyOutGoingQuestions")]
        public async Task<ResponseDTO> OutGoingQuestions(int pageSize, int pageNumber, string searchTerm = "")
        {
            return await _questionService.OutGoingQuestions(pageSize, pageNumber, searchTerm);

        }

        [HttpGet("MyIncomingQuestions")]
        public async Task<ResponseDTO> IncomingQuestions(int pageSize, int pageNumber, string searchTerm = "")
        {
            return await _questionService.IncomingQuestions(pageSize, pageNumber, searchTerm);
        }

        [HttpGet("GetQuestionsByExpertId")]
        public async Task<ResponseDTO> GetQuestionsByExpertId(int pageSize, int pageNumber, Guid expertId)
        {
            return await _questionService.GetQuestionsByExpertId(pageSize, pageNumber, expertId);
        }
    }
}
