namespace KnowledgeManagement.Aggregator.Interface
{
	public interface ISubKnowledgeService
	{
		public Task<ResponseDTO> GetAllSubKnowledgeByKnowledgeId(Guid knowledgeId,int pageSize,int pageNumber,string title,
			string taggedWords,
			DateOnly? createdOn,
			string subKnowledgeId,
			string subKnowledgeCategoryId,
			string knowledgeCategoryId,
			string orgHierarchyId,
            DateOnly? date ,
			int KnowledgeType);
		public Task<ResponseDTO> GetSubKnowledgeById(Guid subKnowledgeId);
	}
}
