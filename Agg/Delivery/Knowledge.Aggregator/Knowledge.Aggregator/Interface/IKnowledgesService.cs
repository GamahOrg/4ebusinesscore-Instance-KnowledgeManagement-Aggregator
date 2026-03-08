namespace KnowledgeManagement.Aggregator.Interface
{
    public interface IKnowledgesService
    {
        public Task<ResponseDTO> GetAllPrivateKnowledges(int pageNumber, int pageSize, string? searchTerm);
        public Task<ResponseDTO> GetUserAndIOrgIndicators();
    }
}
