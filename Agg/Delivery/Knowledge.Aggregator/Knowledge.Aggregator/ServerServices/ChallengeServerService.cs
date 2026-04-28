namespace KnowledgeManagement.Aggregator.ServerServices
{
	//[Authorize("ClientCredentialsPolicy")]
	public class ChallengeServerService:ChallengeProtoService.ChallengeProtoServiceBase
	{
		private readonly ChallengeProtoService.ChallengeProtoServiceClient _challengeProto;

		public ChallengeServerService(ChallengeProtoService.ChallengeProtoServiceClient challengeProto)
		{
			_challengeProto = challengeProto;
		}
		public async override Task<GetControllerAndActionNameResponse> GetChallengesControllerAndActionName(Empty request,ServerCallContext context)
		{
			var challengesPermissions = await _challengeProto.GetChallengesControllerAndActionNameAsync(request);
			return await Task.FromResult(challengesPermissions);
		}
	}
}
