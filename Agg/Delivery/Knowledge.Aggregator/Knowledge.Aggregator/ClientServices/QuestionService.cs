using ControlPanel.ORGHierararchy.Protos;

namespace KnowledgeManagement.Aggregator.ClientServices
{
	public class QuestionService:IQuestionService
	{
		private readonly QuestionProtoService.QuestionProtoServiceClient _questionClient;
		private readonly UserProtoService.UserProtoServiceClient _userClient;
		private readonly IResponseHelper _responseHelper;
		private ResponseDTO _responseDTO;
		private readonly OrgMembersProtoService.OrgMembersProtoServiceClient _orgMembersProtoService;

        public QuestionService(
			IResponseHelper responseHelper,
			QuestionProtoService.QuestionProtoServiceClient questionClient,
			UserProtoService.UserProtoServiceClient userClient, OrgMembersProtoService.OrgMembersProtoServiceClient orgMembersProtoService)
		{
			_responseHelper = responseHelper;
			_responseDTO = new ResponseDTO();
			_questionClient = questionClient;
			_userClient = userClient;
			_orgMembersProtoService = orgMembersProtoService;
		}
		public async Task<ResponseDTO> GetAllQuestions(int pageSize,int pageNumber,string searchTerm)
		{
			var allQuestion = _questionClient.GetAllQuestions(new RequestAllQuestions
			{
				PageIndex = pageNumber,
				PageSize = pageSize,
				SearchTerm = searchTerm
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allQuestion?.Question?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);
            var UsersIds = new RequestGetOrgForMembers();
            UsersIds.UserId.AddRange(allUserData.UserData.Select(a => a.UserId).Distinct());

            var Orgs = await _orgMembersProtoService.GetOrgForMembersAsync(UsersIds);
            var allQuestionsDto = allQuestion?.Question?.Select(c => QuestionMapper.FromAllQuestionProto(c,allUserData, Orgs)).ToList();
			


            _responseDTO = _responseHelper.RetrievedSuccessfully(allQuestionsDto,"allQuestionsIsRetrievedSuccessfully");
			_responseDTO.PageSize = allQuestion.PageSize;
			_responseDTO.PageIndex = allQuestion.PageIndex;
			_responseDTO.TotalPages = allQuestion.TotalPages;
			_responseDTO.TotalItems = allQuestion.TotalItems;

			return _responseDTO;
		}

		public async Task<ResponseDTO> GetAllRequestedQuestions(int pageSize,int pageNumber,int requestStage)
		{
			var allRequestedQuestion = _questionClient.GetAllRequestedQuestions(new RequestAllRequestedQuestions
			{
				PageIndex = pageNumber,
				PageSize = pageSize,
				RequestStage = requestStage
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allRequestedQuestion?.Question?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			var allQuestionsDto = allRequestedQuestion?.Question?.Select(c => QuestionMapper.FromAllRequestedQuestionProto(c,allUserData)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allQuestionsDto,"allRequestedQuestionIsRetrievedSuccessfully");
			_responseDTO.PageSize = allRequestedQuestion.PageSize;
			_responseDTO.PageIndex = allRequestedQuestion.PageIndex;
			_responseDTO.TotalPages = allRequestedQuestion.TotalPages;
			_responseDTO.TotalItems = allRequestedQuestion.TotalItems;

			return _responseDTO;
		}

		public async Task<ResponseDTO> GetQuestionById(Guid questionId)
		{
			var question = _questionClient.GetQuestionById(new RequestQuestionById { QuestionId = questionId.ToString() });

			if(question.Question == null)
			{
				return _responseHelper.NotFound("QuestionIsNotFound");
			}

			var requestedUserId = new RequestGetUserById();
			requestedUserId.UserId = question?.Question?.CreatedBy;

			var userData = _userClient.GetUserById(requestedUserId);

			var questionMapped = QuestionMapper.FromQuestionById(question,userData);

			return _responseHelper.RetrievedSuccessfully(questionMapped,"QuestionIsRetrievedSuccessfully");
		}

		public async Task<ResponseDTO> GetRequestedQuestionById(Guid questionId)
		{
			var requestedQuestion = _questionClient.GetRequestedQuestionById(new RequestQuestionById { QuestionId = questionId.ToString() });

			if(requestedQuestion.Question == null)
			{
				return _responseHelper.NotFound("QuestionIsNotFound");
			}

			var requestedUserId = new RequestGetUserById();
			requestedUserId.UserId = requestedQuestion?.Question?.CreatedBy;

			var userData = _userClient.GetUserById(requestedUserId);

			var questionMapped = QuestionMapper.FromRequestedQuestionById(requestedQuestion,userData);

			return _responseHelper.RetrievedSuccessfully(questionMapped,"QuestionIsRetrievedSuccessfully");
		}

        public async Task<ResponseDTO> GetAllMyQuestions(int pageSize, int pageNumber, string searchTerm)
        {
            var allQuestion = _questionClient.GetAllMyQuestions(new RequestAllQuestions
            {
                PageIndex = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm
            });

            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(allQuestion?.Question?.Select(x => x.CreatedBy).Distinct());

            var allUserData = _userClient.GetListOfUsers(userIds);
            var UsersIds = new RequestGetOrgForMembers();
            UsersIds.UserId.AddRange(allUserData.UserData.Select(a => a.UserId).Distinct());

            var Orgs = await _orgMembersProtoService.GetOrgForMembersAsync(UsersIds);
            var allQuestionsDto = allQuestion?.Question?.Select(c => QuestionMapper.FromAllQuestionProto(c, allUserData, Orgs)).ToList();



            _responseDTO = _responseHelper.RetrievedSuccessfully(allQuestionsDto, "allQuestionsIsRetrievedSuccessfully");
            _responseDTO.PageSize = allQuestion.PageSize;
            _responseDTO.PageIndex = allQuestion.PageIndex;
            _responseDTO.TotalPages = allQuestion.TotalPages;
            _responseDTO.TotalItems = allQuestion.TotalItems;

            return _responseDTO;
        }

        public async Task<ResponseDTO>  OutGoingQuestions(int pageSize, int pageNumber, string searchTerm)
        {
            var allQuestion = _questionClient.OutGoingQuestions(new RequestAllQuestions
            {
                PageIndex = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm
            });

            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(allQuestion?.Question?.Select(x => x.CreatedBy).Distinct());

            var allUserData = _userClient.GetListOfUsers(userIds);
            var UsersIds = new RequestGetOrgForMembers();
            UsersIds.UserId.AddRange(allUserData.UserData.Select(a => a.UserId).Distinct());

            var Orgs = await _orgMembersProtoService.GetOrgForMembersAsync(UsersIds);
            var allQuestionsDto = allQuestion?.Question?.Select(c => QuestionMapper.FromAllQuestionProto(c, allUserData, Orgs)).ToList();



            _responseDTO = _responseHelper.RetrievedSuccessfully(allQuestionsDto, "allQuestionsIsRetrievedSuccessfully");
            _responseDTO.PageSize = allQuestion.PageSize;
            _responseDTO.PageIndex = allQuestion.PageIndex;
            _responseDTO.TotalPages = allQuestion.TotalPages;
            _responseDTO.TotalItems = allQuestion.TotalItems;

            return _responseDTO;
        }

        public async Task<ResponseDTO> IncomingQuestions(int pageSize, int pageNumber, string searchTerm)
        {
             var allQuestion = _questionClient.IncomingQuestions(new RequestAllQuestions
            {
                PageIndex = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm
            });

            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(allQuestion?.Question?.Select(x => x.CreatedBy).Distinct());

            var allUserData = _userClient.GetListOfUsers(userIds);
            var UsersIds = new RequestGetOrgForMembers();
            UsersIds.UserId.AddRange(allUserData.UserData.Select(a => a.UserId).Distinct());

            var Orgs = await _orgMembersProtoService.GetOrgForMembersAsync(UsersIds);
            var allQuestionsDto = allQuestion?.Question?.Select(c => QuestionMapper.FromAllQuestionProto(c, allUserData, Orgs)).ToList();



            _responseDTO = _responseHelper.RetrievedSuccessfully(allQuestionsDto, "allQuestionsIsRetrievedSuccessfully");
            _responseDTO.PageSize = allQuestion.PageSize;
            _responseDTO.PageIndex = allQuestion.PageIndex;
            _responseDTO.TotalPages = allQuestion.TotalPages;
            _responseDTO.TotalItems = allQuestion.TotalItems;

            return _responseDTO;
        }

        public async Task<ResponseDTO> GetQuestionsByExpertId(int pageSize, int pageNumber, Guid expertId)
        {
            var allQuestion = _questionClient.GetQuestionsByExpertId(new RequestGetQuestionsByExpertId
            {
                PageIndex = pageNumber,
                PageSize = pageSize,
               ExpertId = expertId.ToString()
            });

            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(allQuestion?.Question?.Select(x => x.CreatedBy).Distinct());

            var allUserData = _userClient.GetListOfUsers(userIds);
            var UsersIds = new RequestGetOrgForMembers();
            UsersIds.UserId.AddRange(allUserData.UserData.Select(a => a.UserId).Distinct());

            var Orgs = await _orgMembersProtoService.GetOrgForMembersAsync(UsersIds);
            var allQuestionsDto = allQuestion?.Question?.Select(c => QuestionMapper.FromAllQuestionProto(c, allUserData, Orgs)).ToList();



            _responseDTO = _responseHelper.RetrievedSuccessfully(allQuestionsDto, "allQuestionsIsRetrievedSuccessfully");
            _responseDTO.PageSize = allQuestion.PageSize;
            _responseDTO.PageIndex = allQuestion.PageIndex;
            _responseDTO.TotalPages = allQuestion.TotalPages;
            _responseDTO.TotalItems = allQuestion.TotalItems;

            return _responseDTO;
        }
    }
}
