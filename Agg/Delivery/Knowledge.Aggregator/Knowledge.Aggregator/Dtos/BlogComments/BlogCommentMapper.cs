using KnowledgeManagement.Aggregator.Protos;

namespace KnowledgeManagement.Aggregator.Dtos.BlogComments
{
	public class BlogCommentMapper
	{
		public static BlogCommentsDto FromBlogCommentProto(BlogCommentProto commentProto,ResponseUserDataList userProto,Dictionary<string,BlogCommentsDto> processedComments)
		{
			if(processedComments.TryGetValue(commentProto.Id,out var existingDto))
			{
				return existingDto;
			}

			var dto = new BlogCommentsDto
			{
				Id = Guid.TryParse(commentProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(commentProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				BlogId = Guid.TryParse(commentProto.BlogId,out Guid parsedBlogId) ? parsedBlogId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				Body = commentProto.Body ?? "",
				IsDisliked = commentProto.IsDisliked,
				IsLiked = commentProto.IsLiked,
				CreatedOn = DateTime.TryParse(commentProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				LikeCount = commentProto.CommentLikes.Where(l => l.LikeStatus == 0)?.Count() ?? 0,
				DisLikeCount = commentProto.CommentLikes.Where(l => l.LikeStatus == 1)?.Count() ?? 0,
                ParentId = Guid.TryParse(commentProto.ParentId, out Guid ParentId) ? ParentId : default(Guid),
                Replies = commentProto.Replies?
					.Select(reply => FromBlogCommentProto(reply,userProto,processedComments))
					.ToList() ?? new List<BlogCommentsDto>(),
			};

			processedComments[commentProto.Id] = dto;

			return dto;
		}
	}



}
