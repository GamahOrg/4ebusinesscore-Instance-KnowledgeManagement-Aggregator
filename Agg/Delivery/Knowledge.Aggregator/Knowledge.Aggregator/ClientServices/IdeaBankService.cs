using ControlPanel.ORGHierararchy.Protos;

namespace KnowledgeManagement.Aggregator.ClientServices
{
    public class IdeaBankService : IIdeaBankService
    {
        private readonly IdeaBankCommentsProtoService.IdeaBankCommentsProtoServiceClient _ideaBankClient;
        private readonly UserProtoService.UserProtoServiceClient _userClient;
        private readonly IResponseHelper _responseHelper;
        private ResponseDTO _responseDTO;
        private readonly OrgMembersProtoService.OrgMembersProtoServiceClient _orgMembersProtoService;

        public IdeaBankService
        (
            IdeaBankCommentsProtoService.IdeaBankCommentsProtoServiceClient ideaBankClient,
            UserProtoService.UserProtoServiceClient userClient,
            IResponseHelper responseHelper
,
            OrgMembersProtoService.OrgMembersProtoServiceClient orgMembersProtoService)
        {
            _ideaBankClient = ideaBankClient;
            _userClient = userClient;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDTO();
            _orgMembersProtoService = orgMembersProtoService;
        }

        public async Task<ResponseDTO> GetAllIdeaBankComments(Guid ideaBankId,int pageSize,int pageNumber)
		{
            var allIdeaBankComments = _ideaBankClient.GetAllIdeaBankComments(new RequestAllIdeaBankComments
            {
                Id = ideaBankId.ToString(),
                PageIndex = pageNumber,
                PageSize = pageSize,
            });

            if (allIdeaBankComments == null || allIdeaBankComments.ResponseIdeaBankComments == null)
            {
                return _responseHelper.NotFound("Error retrieving idea bank comments.");
            }

            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(allIdeaBankComments?.ResponseIdeaBankComments?.Select(x => x.CreatedBy).Distinct());

            var allUserData = _userClient.GetListOfUsers(userIds);

            var allIdeaBankCommentsDto = allIdeaBankComments?.ResponseIdeaBankComments?.Select(c =>
            {
                var ideaCommentsDto = new GetAllIdeaCommentsDTO
                {
                    Id = Guid.TryParse(c.Id, out Guid parsedId) ? parsedId : default(Guid),
                    CommentText = c.CommentText ?? "",
                    CreatedOn = DateTime.TryParse(c.CreatedOn, out DateTime dateTime) ? dateTime : default(DateTime),
                    CreatedBy = Guid.TryParse(c.CreatedBy, out Guid parsedUserId) ? parsedUserId : default(Guid),
                    IdeaBankId = Guid.TryParse(c.IdeaBankId, out Guid parsedIdeaBankId) ? parsedIdeaBankId : default(Guid),
                    ParentCommentId = Guid.TryParse(c.ParentCommentId, out Guid parsedParentCommentId) ? parsedParentCommentId : (Guid?)null,
                    Replies = MapReplies(c.IdeaBankComments, allUserData),
                    FirstName = GetUserProperty(c.CreatedBy, user => user.FirstName),
                    SecondName = GetUserProperty(c.CreatedBy, user => user.SecondName),
                    ThirdName = GetUserProperty(c.CreatedBy, user => user.ThirdName),
                    FourthName = GetUserProperty(c.CreatedBy, user => user.FourthName),
                    UserImgUrl = GetUserProperty(c.CreatedBy, user => user.ImgUrl)
                };

                return ideaCommentsDto;
            }).ToList();


            _responseDTO = _responseHelper.RetrievedSuccessfully(allIdeaBankCommentsDto, "allIdeaBankCommentsRetrievedSuccessfully");
            _responseDTO.PageSize = allIdeaBankComments.PageSize;
            _responseDTO.PageIndex = allIdeaBankComments.PageIndex;
            _responseDTO.TotalPages = allIdeaBankComments.TotalPages;
            _responseDTO.TotalItems = allIdeaBankComments.TotalItems;

            return _responseDTO;
        }
        private GetAllIdeaCommentsDTO MapToDTO(IdeaBankCommentsProto c, ResponseUserDataList allUserData)
        {
            var ideaCommentsDto = new GetAllIdeaCommentsDTO
            {
                Id = Guid.TryParse(c.Id, out Guid parsedId) ? parsedId : default(Guid),
                CommentText = c.CommentText ?? "",
                CreatedOn = DateTime.TryParse(c.CreatedOn, out DateTime dateTime) ? dateTime : default(DateTime),
                CreatedBy = Guid.TryParse(c.CreatedBy, out Guid parsedUserId) ? parsedUserId : default(Guid),
                IdeaBankId = Guid.TryParse(c.IdeaBankId, out Guid parsedIdeaBankId) ? parsedIdeaBankId : default(Guid),
                ParentCommentId = Guid.TryParse(c.ParentCommentId, out Guid parsedParentCommentId) ? parsedParentCommentId : (Guid?)null,
                Replies = MapReplies(c.IdeaBankComments, allUserData),
                CommentLikesCount = c.CommentLikesCount > 0 ? c.CommentLikesCount : 0,
                FirstName = GetUserProperty(c.CreatedBy, user => user.FirstName) ?? "",
                SecondName = GetUserProperty(c.CreatedBy, user => user.SecondName) ?? "",
                ThirdName = GetUserProperty(c.CreatedBy, user => user.ThirdName) ?? "",
                FourthName = GetUserProperty(c.CreatedBy, user => user.FourthName) ?? "",
                UserImgUrl = GetUserProperty(c.CreatedBy, user => user.ImgUrl) ?? ""
            };

            return ideaCommentsDto;
        }
        private List<GetAllIdeaCommentsDTO> MapReplies(IEnumerable<IdeaBankCommentsProto> replies, ResponseUserDataList allUserData)
        {
            if (replies == null)
            {
                return null;
            }

            return replies.Select(reply => MapToDTO(reply, allUserData)).ToList();
        }

