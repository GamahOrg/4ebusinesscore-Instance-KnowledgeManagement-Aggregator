namespace KnowledgeManagement.Aggregator.Interface
{
	public interface IQuestionService
	{
		public Task<ResponseDTO> GetAllQuestions(int pageSize,int pageNumber,string searchTerm);
		public Task<ResponseDTO> GetQuestionById(Guid questionId);
		public Task<ResponseDTO> GetAllRequestedQuestions(int pageSize,int pageNumber,int requestStage);
		public Task<ResponseDTO> GetRequestedQuestionById(Guid questionId);

        public Task<ResponseDTO> GetAllMyQuestions(int pageSize, int pageNumber, string searchTerm);
        public Task<ResponseDTO> OutGoingQuestions(int pageSize, int pageNumber, string searchTerm);
        public Task<ResponseDTO> IncomingQuestions(int pageSize, int pageNumber, string searchTerm);
        public Task<ResponseDTO> GetQuestionsByExpertId(int pageSize, int pageNumber, Guid expertId);
    }
}
