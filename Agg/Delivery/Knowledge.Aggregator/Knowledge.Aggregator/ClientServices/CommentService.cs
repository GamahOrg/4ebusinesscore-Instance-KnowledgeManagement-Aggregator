namespace KnowledgeManagement.Aggregator.ClientServices
{
	public class CommentService:ICommentService
	{
		private readonly CommentProtoService.CommentProtoServiceClient _commentClient;
		private readonly ChallengeProtoService.ChallengeProtoServiceClient _challengeClient;
		private readonly UserProtoService.UserProtoServiceClient _userClient;
		private readonly IResponseHelper _responseHelper;
		private ResponseDTO _responseDTO;

		public CommentService(
			IResponseHelper responseHelper,
			UserProtoService.UserProtoServiceClient userClient,
			CommentProtoService.CommentProtoServiceClient commentClient,
			ChallengeProtoService.ChallengeProtoServiceClient challengeClient
			)
		{
			_responseHelper = responseHelper;
			_responseDTO = new ResponseDTO();
			_userClient = userClient;
			_commentClient = commentClient;
			_challengeClient = challengeClient;
		}
		public async Task<ResponseDTO> GetAllCommentsByChallengeId(Guid challengeId,int pageSize,int pageIndex)
		{
			var challenge = _challengeClient.GetChallengeById(new RequestChallengeById { ChallengeId = challengeId.ToString() });

			if(challenge.Challenge == null)
			{
				return _responseHelper.NotFound("challengeIsNotFound");
			}

			var allComments = _commentClient.GetCommentsByChallengeId(new RequestChallengeByIdPaginated
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				ChallengeId = challengeId.ToString()
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allComments?.Comment?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			//PASSING THIS PARAMETER TO AVOID DEPULICATING FROM MAPPING
			var processedComments = new Dictionary<string,CommentDto>();

			var allCommentsDto = allComments?.Comment?.Where(x => x.ParentId == "")?.Select(c =>CommentMapper.FromCommentProto(c,allUserData,processedComments)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allCommentsDto,"allCommentIsRetrievedSuccessfully");
			_responseDTO.PageSize = allComments.PageSize;
			_responseDTO.PageIndex = allComments.PageIndex;
			_responseDTO.TotalPages = allComments.TotalPages;
			_responseDTO.TotalItems = allComments.TotalItems;

			return _responseDTO;
		}
	}
}
