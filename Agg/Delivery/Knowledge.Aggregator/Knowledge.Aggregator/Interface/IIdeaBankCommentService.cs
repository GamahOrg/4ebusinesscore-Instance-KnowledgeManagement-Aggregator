namespace KnowledgeManagement.Aggregator.Interface
{
    public interface IIdeaBankCommentService
    {
        public Task<ResponseDTO> GetCommentsForIdeaBankById(Guid ideaBankId, int pageSize, int pageIndex);
    }
}
