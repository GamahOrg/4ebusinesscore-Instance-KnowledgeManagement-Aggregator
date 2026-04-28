using ControlPanel.ORGHierararchy.Protos;

namespace KnowledgeManagement.Aggregator.Dtos.Blogs
{
	public class QuestionMapper
	{
		#region FromAllQuestionProto
		public static QuestionDto FromAllQuestionProto(QuestionProto questionProto,ResponseUserDataList userProto,ResponseGetAllMembers org)
		{
			return new QuestionDto
			{
				Id = Guid.TryParse(questionProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(questionProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				Body = questionProto.Body ?? "",
				CreatedOn = DateTime.TryParse(questionProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				CommentCount = (int?)questionProto.Comments.Count() ?? 0,
				RequestStage = questionProto.RequestStage,
				OrgNameAr=org?.ResponseALLManger?.FirstOrDefault(a=>a.MebmerId.ToLower()== questionProto.CreatedBy.ToLower())?.OrgNameAr,
                OrgNameEn = org?.ResponseALLManger?
				          .FirstOrDefault(a => a.MebmerId.ToLower() == questionProto.CreatedBy.ToLower())?.OrgNameEn,
				OrgId = Guid.TryParse(org?.ResponseALLManger?.FirstOrDefault(a => a.MebmerId.ToLower() == questionProto.CreatedBy.ToLower())?.OrgId , out Guid ParesOrgId) ? ParesOrgId : default(Guid),
				SpecializationNameAr=questionProto.SpecializationNameAr,
				SpecializationNameEn=questionProto.SpecializationNameEn,
				Type = questionProto.Type,
                Experts = questionProto?.Experts?.Select(x => new ExpertInfo()
                {
                    ExpertId = Guid.Parse(x.ExpertId),
                    ExpertEmail = x.ExpertEmail ?? "" ,
					ExpertName = x.ExpertName ?? ""
                }).ToList(),

            };
		}
		#endregion

		#region FromQuestionById
		public static QuestionDto FromQuestionById(ResponseQuestionById questionProto,ResponseGetUserById userProto)
		{
			return new QuestionDto
			{
				Id = Guid.TryParse(questionProto?.Question?.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(questionProto?.Question?.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto.FirstName ?? "",
				SecondName = userProto.SecondName ?? "",
				ThirdName = userProto.ThirdName ?? "",
				FourthName = userProto.FourthName ?? "",
				UserImgUrl = userProto.ImgUrl ?? "",
				Email = userProto.Email ?? "",
				Body = questionProto?.Question?.Body ?? "",
				CreatedOn = DateTime.TryParse(questionProto?.Question?.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				CommentCount = questionProto?.Question?.Comments?.Count() ?? 0,
                SpecializationNameAr = questionProto?.Question?.SpecializationNameAr,
                SpecializationNameEn = questionProto?.Question?.SpecializationNameEn,
				Type = questionProto.Question.Type,
                Experts = questionProto?.Question?.Experts?.Select(x => new ExpertInfo()
                {
                    ExpertId = Guid.Parse(x.ExpertId),
                    ExpertEmail = x.ExpertEmail ?? "" , 
					ExpertName = x.ExpertName ?? ""
                }).ToList(),
            };
		}
		#endregion

		#region FromAllRequestedQuestionProto
		public static RequestedQuestionDto FromAllRequestedQuestionProto(QuestionProto questionProto,ResponseUserDataList userProto)
		{
			return new RequestedQuestionDto
			{
				Id = Guid.TryParse(questionProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(questionProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				Email = userProto?.UserData?.Where(user => user.UserId.ToLower() == questionProto.CreatedBy.ToLower())?.FirstOrDefault()?.Email ?? "",
				Body = questionProto.Body ?? "",
				CreatedOn = DateTime.TryParse(questionProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				Notes = questionProto.Notes ?? "",
				RequestStage = questionProto.RequestStage
			};
		}
		#endregion

		#region FromRequestedQuestionById
		public static RequestedQuestionDto FromRequestedQuestionById(ResponseQuestionById questionProto,ResponseGetUserById userProto)
		{
			return new RequestedQuestionDto
			{
				Id = Guid.TryParse(questionProto?.Question?.Id, out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(questionProto?.Question?.CreatedBy, out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto.FirstName ?? "",
				SecondName = userProto.SecondName ?? "",
				ThirdName = userProto.ThirdName ?? "",
				FourthName = userProto.FourthName ?? "",
				UserImgUrl = userProto.ImgUrl ?? "",
				Email = userProto.Email ?? "",
				Body = questionProto?.Question?.Body ?? "",
				CreatedOn = DateTime.TryParse(questionProto?.Question?.CreatedOn, out DateTime dateTime) ? dateTime : default(DateTime),
				Notes = questionProto?.Question?.Notes ?? "",
				RequestStage = questionProto.Question.RequestStage,
				Experts = questionProto?.Question?.Experts?.Select(x=> new ExpertInfo()
				{
					ExpertId = Guid.Parse(x.ExpertId) ,
					ExpertEmail = x.ExpertEmail ??"" ,
					ExpertName = x.ExpertName ??""
				}).ToList(),
            };
			
		}
		#endregion
	}
}
