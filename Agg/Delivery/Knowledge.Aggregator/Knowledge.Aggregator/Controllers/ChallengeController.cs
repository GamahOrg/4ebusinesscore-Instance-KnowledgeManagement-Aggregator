namespace KnowledgeManagement.Aggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize("ResourceOwnerPolicy")]
	public class ChallengeController:ControllerBase
	{
		private readonly IChallengeService _challengeService;

		public ChallengeController(IChallengeService challengeService)
		{
			_challengeService = challengeService;
		}

		[HttpGet]
		public async Task<ResponseDTO> GetAllChallenges(int pageSize,int pageNumber, string searchTerm = "")
		{
			return await _challengeService.GetAllChallenges(pageSize,pageNumber,searchTerm);
		}

		[HttpGet("{challengeId}")]
		public async Task<ResponseDTO> GetChallengeById(Guid challengeId)
		{
			return await _challengeService.GetChallengeById(challengeId);
		}

		[HttpGet("GetAllRequestedChallenges")]
		public async Task<ResponseDTO> GetAllRequestedChallenges(int pageSize,int pageNumber,int requestedStage = 0)
		{
			return await _challengeService.GetAllRequestedChallenges(pageSize,pageNumber,requestedStage);
		}

		[HttpGet("GetRequestedChallengeById/{challengeId}")]
		public async Task<ResponseDTO> GetRequestedChallengeById(Guid challengeId)
		{
			return await _challengeService.GetRequestedChallengeById(challengeId);
		}
	}
}
