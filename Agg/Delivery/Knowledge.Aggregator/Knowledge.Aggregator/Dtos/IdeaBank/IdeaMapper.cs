namespace KnowledgeManagement.Aggregator.Dtos.IdeaBank
{
	public class IdeaMapper
	{

		#region FromAllRequestedIdeaProto
		public static RequestedIdeaDto FromAllRequestedIdeaProto(IdeaProto ideaProto,ResponseUserDataList userProto)
		{
			return new RequestedIdeaDto
			{
				Id = Guid.TryParse(ideaProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(ideaProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				Email = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.Email ?? "",
				Title = ideaProto.Title ?? "",
				Description = ideaProto.Description ?? "",
				CreatedOn = DateTime.TryParse(ideaProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				Notes = ideaProto.Notes ?? "",
				RequestStage = ideaProto.RequestStage ,
                IsPublished = ideaProto.IsPublished ,
                IdeaCategory = ideaProto.IdeaCategory
			};
		}
		#endregion

		#region FromRequestedIdeaById
		public static RequestedIdeaDto FromRequestedIdeaById(ResponseIdeaById ideaProto,ResponseGetUserById userProto)
		{
			return new RequestedIdeaDto
			{
				Id = Guid.TryParse(ideaProto?.Idea?.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(ideaProto?.Idea?.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto.FirstName ?? "",
				SecondName = userProto.SecondName ?? "",
				ThirdName = userProto.ThirdName ?? "",
				FourthName = userProto.FourthName ?? "",
				UserImgUrl = userProto.ImgUrl ?? "",
				Email = userProto.Email ?? "",
				Title = ideaProto?.Idea?.Title ?? "",
                Lk_InputTypeId = ideaProto.Idea.LkInputTypeId != null ? ideaProto.Idea.LkInputTypeId : -1,
                Lk_InputTypeNameAr = ideaProto.Idea.LkInputTypeNameAr ?? "",
                Lk_InputTypeNameEn = ideaProto.Idea.LkInputTypeNameEn ?? "",
                Description = ideaProto?.Idea?.Description ?? "",
				CreatedOn = DateTime.TryParse(ideaProto?.Idea?.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				Notes = ideaProto?.Idea?.Notes ?? "",
				RequestStage = ideaProto.Idea.RequestStage,
				IdeaAttachments=ideaProto.Idea.IdeaAttachments.ToList(),
                IsPublished = ideaProto?.Idea?.IsPublished ?? false,
                IdeaCategory = ideaProto.Idea.IdeaCategory,
				
			};
		}
        #endregion

        #region IdeaBankCommentMapper
        public static GetAllIdeaCommentsDTO FromIdeaBankCommentProto(IdeaBankCommentsProto commentProto, ResponseUserDataList userProto, Dictionary<string, GetAllIdeaCommentsDTO> processedComments)
        {
            if (processedComments.TryGetValue(commentProto.Id, out var existingDto))
            {
                return existingDto;
            }

            var dto = new GetAllIdeaCommentsDTO
            {
                Id = Guid.TryParse(commentProto.Id, out Guid parsedId) ? parsedId : default(Guid),
                CreatedBy = Guid.TryParse(commentProto.CreatedBy, out Guid parsedUserId) ? parsedUserId : default(Guid),
                IdeaBankId = Guid.TryParse(commentProto.IdeaBankId, out Guid parsedBlogId) ? parsedBlogId : default(Guid),
                FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
                SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
                ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
                FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
                UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == commentProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
                CommentText = commentProto.CommentText ?? "",
                CreatedOn = DateTime.TryParse(commentProto.CreatedOn, out DateTime dateTime) ? dateTime : default(DateTime),
                Replies = commentProto.IdeaBankComments?
                    .Select(reply => FromIdeaBankCommentProto(reply, userProto, processedComments))
                    .ToList() ?? new List<GetAllIdeaCommentsDTO>(),
            };

            processedComments[commentProto.Id] = dto;

            return dto;
        }
        #endregion

        #region FromAllIdeasProto
        public static GetAllIdeasPaginatedDTO FromAllIdeasProto(IdeabankProtoModel ideaProto, ResponseUserDataList userProto)
        {
            return new GetAllIdeasPaginatedDTO
            {
                Id = Guid.TryParse(ideaProto.Id, out Guid parsedId) ? parsedId : default(Guid),
                CreatedBy = Guid.TryParse(ideaProto.CreatedBy, out Guid parsedUserId) ? parsedUserId : default(Guid),
                FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
                SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
                ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
                FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
                UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
                Email = userProto?.UserData?.Where(user => user.UserId.ToLower() == ideaProto.CreatedBy.ToLower())?.FirstOrDefault()?.Email ?? "",
                Title = ideaProto.Title ?? "",
                Description = ideaProto.Description ?? "",
                CreatedOn = DateTime.TryParse(ideaProto.CreatedOn, out DateTime dateTime) ? dateTime : default(DateTime),
                IsDisLikedByLoggedInUser = ideaProto.IsDisLikedByLoggedInUser,
                IsLikedByLoggedInUser = ideaProto.IsLikedByLoggedInUser,
                CommentCount = ideaProto.CommentsCount,
                IdeaCategory = ideaProto.IdeaCategory,
                LK_InputTypeId = ideaProto.LkInputTypeId != null ? ideaProto.LkInputTypeId : -1,
                LK_InputTypeNameAr = ideaProto.LkInputTypeNameAr ?? "",
                LK_InputTypeNameEn = ideaProto.LkInputTypeNameEn ?? "",
                DislikesCount = ideaProto.DislikesCount,
                LikesCount = ideaProto.LikesCount,
                IdeaViews = ideaProto.IdeaViews,
                IdeaAttachments = ideaProto.IdeaAttachments.Select(x=>x.Path).ToList(),
            };
        }
        #endregion
        #region FromGetIdeaByIdProto
        public static IdeaBankDetailsDTO FromGetIdeaByIdProto(IdeabankProtoModel ideaProto, ResponseGetUserById userProto)
        {
            return new IdeaBankDetailsDTO
            {
                Id = Guid.TryParse(ideaProto.Id, out Guid parsedId) ? parsedId : default(Guid),
                CreatedBy = Guid.TryParse(ideaProto.CreatedBy, out Guid parsedUserId) ? parsedUserId : default(Guid),
                FirstName = userProto.FirstName ?? "",
                SecondName = userProto.SecondName ?? "",
                ThirdName = userProto.ThirdName ?? "",
                FourthName = userProto.FourthName ?? "",
                UserImgUrl = userProto.ImgUrl ?? "",
                Email = userProto.Email ?? "",
                Title = ideaProto.Title ?? "",
                Lk_InputTypeId = ideaProto.LkInputTypeId != null ? ideaProto.LkInputTypeId : -1,
                Lk_InputTypeNameAr = ideaProto.LkInputTypeNameAr ?? "",
                Lk_InputTypeNameEn = ideaProto.LkInputTypeNameEn ?? "",
                Description = ideaProto.Description ?? "",
                CreatedOn = DateTime.TryParse(ideaProto.CreatedOn, out DateTime dateTime) ? dateTime : default(DateTime),
                IsDisLikedByLoggedInUser = ideaProto.IsDisLikedByLoggedInUser,
                IsLikedByLoggedInUser = ideaProto.IsLikedByLoggedInUser,
                CommentsCount = ideaProto.CommentsCount,
                IdeaCategory = ideaProto.IdeaCategory,
                DislikesCount = ideaProto.DislikesCount,
                LikesCount = ideaProto.LikesCount,
                Rates = ideaProto.Rates,
                IdeaAttachments = ideaProto.IdeaAttachments.Select(x => x.Path).ToList(),
            };
        }
        #endregion
    }
}
