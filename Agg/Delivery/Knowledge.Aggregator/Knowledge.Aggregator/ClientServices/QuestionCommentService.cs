namespace KnowledgeManagement.Aggregator.ClientServices
{
	public class QuestionCommentService:IQuestionCommentService
	{
		private readonly QuestionCommentProtoService.QuestionCommentProtoServiceClient _questionCommentClient;
		private readonly QuestionProtoService.QuestionProtoServiceClient _questionClient;
		private readonly UserProtoService.UserProtoServiceClient _userClient;
		private readonly IResponseHelper _responseHelper;
		private ResponseDTO _responseDTO;

		public QuestionCommentService(IResponseHelper responseHelper,
								UserProtoService.UserProtoServiceClient userClient,
								QuestionCommentProtoService.QuestionCommentProtoServiceClient questionCommentClient,
								QuestionProtoService.QuestionProtoServiceClient questionClient)
		{
			_responseHelper = responseHelper;
			_responseDTO = new ResponseDTO();
			_userClient = userClient;
			_questionCommentClient = questionCommentClient;
			_questionClient = questionClient;
		}
		public async Task<ResponseDTO> GetCommentByQuestionId(Guid questionId,int pageSize,int pageIndex)
		{
			var question = _questionClient.GetQuestionById(new RequestQuestionById { QuestionId = questionId.ToString() });

			if(question.Question == null)
			{
				return _responseHelper.NotFound("questionIsNotFound");
			}

			var allComments = _questionCommentClient.GetCommentsByQuestionId(new RequestQuestionByIdPaginated
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				QuestionId = questionId.ToString()
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allComments?.Comment?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			//PASSING THIS PARAMETER TO AVOID DEPULICATING FROM MAPPING
			var processedComments = new Dictionary<string,QuestionCommentsDto>();

			var allCommentsDto = allComments?.Comment?.Where(x => x.ParentId == "")?.Select(c => QuestionCommentMapper.FromQuestionCommentProto(c,allUserData,processedComments)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allCommentsDto,"allCommentIsRetrievedSuccessfully");
			_responseDTO.PageSize = allComments.PageSize;
			_responseDTO.PageIndex = allComments.PageIndex;
			_responseDTO.TotalPages = allComments.TotalPages;
			_responseDTO.TotalItems = allComments.TotalItems;

			return _responseDTO;
		}
	}
}
