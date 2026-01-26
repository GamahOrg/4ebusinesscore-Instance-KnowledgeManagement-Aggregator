namespace KnowledgeManagement.Aggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize("ResourceOwnerPolicy")]
	public class SubKnowledgeController:ControllerBase
	{
		private readonly ISubKnowledgeService _subKnowledgeService;

		public SubKnowledgeController(ISubKnowledgeService subKnowledgeService)
		{
			_subKnowledgeService = subKnowledgeService;
		}

		[HttpGet("GetAllSubKnowledges")]
		public async Task<ResponseDTO> GetAllSubKnowledgeByKnowledgeId(Guid knowledgeId,int pageSize,int pageNumber,
			string title = "",
			string taggedWords = "",
			DateOnly? createdOn = null,
			string subKnowledgeId = "",
			string subKnowledgeCategoryId = "",
			string knowledgeCategoryId = "",
			string orgHierarchyId = "" , 
			DateOnly? date = null,
			int knowledgeType =-1
            )
		{
			return await _subKnowledgeService.GetAllSubKnowledgeByKnowledgeId(knowledgeId,pageSize,pageNumber,title,taggedWords,createdOn,subKnowledgeId,subKnowledgeCategoryId,knowledgeCategoryId,orgHierarchyId ,date , knowledgeType);
		}

		[HttpGet("{subKnowledgeId}")]
		public async Task<ResponseDTO> GetSubKnowledgeById(Guid subKnowledgeId)
		{
			return await _subKnowledgeService.GetSubKnowledgeById(subKnowledgeId);
		}
	}
}
