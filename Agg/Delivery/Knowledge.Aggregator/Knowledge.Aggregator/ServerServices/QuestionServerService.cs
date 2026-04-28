namespace KnowledgeManagement.Aggregator.ServerServices
{
	//[Authorize("ClientCredentialsPolicy")]
	public class QuestionServerService : QuestionProtoService.QuestionProtoServiceBase
	{
		private readonly QuestionProtoService.QuestionProtoServiceClient _questionClient;

		public QuestionServerService(QuestionProtoService.QuestionProtoServiceClient questionClient)
		{
			_questionClient = questionClient;
		}

		public async override Task<GetQuestionControllerAndActionNameResponse> GetQuestionControllerAndActionName(Empty request,ServerCallContext context)
		{
			var questionPermissions = await _questionClient.GetQuestionControllerAndActionNameAsync(request);
			return await Task.FromResult(questionPermissions);
		}
	}
}
