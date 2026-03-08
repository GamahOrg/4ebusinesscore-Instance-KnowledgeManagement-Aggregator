namespace KnowledgeManagement.Aggregator.Dtos.Blogs
{
	public class BlogMapper
	{
		#region FromAllBlogProto
		public static BlogDto FromAllBlogProto(BlogProto blogProto,ResponseUserDataList userProto)
		{
			return new BlogDto
			{
				Id = Guid.TryParse(blogProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(blogProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				Email = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.Email ?? "",
				Title = blogProto.Title ?? "",
				Description = blogProto.Description ?? "",
				CreatedOn = DateTime.TryParse(blogProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				CommentCount = blogProto.BlogComments.Count(),
				LikeCount = blogProto.BlogLikes.Where(l => l.LikeStatus == 0).Count(),
				DisLikeCount = blogProto.BlogLikes.Where(l => l.LikeStatus == 1).Count(),
				RequestStage = blogProto.RequestStage,
				IsDisliked = blogProto.IsDisliked,
				IsLiked = blogProto.IsLiked,
				BlogAttachments = blogProto?.BlogAttachments?.Select(x => x.Path).ToList()
			};
		}
		#endregion

		#region FromBlogById
		public static BlogDto FromBlogById(ResponseBlogById blogProto,ResponseGetUserById userProto)
		{
			return new BlogDto
			{
				Id = Guid.TryParse(blogProto?.Blog?.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(blogProto?.Blog?.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto.FirstName ?? "",
				SecondName = userProto.SecondName ?? "",
				ThirdName = userProto.ThirdName ?? "",
				FourthName = userProto.FourthName ?? "",
				UserImgUrl = userProto.ImgUrl ?? "",
				Email = userProto.Email ?? "",
				Title = blogProto?.Blog?.Title ?? "",
				Description = blogProto?.Blog?.Description ?? "",
				CreatedOn = DateTime.TryParse(blogProto?.Blog?.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				CommentCount = blogProto?.Blog?.BlogComments?.Count() ?? 0,
				LikeCount = blogProto?.Blog?.BlogLikes?.Where(l => l.LikeStatus == 0).Count() ?? 0,
				DisLikeCount = blogProto?.Blog?.BlogLikes?.Where(l => l.LikeStatus == 1).Count() ?? 0,
				BlogAttachments = blogProto?.Blog?.BlogAttachments?.Select(x => x.Path).ToList()
			};
		}
		#endregion

		#region FromAllRequestedBlogProto
		public static RequestedBlogDto FromAllRequestedBlogProto(BlogProto blogProto,ResponseUserDataList userProto)
		{
			return new RequestedBlogDto
			{
				Id = Guid.TryParse(blogProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(blogProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				Email = userProto?.UserData?.Where(user => user.UserId.ToLower() == blogProto.CreatedBy.ToLower())?.FirstOrDefault()?.Email ?? "",
				Title = blogProto.Title ?? "",
				Description = blogProto.Description ?? "",
				CreatedOn = DateTime.TryParse(blogProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				Notes = blogProto.Notes ?? "",
				RequestStage = blogProto.RequestStage
			};
		}
		#endregion

		#region FromRequestedBlogById
		public static RequestedBlogDto FromRequestedBlogById(ResponseBlogById blogProto,ResponseGetUserById userProto)
		{
			return new RequestedBlogDto
			{
				Id = Guid.TryParse(blogProto?.Blog?.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(blogProto?.Blog?.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto.FirstName ?? "",
				SecondName = userProto.SecondName ?? "",
				ThirdName = userProto.ThirdName ?? "",
				FourthName = userProto.FourthName ?? "",
				UserImgUrl = userProto.ImgUrl ?? "",
				Email = userProto.Email ?? "",
				Title = blogProto?.Blog?.Title ?? "",
				Description = blogProto?.Blog?.Description ?? "",
				CreatedOn = DateTime.TryParse(blogProto?.Blog?.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				Notes = blogProto?.Blog?.Notes ?? "",
				RequestStage = blogProto.Blog.RequestStage,
				BlogAttachments=blogProto.Blog.BlogAttachments.Select(a=>a.Path).ToList()
			};
		}
		#endregion
	}
}
