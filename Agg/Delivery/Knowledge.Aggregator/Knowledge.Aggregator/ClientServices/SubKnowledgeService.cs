namespace KnowledgeManagement.Aggregator.ClientServices
{
	public class SubKnowledgeService:ISubKnowledgeService
	{
		private readonly SubKnowledgeProtoService.SubKnowledgeProtoServiceClient _subKnowledgeClient;
		private readonly OrgProtoService.OrgProtoServiceClient _orgClient;
		private readonly UserProtoService.UserProtoServiceClient _userClient;
		private readonly IResponseHelper _responseHelper;
		private ResponseDTO _responseDTO;


		public SubKnowledgeService(
			IResponseHelper responseHelper,
			SubKnowledgeProtoService.SubKnowledgeProtoServiceClient subKnowledgeClient,
			OrgProtoService.OrgProtoServiceClient orgClient,
			UserProtoService.UserProtoServiceClient userClient)
		{
			_responseHelper = responseHelper;
			_responseDTO = new ResponseDTO();
			_subKnowledgeClient = subKnowledgeClient;
			_orgClient = orgClient;
			_userClient = userClient;
		}
		public async Task<ResponseDTO> GetAllSubKnowledgeByKnowledgeId(Guid knowledgeId,int pageSize,int pageNumber,string title,string taggedWords,DateOnly? createdOn,string subKnowledgeId,string subKnowledgeCategoryId,
			string knowledgeCategoryId,string orgHierarchyId , DateOnly? date , int knowledgeType)
		{
			var allSubKnowledges = _subKnowledgeClient.GetAllSubKnowledgesByKnowledgeId(new RequestAllSubKnowledge
			{
				KnowledgeId = knowledgeId.ToString(),
				PageIndex = pageNumber,
				PageSize = pageSize,
				Title = title,
				TaggedWords = taggedWords,
				CreatedOn = createdOn.ToString() ?? "",
				SubKnowledgeId = subKnowledgeId,
				//KnowledgeCategoryId = knowledgeCategoryId,
				//SubKnowledgeCategoryId = subKnowledgeCategoryId,
				OrgHierarchyId = orgHierarchyId,
				Date = date.ToString() ?? "",
				KnowledgeType = knowledgeType ,

			});

			var orgIds = new GetOrgHierarchysByIdsRequest();
			orgIds.OrgId.AddRange(allSubKnowledges?.SubKnowledge?.Select(x => x.OrgHierarchyId).Distinct());

			var allOrgData = _orgClient.GetOrgHierarchysByIds(orgIds);

			var allSubKnowledgeDto = allSubKnowledges?.SubKnowledge?.Select(c => SubKnowledgeMapper.FromAllSubKnowledgeProto(c,allOrgData)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allSubKnowledgeDto,"allSubKnowledgeIsRetrievedSuccessfully");
			_responseDTO.PageSize = allSubKnowledges.PageSize;
			_responseDTO.PageIndex = allSubKnowledges.PageIndex;
			_responseDTO.TotalPages = allSubKnowledges.TotalPages;
			_responseDTO.TotalItems = allSubKnowledges.TotalItems;

			return _responseDTO;
		}


		public async Task<ResponseDTO> GetSubKnowledgeById(Guid subKnowledgeId)
		{
			var subKnowledge = _subKnowledgeClient.GetSubKnowledgesById(new RequestSubKnowledgeById { SubKnowledgeId = subKnowledgeId.ToString() });

			if(subKnowledge.SubKnowledge == null)
			{
				return _responseHelper.NotFound("SubKnowledgeIsNotFound");
			}

			//GET ALL USERS DATA
			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(subKnowledge?.SubKnowledge?.WorkParticipants);

			var allUserData = _userClient.GetListOfUsers(userIds);

			//GET ALL ORGANIZATION DATA
			var orgIds = new GetOrgHierarchysByIdsRequest();
			orgIds.OrgId.AddRange(subKnowledge?.SubKnowledge?.ParticipatingParties);

			var allOrgData = _orgClient.GetOrgHierarchysByIds(orgIds);

			//GET OWNER ORGANIZATION IF EXISTS
			var orgId = new GetOrgHierarchyByIdRequest();
			orgId.OrgId = subKnowledge?.SubKnowledge?.OrgHierarchyId ?? default(Guid).ToString();

			var orgData = _orgClient.GetOrgHierarchyById(orgId);

			var subKnowledgeMapped = SubKnowledgeMapper.FromSubKnowledgeById(subKnowledge,allOrgData,allUserData,orgData);

			return _responseHelper.RetrievedSuccessfully(subKnowledgeMapped,"subKnowledgeIsRetrievedSuccessfully");
		}
	}
}
