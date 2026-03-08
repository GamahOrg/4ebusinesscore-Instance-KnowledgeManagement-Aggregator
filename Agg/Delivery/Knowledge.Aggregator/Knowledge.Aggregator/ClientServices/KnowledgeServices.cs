//namespace KnowledgeManagement.Aggregator.ClientServices
//{
//	public class KnowledgeManagementServices:IKnowledgeManagementServices
//	{
//		private readonly KnowledgeManagementProtoService.KnowledgeManagementProtoServiceClient _KnowledgeManagementProto;
//		private readonly WorkspacePermissionProtoService.WorkspacePermissionProtoServiceClient _workspacePermissionProto;

//		public KnowledgeManagementServices(KnowledgeManagementProtoService.KnowledgeManagementProtoServiceClient KnowledgeManagementProto,
//					  WorkspacePermissionProtoService.WorkspacePermissionProtoServiceClient workspacePermissionProto)
//		{
//			_KnowledgeManagementProto = KnowledgeManagementProto;
//			_workspacePermissionProto = workspacePermissionProto;
//		}

//		public async Task PostActionContollerName()
//		{
//			try
//			{

//				var KnowledgeManagement = await GetKnowledgeManagementControllerAndActionName();

//				var Requst = new RequestWorkspacePermissionRequest();

//				foreach(var item in KnowledgeManagement)
//				{

//					Requst.ModuleInfo.Add(new ModuleInfo
//					{
//						ActionName = item.ActionName,
//						ControllerName = item.ControllerName,
//						Key = item.Key,
//						MethodVerd = item.MethodVerd,
//						Path = item.Path,
//					});
//				}
//				var response = await _workspacePermissionProto.PostWorkspacePermissionAsync(Requst);

//			}
//			catch(Exception)
//			{


//			}
//		}

//		public async Task<List<ModulaInfo>> GetKnowledgeManagementControllerAndActionName()
//		{

//			var ModulaInfo = new List<ModulaInfo>();
//			try
//			{
//				var response = await _KnowledgeManagementProto.GetKnowledgeManagementControllerAndActionNameAsync(new Empty());
//				;
//				foreach(var item in response.ModulaInfo)
//				{
//					ModulaInfo.Add(new ModulaInfo()
//					{
//						ActionName = item.ActionName,
//						ControllerName = item.ActionName,
//						Path = item.Path
//					,
//						MethodVerd = item.MethodVerd,
//						Key = item.Key
//					});
//				}
//			}
//			catch(Exception ex) { }

//			return ModulaInfo;
//		}
//	}
//}
