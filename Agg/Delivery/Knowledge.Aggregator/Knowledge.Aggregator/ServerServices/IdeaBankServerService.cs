namespace KnowledgeManagement.Aggregator.ServerServices
{
	public class IdeaBankServerService:IdeaBankCommentsProtoService.IdeaBankCommentsProtoServiceBase
	{
		private readonly IdeaBankCommentsProtoService.IdeaBankCommentsProtoServiceClient _ideaBankClient;

		public IdeaBankServerService(IdeaBankCommentsProtoService.IdeaBankCommentsProtoServiceClient ideaBankClient)
		{
			_ideaBankClient = ideaBankClient;
		}

		public async override Task<GetIdeaBankControllerAndActionNameResponse> GetIdeaBankControllerAndActionName(Empty request,ServerCallContext context)
		{
			var ideaPermissions = await _ideaBankClient.GetIdeaBankControllerAndActionNameAsync(request);
			return await Task.FromResult(ideaPermissions);
		}
	}
}
