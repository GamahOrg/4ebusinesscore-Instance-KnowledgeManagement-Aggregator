namespace KnowledgeManagement.Aggregator.ServerServices
{
	public class KnowledgeServerService:SubKnowledgeProtoService.SubKnowledgeProtoServiceBase
	{
		private readonly SubKnowledgeProtoService.SubKnowledgeProtoServiceClient _knowledgeClient;

		public KnowledgeServerService(SubKnowledgeProtoService.SubKnowledgeProtoServiceClient knowledgeClient)
		{
			_knowledgeClient = knowledgeClient;
		}
		public async override Task<GetKnowledgeControllerAndActionNameResponse> GetKnowledgeControllerAndActionName(Empty request,ServerCallContext context)
		{
			var knowledgePermissions = await _knowledgeClient.GetKnowledgeControllerAndActionNameAsync(request);
			return await Task.FromResult(knowledgePermissions);
		}
	}
}
