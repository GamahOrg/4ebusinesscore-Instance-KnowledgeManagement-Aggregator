namespace KnowledgeManagement.Aggregator.Dtos.Challenges
{
	public class ChallengeMapper
	{
		public static GetAllChallengeDto FromAllChallengeProto(ChallengeProto challengeProto,ResponseUserDataList userProto)
		{
			return new GetAllChallengeDto
			{
				Id = Guid.TryParse(challengeProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(challengeProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
				challegeCategoryId = Guid.Parse(challengeProto.LKChallengeCategoryId),
                LK_ChallengeCategoryNameAr = challengeProto.LKChallengeCategoryNameAr ?? "",
                LK_ChallengeCategoryNameEn = challengeProto.LKChallengeCategoryNameEn ?? "",
				Title = challengeProto.Title ?? "",
				Description = challengeProto.Description ?? "",
				CreatedOn = DateTime.TryParse(challengeProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				CommentCount = challengeProto.ChallengeComments.Count(),
				RequestStage = challengeProto.RequestStage,
                ChallengeAttachments = challengeProto?.ChallengeAttachments?.Select(x => x.Path).ToList()
            };
		}

		public static RequestedChallengeDto FromAllRequestedChallengeProto(ChallengeProto challengeProto,ResponseUserDataList userProto)
		{
			return new RequestedChallengeDto
			{
				Id = Guid.TryParse(challengeProto.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(challengeProto.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.FirstName ?? "",
				SecondName = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.SecondName ?? "",
				ThirdName = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
				FourthName = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.FourthName ?? "",
				UserImgUrl = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.ImgUrl ?? "",
                Email = userProto?.UserData?.Where(user => user.UserId.ToLower() == challengeProto.CreatedBy.ToLower())?.FirstOrDefault()?.Email ?? "",
                challegeCategoryId = Guid.Parse(challengeProto.LKChallengeCategoryId),
                LK_ChallengeCategoryNameAr = challengeProto.LKChallengeCategoryNameAr ?? "",
                LK_ChallengeCategoryNameEn = challengeProto.LKChallengeCategoryNameEn ?? "",
                Title = challengeProto.Title ?? "",
				Description = challengeProto.Description ?? "",
				CreatedOn = DateTime.TryParse(challengeProto.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				Notes = challengeProto.Notes ?? "",
				RequestStage = challengeProto.RequestStage
			};
		}

		public static ChallengeByIdDto FromChallengeById(ResponseChallengeById challengeProto,ResponseGetUserById userProto)
		{
			return new ChallengeByIdDto
			{
				Id = Guid.TryParse(challengeProto?.Challenge?.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(challengeProto?.Challenge?.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto.FirstName ?? "",
				SecondName = userProto.SecondName ?? "",
				ThirdName = userProto.ThirdName ?? "",
				FourthName = userProto.FourthName ?? "",
				UserImgUrl = userProto.ImgUrl ?? "",
                challegeCategoryId = Guid.Parse(challengeProto.Challenge.LKChallengeCategoryId),
                LK_ChallengeCategoryNameAr = challengeProto.Challenge.LKChallengeCategoryNameAr ?? "",
                LK_ChallengeCategoryNameEn = challengeProto.Challenge.LKChallengeCategoryNameEn ?? "",
                Description = challengeProto?.Challenge?.Description ?? "",
				CreatedOn = DateTime.TryParse(challengeProto?.Challenge?.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				CommentCount = challengeProto?.Challenge?.ChallengeComments?.Count() ?? 0,
				ChallengeAttachments = challengeProto?.Challenge?.ChallengeAttachments?.Select(x => x.Path).ToList()
			};
		}

		public static RequestedChallengeDto FromRequestedChallengeById(ResponseChallengeById challengeProto,ResponseGetUserById userProto)
		{
			return new RequestedChallengeDto
			{
				Id = Guid.TryParse(challengeProto?.Challenge?.Id,out Guid parsedId) ? parsedId : default(Guid),
				CreatedBy = Guid.TryParse(challengeProto?.Challenge?.CreatedBy,out Guid parsedUserId) ? parsedUserId : default(Guid),
				FirstName = userProto.FirstName ?? "",
				SecondName = userProto.SecondName ?? "",
				ThirdName = userProto.ThirdName ?? "",
				FourthName = userProto.FourthName ?? "",
				UserImgUrl = userProto.ImgUrl ?? "",
                Email = userProto.Email ?? "",
                challegeCategoryId = Guid.Parse(challengeProto.Challenge.LKChallengeCategoryId),
                LK_ChallengeCategoryNameAr = challengeProto.Challenge.LKChallengeCategoryNameAr ?? "",
                LK_ChallengeCategoryNameEn = challengeProto.Challenge.LKChallengeCategoryNameEn ?? "",
                Title = challengeProto?.Challenge?.Title ?? "",
				Description = challengeProto?.Challenge?.Description ?? "",
				CreatedOn = DateTime.TryParse(challengeProto?.Challenge?.CreatedOn,out DateTime dateTime) ? dateTime : default(DateTime),
				Notes = challengeProto?.Challenge?.Notes ?? "",
				RequestStage = challengeProto.Challenge.RequestStage,
                ChallengeAttachments = challengeProto?.Challenge?.ChallengeAttachments?.Select(x => x.Path).ToList()
            };
		}
	}
}
