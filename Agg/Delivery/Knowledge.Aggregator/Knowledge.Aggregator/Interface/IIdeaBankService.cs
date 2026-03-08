namespace KnowledgeManagement.Aggregator.Interface
{
    public interface IIdeaBankService
    {
        public Task<ResponseDTO> GetAllIdeaBankComments(Guid ideaBankId, int pageSize, int pageNumber);
		public Task<ResponseDTO> GetAllRequestedIdeas(int pageSize,int pageNumber,int requestStage);
		public Task<ResponseDTO> GetRequestedIdeaById(Guid ideaId);

        public Task<ResponseDTO> GetAllIdeasPaginated(int pageSize, int pageNumber);
        public Task<ResponseDTO> GetIdeaBankById(Guid id);
    }
}
