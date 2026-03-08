using ControlPanel.ORGHierararchy.Protos;
using KnowledgeManagement.Aggregator.Dtos.Knowledge;

namespace KnowledgeManagement.Aggregator.ClientServices
{
    public class KnowledgeService : IKnowledgesService
    {
        private readonly KnowledgeProtoService.KnowledgeProtoServiceClient _knowledgeClient;
        private readonly OrgMembersProtoService.OrgMembersProtoServiceClient _orgMembersProto;
        private readonly IndexProtoService.IndexProtoServiceClient _indexProto;
        private readonly OrgProtoService.OrgProtoServiceClient _orgClient;
        private readonly UserProtoService.UserProtoServiceClient _userClient;
        private readonly IResponseHelper _responseHelper;
        private ResponseDTO _responseDTO;
        public Guid _loggedInUserId;

        public KnowledgeService(
            KnowledgeProtoService.KnowledgeProtoServiceClient knowledgeClient,
            OrgMembersProtoService.OrgMembersProtoServiceClient orgMembersProto, IResponseHelper responseHelper, IHttpContextAccessor _httpContextAccessor, IndexProtoService.IndexProtoServiceClient indexProto, UserProtoService.UserProtoServiceClient userClient, OrgProtoService.OrgProtoServiceClient orgClient)
        {
            _knowledgeClient = knowledgeClient;
            _orgMembersProto = orgMembersProto;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDTO();
            _loggedInUserId = Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
            _indexProto = indexProto;
            _userClient = userClient;
            _orgClient = orgClient;
        }


        public async Task<ResponseDTO> GetAllPrivateKnowledges(int pageNumber, int pageSize, string? searchTerm)
        {
            //var orgs = _orgMembersProto.GetOrgForMember(new RequestGetOrgForMember() { UserId = _loggedInUserId.ToString() });
            //if (orgs == null)
            //{
            //    return _responseHelper.NotFound("OrgsNotFound");
            //}
            //var orgIds = new IdProtoModel();
            //orgIds.Id.AddRange(orgs.ResponseALLManger.Select(x => x.OrgId));

            var knowledges = _knowledgeClient.GetAllPrivateKnowledges(new RequestGetAllPrivateKnowledge()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm ?? "",
                //OrgIds = orgIds
            });
            if (knowledges == null || knowledges.PrivateKnowles.Count ==0)
            {
                return _responseHelper.NotFound("knowledgesNotFound");
            }
            var knowledgeDto = new List<GetAllKnowledgeDTO>();
            foreach (var knowledge in knowledges.PrivateKnowles)
            {
                knowledgeDto.Add(new GetAllKnowledgeDTO()
                {
                    Id = knowledge.Id != null ? Guid.Parse(knowledge.Id) : Guid.Empty,
                    Name = knowledge.Name,
                    //CategoryId = knowledge.CategoryId != null ? Guid.Parse(knowledge.CategoryId) : Guid.Empty,
                    OrgHierarchyId = knowledge?.OrgHierarchyId?.Select(x => Guid.Parse(x)).ToList() ,
                    Color = knowledge.Color,
                    Icon = knowledge.Icon,
                    KnowledgeType = knowledge.KnowledgeType
                });
            }
            _responseDTO = _responseHelper.RetrievedSuccessfully(knowledgeDto, "KnowledgeIsRetrievedSuccessfully");
            _responseDTO.PageSize = knowledges.PageSize;
            _responseDTO.PageIndex = knowledges.PageIndex;
            _responseDTO.TotalPages = knowledges.TotalPages;
            _responseDTO.TotalItems = knowledges.TotalItems;

            return _responseDTO;
        }

        public async Task<ResponseDTO> GetUserAndIOrgIndicators()
        {
            var request = _indexProto.GetUserAndIOrgIndicators(new RequestGetUserAndIOrgIndicators());
            if( request.Model == null)
            {
                return _responseHelper.NotFound("NotFound");
            }
            //GET ALL USERS DATA
            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(request.Model?.UserInfoProto?.Select(x=> x.UserId));

            var allUserData = _userClient.GetListOfUsers(userIds);

            //GET ALL ORGANIZATION DATA
            var orgIds = new GetOrgHierarchysByIdsRequest();
            orgIds.OrgId.AddRange(request.Model?.OrgInfoProto?.Select(x => x.OrgId));

            var allOrgData = _orgClient.GetOrgHierarchysByIds(orgIds);
            #region map
            var response =  new UserAndIOrgIndicatorsDto()
            {
                CountOfInteractiveUsers = request.Model.CountOfInteractiveUsers ,
                Knowledges = request.Model.GetKnowledgeInfoProto.Select(kn => new GetKnowledgeInfo()
                {
                    KnowlegeId = Guid.TryParse( kn.KnowlegeId ,out var KnowlegeId) ? KnowlegeId : Guid.Empty ,
                    KnowlegeName = kn.KnowlegeName ??"",
                    CountOfOrgs = kn.CountOfOrgs ,
                    type = kn.Type,
                    
                }).ToList(),
                SubKnowledges = request.Model.GetSubKnowledgeInfoProto.Select(su => new GetSubKnowledgeInfo() 
                {
                    SubKnowledgeId = Guid.TryParse(su.SubKnowledgeId, out var suId) ? suId : Guid.Empty,
                    CountOfOrgs= su.CountOfOrgs ,
                    CountOfUsers = su.CountOfUsers ,
                    SubKnowledgeTitle = su.SubKnowledgeTitle??""
                }).ToList(),
                Users = request.Model.UserInfoProto.Select(u => new UserInfo() 
                {
                    UserId = Guid.TryParse(u.UserId, out var userId) ? userId : Guid.Empty,
                    UserFullName = allUserData.UserData.Where(x=> x.UserId.ToLower() ==u.UserId.ToLower() ).Select(x=> $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}").FirstOrDefault(),
                    CountOfContributions = u.CountOfContributions
                }).ToList(),
                Orgs = request.Model.OrgInfoProto.Select(o=> new OrgInfo()
                {
                    OrgId = Guid.TryParse(o.OrgId, out var orgId) ? orgId : Guid.Empty,
                    OrgNameAr = allOrgData.Orgs.Where(x=>x.Id.ToLower() == o.OrgId.ToLower()).Select(x=> x.NameAr).FirstOrDefault(),
                    OrgNameEn = allOrgData.Orgs.Where(x=>x.Id.ToLower() == o.OrgId.ToLower()).Select(x=> x.NameEn).FirstOrDefault(),
                    CountOfSubKnowledges = o.CountOfSubKnowledges
                }).ToList()
            };
            #endregion

            return _responseHelper.RetrievedSuccessfully(response, "RetrievedSuccessfully");

        }
    }
}
