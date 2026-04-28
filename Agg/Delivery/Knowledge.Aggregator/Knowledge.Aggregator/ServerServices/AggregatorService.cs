namespace KnowledgeManagement.Aggregator.ServerServices
{
	//[Authorize("ClientCredentialsPolicy")]
	public class AggregatorService :KnowlageAggProtoService.KnowlageAggProtoServiceBase
	{
		private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

		public AggregatorService(
						  IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
		{
			_actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
		}
		public async override Task<GetControllerAndActionNameResponse> GetKnowlageAggControllerAndActionName(Empty request,ServerCallContext context)
		{
			var Response = new GetControllerAndActionNameResponse();

			long key = 90;
			var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Where(
								ad => ad.AttributeRouteInfo != null).ToList();

			foreach(var item in routes)
			{
				var ControllerInfo = new ModulaInfo()
				{

					Key = key,
					ControllerName = item.RouteValues["controller"],
					Path = item.AttributeRouteInfo?.Template ?? "",
					MethodVerd = item.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First(),
					ActionName = item.RouteValues["action"]
				};
				var controllerName = item.RouteValues["controller"];
				var actionName = item.RouteValues["action"];

				Response.ModulaInfo.Add(ControllerInfo);

			}
			return Response;
		}
	}
}
