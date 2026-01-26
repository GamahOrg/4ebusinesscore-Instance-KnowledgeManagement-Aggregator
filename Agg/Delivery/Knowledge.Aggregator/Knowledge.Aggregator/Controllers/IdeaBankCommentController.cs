using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeManagement.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ResourceOwnerPolicy")]
    public class IdeaBankCommentController : ControllerBase
    {
        private readonly IIdeaBankCommentService _commentService;

        public IdeaBankCommentController(IIdeaBankCommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("GetAllCommentsByIdeaBankId/{ideaBankId}")]
        public async Task<ResponseDTO> GetAllCommentsByBlogId(Guid ideaBankId, int pageSize, int pageNumber)
        {
            return await _commentService.GetCommentsForIdeaBankById(ideaBankId, pageSize, pageNumber);
        }
    }
}
