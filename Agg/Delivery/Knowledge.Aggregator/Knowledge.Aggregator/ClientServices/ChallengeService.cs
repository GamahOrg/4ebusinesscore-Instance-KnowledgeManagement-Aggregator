namespace KnowledgeManagement.Aggregator.ClientServices
{
	public class ChallengeService:IChallengeService
	{
		private readonly ChallengeProtoService.ChallengeProtoServiceClient _challengeClient;
		private readonly UserProtoService.UserProtoServiceClient _userClient;
		private readonly IResponseHelper _responseHelper;
		private ResponseDTO _responseDTO;


		public ChallengeService(
			IResponseHelper responseHelper,
			ChallengeProtoService.ChallengeProtoServiceClient challengeClient,
			UserProtoService.UserProtoServiceClient userClient)
		{
			_responseHelper = responseHelper;
			_responseDTO = new ResponseDTO();
			_challengeClient = challengeClient;
			_userClient = userClient;
		}
		public async Task<ResponseDTO> GetAllChallenges(int pageSize,int pageNumber,string searchTerm)
		{
			var allChallenges = _challengeClient.GetAllChallenges(new RequestAllChallenges { PageIndex = pageNumber,
																						     PageSize = pageSize,
																							 SearchTerm = searchTerm
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allChallenges?.Challenge?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			var allChallengesDto = allChallenges?.Challenge?.Select(c => ChallengeMapper.FromAllChallengeProto(c,allUserData)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allChallengesDto,"allChallengesIsRetrievedSuccessfully");
			_responseDTO.PageSize = allChallenges.PageSize;
			_responseDTO.PageIndex = allChallenges.PageIndex;
			_responseDTO.TotalPages = allChallenges.TotalPages;
			_responseDTO.TotalItems = allChallenges.TotalItems;

			return _responseDTO;
		}

		public async Task<ResponseDTO> GetAllRequestedChallenges(int pageSize,int pageNumber,int requestStage)
		{
			var allRequestedChallenges = _challengeClient.GetAllRequestedChallenges(new RequestAllRequestedChallenges
			{
				PageIndex = pageNumber,
				PageSize = pageSize,
				RequestStage = requestStage
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allRequestedChallenges?.Challenge?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			var allChallengesDto = allRequestedChallenges?.Challenge?.Select(c => ChallengeMapper.FromAllRequestedChallengeProto(c,allUserData)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allChallengesDto,"allRequestedChallengesIsRetrievedSuccessfully");
			_responseDTO.PageSize = allRequestedChallenges.PageSize;
			_responseDTO.PageIndex = allRequestedChallenges.PageIndex;
			_responseDTO.TotalPages = allRequestedChallenges.TotalPages;
			_responseDTO.TotalItems = allRequestedChallenges.TotalItems;

			return _responseDTO;
		}

		public async Task<ResponseDTO> GetChallengeById(Guid challengeId)
		{
			var challenge = _challengeClient.GetChallengeById(new RequestChallengeById { ChallengeId = challengeId.ToString() });

			if(challenge.Challenge == null)
			{
				return _responseHelper.NotFound("challengeIsNotFound");
			}

			var requestedUserId = new RequestGetUserById();
			requestedUserId.UserId = challenge?.Challenge?.CreatedBy;

			var userData = _userClient.GetUserById(requestedUserId);

			var challengeMapped = ChallengeMapper.FromChallengeById(challenge,userData);

			return _responseHelper.RetrievedSuccessfully(challengeMapped,"challengeIsRetrievedSuccessfully");
		}

		public async Task<ResponseDTO> GetRequestedChallengeById(Guid challengeId)
		{
			var requestedChallenge = _challengeClient.GetRequestedChallengeById(new RequestChallengeById { ChallengeId = challengeId.ToString() });

			if(requestedChallenge.Challenge == null)
			{
				return _responseHelper.NotFound("challengeIsNotFound");
			}

			var requestedUserId = new RequestGetUserById();
			requestedUserId.UserId = requestedChallenge?.Challenge?.CreatedBy;

			var userData = _userClient.GetUserById(requestedUserId);

			var challengeMapped = ChallengeMapper.FromRequestedChallengeById(requestedChallenge,userData);

			return _responseHelper.RetrievedSuccessfully(challengeMapped,"challengeIsRetrievedSuccessfully");
		}
	}
}
