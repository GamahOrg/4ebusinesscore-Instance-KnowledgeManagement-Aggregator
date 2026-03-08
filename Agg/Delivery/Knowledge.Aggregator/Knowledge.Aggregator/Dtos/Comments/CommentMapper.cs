namespace KnowledgeManagement.Aggregator.Dtos.Comments
{
	public class CommentMapper
	{
		public static CommentDto FromCommentProto(CommentProto commentProto,ResponseUserDataList userProto,Dictionary<string,CommentDto> processedComments)
		{
			if(processedComments.TryGetValue(commentProto.Id,out var existingDto))
			{
				return existingDto;
			}

			var dto = new CommentDto
			{
				Id = Guid.TryParse(commentProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(commentProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				ChallengeId = Guid.TryParse(commentProto.ChallengeId,out Guid parsedchallengeId) ? parsedchallengeId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				Description = commentProto.Description ?? "",
				CreatedOn = DateTime.TryParse(commentProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				CommentAttachment = commentProto.CommentAttachment ?? "",
				Replies = commentProto.Replies?
					.Select(reply => FromCommentProto(reply,userProto,processedComments))
					.ToList() ?? new List<CommentDto>(),
			};

			processedComments[commentProto.Id] = dto;

			return dto;
		}
	}
}