        private string GetUserProperty(string userId, Func<ResponseGetUserById, string> propertySelector)
        {
            var userResponse = _userClient.GetListOfUsers(new RequestedUserList
            {
                UserId = { userId }
            });

            var user = userResponse?.UserData?.FirstOrDefault();

            return user != null ? propertySelector(user) : "";
        }

        public async Task<ResponseDTO> GetAllRequestedIdeas(int pageSize,int pageNumber,int requestStage)
		{
			var allRequestedIdea = _ideaBankClient.GetAllRequestedIdeas(new RequestAllRequestedIdeas
			{
				PageIndex = pageNumber,
				PageSize = pageSize,
				RequestStage = requestStage
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allRequestedIdea?.Idea?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			var allIdeasDto = allRequestedIdea?.Idea?.Select(c => IdeaMapper.FromAllRequestedIdeaProto(c,allUserData)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allIdeasDto,"allRequestedIdeaIsRetrievedSuccessfully");
			_responseDTO.PageSize = allRequestedIdea.PageSize;
			_responseDTO.PageIndex = allRequestedIdea.PageIndex;
			_responseDTO.TotalPages = allRequestedIdea.TotalPages;
			_responseDTO.TotalItems = allRequestedIdea.TotalItems;

			return _responseDTO;
		}

		public async Task<ResponseDTO> GetRequestedIdeaById(Guid ideaId)
		{
			var requestedIdea = _ideaBankClient.GetRequestedIdeaById(new RequestIdeaById { IdeaId = ideaId.ToString() });

			if(requestedIdea.Idea == null)
			{
				return _responseHelper.NotFound("ideaIsNotFound");
			}

			var requestedUserId = new RequestGetUserById();
			requestedUserId.UserId = requestedIdea?.Idea?.CreatedBy;

			var userData = _userClient.GetUserById(requestedUserId);

			var ideaMapped = IdeaMapper.FromRequestedIdeaById(requestedIdea,userData);

			return _responseHelper.RetrievedSuccessfully(ideaMapped,"ideaIsRetrievedSuccessfully");
		}

        public async Task<ResponseDTO> GetAllIdeasPaginated(int pageSize, int pageNumber)
        {
            var ideas =_ideaBankClient.GetAllIdeaBanks(new RequestGetAllIdeaBanks
            {
                PageIndex = pageNumber,
                PageSize = pageSize,
            });
            if(ideas.Ideabanks.Count ==0 || ideas.Ideabanks == null)
           {
                _responseDTO = _responseHelper.NotFound( "IdeasNotFound");
            }
            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(ideas?.Ideabanks.Select(x => x.CreatedBy).Distinct());

            var allUserData = _userClient.GetListOfUsers(userIds);
            var UsersIds = new RequestGetOrgForMembers();
            UsersIds.UserId.AddRange(allUserData.UserData.Select(a => a.UserId).Distinct());
            var ideasDto = ideas?.Ideabanks?.Select(c => IdeaMapper.FromAllIdeasProto(c, allUserData)).ToList();



            _responseDTO = _responseHelper.RetrievedSuccessfully(ideasDto, "allQuestionsIsRetrievedSuccessfully");
            _responseDTO.PageSize = ideas.PageSize;
            _responseDTO.PageIndex = ideas.PageIndex;
            _responseDTO.TotalPages = ideas.TotalPages;
            _responseDTO.TotalItems = ideas.TotalItems;

            return _responseDTO;
        }

        public async Task<ResponseDTO> GetIdeaBankById(Guid id)
        {
            var idea = _ideaBankClient.GetIdeaBankById(new RequestGetIdeaBankById { Id = id.ToString() });

            if (idea.IdeabankProtoModel == null)
            {
                return _responseHelper.NotFound("IdeaBankIsNotFound");
            }

            var requestedUserId = new RequestGetUserById();
            requestedUserId.UserId = idea?.IdeabankProtoModel?.CreatedBy;

            var userData = _userClient.GetUserById(requestedUserId);

            var questionMapped = IdeaMapper.FromGetIdeaByIdProto(idea.IdeabankProtoModel, userData);

            return _responseHelper.RetrievedSuccessfully(questionMapped, "QuestionIsRetrievedSuccessfully");
        }
    }
}
