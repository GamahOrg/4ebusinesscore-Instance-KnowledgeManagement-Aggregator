namespace KnowledgeManagement.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeaBankController : ControllerBase
    {
        private readonly IIdeaBankService _ideaBankService;

        public IdeaBankController(IIdeaBankService ideaBankService)
        {
            _ideaBankService = ideaBankService;
        }

        /*[HttpGet]
        [Route("GetAllIdeaComments/{ideaBankId}")]
        public async Task<ResponseDTO> ReturnedAllIdeaBankComments(Guid ideaBankId, int pageSize, int pageNumber)
        {
            return await _ideaBankService.GetAllIdeaBankComments(ideaBankId, pageSize, pageNumber);
        }*/
		[HttpGet("GetAllRequestedIdeas")]
		public async Task<ResponseDTO> GetAllRequestedIdeas(int pageSize,int pageNumber,int requestedStage = 0)
		{
			return await _ideaBankService.GetAllRequestedIdeas(pageSize,pageNumber,requestedStage);
		}

		[HttpGet("GetRequestedIdeaById/{ideaId}")]
		public async Task<ResponseDTO> GetRequestedIdeaById(Guid ideaId)
		{
			return await _ideaBankService.GetRequestedIdeaById(ideaId);
		}

        [HttpGet]
        [Route("GetAllIdeasPaginated")]
        public async Task<ResponseDTO> GetAllIdeasPaginated(int pageNumber, int pageSize)
        {

            return await _ideaBankService.GetAllIdeasPaginated(pageSize, pageNumber);
        }

        // GetById
        [HttpGet, Route("GetIdeaDetailsById/{ideaId}")]
        public async Task<ResponseDTO> GetIdeaDetailsId(Guid ideaId) =>  await _ideaBankService.GetIdeaBankById(ideaId);
    }
}
