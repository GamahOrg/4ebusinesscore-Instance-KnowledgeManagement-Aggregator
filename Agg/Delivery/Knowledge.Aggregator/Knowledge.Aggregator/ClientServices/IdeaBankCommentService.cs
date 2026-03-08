using IdeaBank.API.Protos;
using KnowledgeManagement.Aggregator.Dtos.IdeaBankComments;

namespace KnowledgeManagement.Aggregator.ClientServices
{
    public class IdeaBankCommentService : IIdeaBankCommentService
    {
        private readonly GetAllCommentsForIdeaBanksProtoService.GetAllCommentsForIdeaBanksProtoServiceClient _ideaCommentClient;
        private readonly UserProtoService.UserProtoServiceClient _userClient;
        private readonly IResponseHelper _responseHelper;
        private ResponseDTO _responseDTO;

        public IdeaBankCommentService(GetAllCommentsForIdeaBanksProtoService.GetAllCommentsForIdeaBanksProtoServiceClient ideaCommentClient, UserProtoService.UserProtoServiceClient userClient, IResponseHelper responseHelper)
        {
            _ideaCommentClient = ideaCommentClient;
            _userClient = userClient;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> GetCommentsForIdeaBankById(Guid ideaBankId, int pageSize, int pageIndex)
        {
            var allIdeaBankComments = _ideaCommentClient.GetCommentsForIdeaBanksById(new RequestAllCommentsForIdeaBanks
            {
                Id = ideaBankId.ToString(),
                PageIndex = pageIndex,
                PageSize = pageSize,
            });
            if (allIdeaBankComments == null || allIdeaBankComments.Comment == null)
            {
                return _responseHelper.NotFound("Error retrieving idea bank comments.");
            }

            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(allIdeaBankComments?.Comment?.Select(x => x.CreatedBy).Distinct());

            var allUserData = _userClient.GetListOfUsers(userIds);

            var processedComments = new Dictionary<string, IdeaBankCommentsDto>();
            Guid guid = default;
            var allCommentsDto = allIdeaBankComments?.Comment?.Where(x => x.ParentCommentId == guid.ToString())?.Select(c => IdeaBankCommentMapper.GetAllIdeaBankCommentsProto(c, allUserData, processedComments)).ToList();

            _responseDTO = _responseHelper.RetrievedSuccessfully(allCommentsDto, "allCommentIsRetrievedSuccessfully");
            
            _responseDTO.PageSize = allIdeaBankComments.PageSize;
            _responseDTO.PageIndex = allIdeaBankComments.PageIndex;
            _responseDTO.TotalPages = allIdeaBankComments.TotalPages;
            _responseDTO.TotalItems = allIdeaBankComments.TotalItems;

            return _responseDTO;
        }
    }
}
