namespace KnowledgeManagement.Aggregator.ClientServices
{
	public class BlogService:IBlogService
	{
		private readonly BlogProtoService.BlogProtoServiceClient _blogClient;
		private readonly UserProtoService.UserProtoServiceClient _userClient;
		private readonly IResponseHelper _responseHelper;
		private ResponseDTO _responseDTO;


		public BlogService(
			IResponseHelper responseHelper,
			BlogProtoService.BlogProtoServiceClient blogClient,
			UserProtoService.UserProtoServiceClient userClient)
		{
			_responseHelper = responseHelper;
			_responseDTO = new ResponseDTO();
			_blogClient = blogClient;
			_userClient = userClient;
		}
		public async Task<ResponseDTO> GetAllBlogs(int pageSize,int pageNumber,string searchTerm)
		{
				var allBlogs = _blogClient.GetAllBlogs(new RequestAllBlogs
				{
					PageIndex = pageNumber,
					PageSize = pageSize,
					SearchTerm = searchTerm
				});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allBlogs?.Blog?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			var allBlogsDto = allBlogs?.Blog?.Select(c => BlogMapper.FromAllBlogProto(c,allUserData)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allBlogsDto,"allBlogsIsRetrievedSuccessfully");
			_responseDTO.PageSize = allBlogs.PageSize;
			_responseDTO.PageIndex = allBlogs.PageIndex;
			_responseDTO.TotalPages = allBlogs.TotalPages;
			_responseDTO.TotalItems = allBlogs.TotalItems;

			return _responseDTO;
		}

		public async Task<ResponseDTO> GetAllRequestedBlogs(int pageSize,int pageNumber,int requestStage)
		{
			var allRequestedBlog = _blogClient.GetAllRequestedBlogs(new RequestAllRequestedBlogs
			{
				PageIndex = pageNumber,
				PageSize = pageSize,
				RequestStage = requestStage
			});

			var userIds = new RequestedUserList();
			userIds.UserId.AddRange(allRequestedBlog?.Blog?.Select(x => x.CreatedBy).Distinct());

			var allUserData = _userClient.GetListOfUsers(userIds);

			var allBlogsDto = allRequestedBlog?.Blog?.Select(c => BlogMapper.FromAllRequestedBlogProto(c,allUserData)).ToList();

			_responseDTO = _responseHelper.RetrievedSuccessfully(allBlogsDto,"allRequestedBlogIsRetrievedSuccessfully");
			_responseDTO.PageSize = allRequestedBlog.PageSize;
			_responseDTO.PageIndex = allRequestedBlog.PageIndex;
			_responseDTO.TotalPages = allRequestedBlog.TotalPages;
			_responseDTO.TotalItems = allRequestedBlog.TotalItems;

			return _responseDTO;
		}

		public async Task<ResponseDTO> GetBlogById(Guid blogId)
		{
			var blog = _blogClient.GetBlogById(new RequestBlogById { BlogId = blogId.ToString() });

			if(blog.Blog == null)
			{
				return _responseHelper.NotFound("blogIsNotFound");
			}

			var requestedUserId = new RequestGetUserById();
			requestedUserId.UserId = blog?.Blog?.CreatedBy;

			var userData = _userClient.GetUserById(requestedUserId);

			var blogMapped = BlogMapper.FromBlogById(blog,userData);

			return _responseHelper.RetrievedSuccessfully(blogMapped,"blogIsRetrievedSuccessfully");
		}

		public async Task<ResponseDTO> GetRequestedBlogById(Guid blogId)
		{
			var requestedBlog = _blogClient.GetRequestedBlogById(new RequestBlogById { BlogId = blogId.ToString() });

			if(requestedBlog.Blog == null)
			{
				return _responseHelper.NotFound("blogIsNotFound");
			}

			var requestedUserId = new RequestGetUserById();
			requestedUserId.UserId = requestedBlog?.Blog?.CreatedBy;

			var userData = _userClient.GetUserById(requestedUserId);

			var blogMapped = BlogMapper.FromRequestedBlogById(requestedBlog,userData);

			return _responseHelper.RetrievedSuccessfully(blogMapped,"blogIsRetrievedSuccessfully");
		}
	}
}
