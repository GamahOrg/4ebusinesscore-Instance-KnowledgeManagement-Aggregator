namespace KnowledgeManagement.Aggregator.Dtos.BlogComments
{
	public class QuestionCommentMapper
	{
		public static QuestionCommentsDto FromQuestionCommentProto(QuestionCommentProto commentProto,ResponseUserDataList userProto,Dictionary<string,QuestionCommentsDto> processedComments)
		{
			if(processedComments.TryGetValue(commentProto.Id,out var existingDto))
			{
				return existingDto;
			}

			var dto = new QuestionCommentsDto
			{
				Id = Guid.TryParse(commentProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(commentProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				QuestionId = Guid.TryParse(commentProto.QuestionId,out Guid parsedBlogId) ? parsedBlogId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				Body = commentProto.Body ?? "",
				Attachment = commentProto.Attachment ?? "",
				CreatedOn = DateTime.TryParse(commentProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				Replies = commentProto.Replies?
					.Select(reply => FromQuestionCommentProto(reply,userProto,processedComments))
					.ToList() ?? new List<QuestionCommentsDto>(),
			};

			processedComments[commentProto.Id] = dto;

			return dto;
		}
	}



}
