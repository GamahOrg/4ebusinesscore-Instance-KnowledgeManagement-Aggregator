using ControlPanel.ORGHierararchy.Protos;
using IdeaBank.API.Protos;
using System;

namespace KnowledgeManagement.Aggregator.GrpClientsConfig
{
    public static class GrpcClients
    {
        #region Challenges
        public static void AddChallengesGrpcClients(this IServiceCollection services, string url)
        {
			services.AddGrpcServiceClientWithClientCredentials<ChallengeProtoService.ChallengeProtoServiceClient>(url);
			services.AddGrpcServiceClientWithClientCredentials<CommentProtoService.CommentProtoServiceClient>(url);

		}
		#endregion

		#region ControlPanel.Aggregator
		public static void AddCPAggregatorGrpcClients(this IServiceCollection services,string url)
		{
			
			services.AddGrpcServiceClient<OrgProtoService.OrgProtoServiceClient>(url);
            services.AddGrpcServiceClient<OrgMembersProtoService.OrgMembersProtoServiceClient>(url);

        }
        #endregion

        #region Blogs
        public static void AddBlogGrpcClients(this IServiceCollection services,string url)
		{
			services.AddGrpcServiceClient<BlogProtoService.BlogProtoServiceClient>(url);
			services.AddGrpcServiceClient<BlogCommentProtoService.BlogCommentProtoServiceClient>(url);

		}
		#endregion

		#region KnowLedge
		public static void AddKnowLedgeGrpcClients(this IServiceCollection services,string url)
		{
			services.AddGrpcServiceClient<SubKnowledgeProtoService.SubKnowledgeProtoServiceClient>(url);
            services.AddGrpcServiceClient<KnowledgeProtoService.KnowledgeProtoServiceClient>(url);
            services.AddGrpcServiceClient<IndexProtoService.IndexProtoServiceClient>(url);
        }
        #endregion

        #region IdeaBank
        public static void AddIdeaBankGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<IdeaBankCommentsProtoService.IdeaBankCommentsProtoServiceClient>(url);
            services.AddGrpcServiceClient<GetAllCommentsForIdeaBanksProtoService.GetAllCommentsForIdeaBanksProtoServiceClient>(url);
        }
        #endregion

		#region AskExpert
		public static void AddAskExpertGrpcClients(this IServiceCollection services,string url)
		{
			services.AddGrpcServiceClient<QuestionProtoService.QuestionProtoServiceClient>(url);
			services.AddGrpcServiceClientWithClientCredentials<QuestionCommentProtoService.QuestionCommentProtoServiceClient>(url);

		}
        #endregion
        #region UserManagement
        public static void AddUserManagementGrpcClients(
            this IServiceCollection services,
            string UserManagmentUrl)
        {
            services.AddGrpcServiceClient<UserProtoService.UserProtoServiceClient>(UserManagmentUrl);
            //services.AddGrpcServiceClient<PermissionService.PermissionServiceClient>(UserManagmentUrl);
            services.AddGrpcServiceClient<PermissionGroupingService.PermissionGroupingServiceClient>(UserManagmentUrl);
        }
        #endregion
    }
}
