namespace KnowledgeManagement.Aggregator.ClientServices
{
	public class BlogCommentService:IBlogCommentService
	{
		private readonly BlogCommentProtoService.BlogCommentProtoServiceClient _blogCommentClient;
		private readonly BlogProtoService.BlogProtoServiceClient _blogClient;
		private readonly UserProtoService.UserProtoServiceClient _userClient;
		private readonly IResponseHelper _responseHelper;
		private ResponseDTO _responseDTO;

		public BlogCommentService(
			IResponseHelper responseHelper,
			UserProtoService.UserProtoServiceClient userClient,
			BlogCommentProtoService.BlogCommentProtoServiceClient blogCommentClient,
			BlogProtoService.BlogProtoServiceClient blogClient
			)
		{
			_responseHelper = responseHelper;
			_responseDTO = new ResponseDTO();
			_userClient = userClient;
			_blogCommentClient = blogCommentClient;
			_blogClient = blogClient;
		}
		public async Task<ResponseDTO> GetCommentByBlogId(Guid blogId,int pageSize,int pageIndex)
		{
			var blog = _blogClient.GetBlogById(new RequestBlogById { BlogId = blogId.ToString() });

			if(blog.Blog == null)
			{
				return _responseHelper.NotFound("blogIsNotFound");
			}

			var allComments = _blogCommentClient.GetCommentsByBlogId(new RequestBlogByIdPaginated
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				BlogId = blogId.ToString()
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allComments?.Comment?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			//PASSING THIS PARAMETER TO AVOID DEPULICATING FROM MAPPING
			var processedComments = new Dictionary<string,BlogCommentsDto>();

			var allCommentsDto = allComments?.Comment?.Where(x => x.ParentId == "")?.Select(c => BlogCommentMapper.FromBlogCommentProto(c,allUserData,processedComments)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allCommentsDto,"allCommentIsRetrievedSuccessfully");
			_responseDTO.PageSize = allComments.PageSize;
			_responseDTO.PageIndex = allComments.PageIndex;
			_responseDTO.TotalPages = allComments.TotalPages;
			_responseDTO.TotalItems = allComments.TotalItems;

			return _responseDTO;
		}
	}
}
