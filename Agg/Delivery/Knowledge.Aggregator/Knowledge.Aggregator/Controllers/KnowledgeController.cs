using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeManagement.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeController : ControllerBase
    {
        private readonly IKnowledgesService _knowledgesService;

        public KnowledgeController(IKnowledgesService knowledgesService)
        {
            _knowledgesService = knowledgesService;
        }

        [HttpGet("GetAllPrivateKnowledges")]
        public async Task<ResponseDTO> GetAllPrivateKnowledges(int pageNumber, int pageSize, string? searchTerm)
        {
            return await _knowledgesService.GetAllPrivateKnowledges(pageNumber, pageSize, searchTerm);
        }
        [HttpGet("GetUserAndIOrgIndicators")]
        public async Task<ResponseDTO> GetUserAndIOrgIndicators()
        {
            return await _knowledgesService.GetUserAndIOrgIndicators();
        }
    }
}
