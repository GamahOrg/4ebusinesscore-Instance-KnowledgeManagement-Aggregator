using IdeaBank.API.Protos;

namespace KnowledgeManagement.Aggregator.Dtos.IdeaBankComments
{
    public class IdeaBankCommentMapper
    {
        public static IdeaBankCommentsDto GetAllIdeaBankCommentsProto(CommentsForIdeaBanks commentProto, ResponseUserDataList userProto, Dictionary<string, IdeaBankCommentsDto> processedComments)
        {
            if (processedComments.TryGetValue(commentProto.Id, out var existingDto))
            {
                return existingDto;
            }

            var dto = new IdeaBankCommentsDto
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
                LikeCount = commentProto.CommentLikes.Where(l => l.LikeStatus == 0)?.Count() ?? 0,
                DisLikeCount = commentProto.CommentLikes.Where(l => l.LikeStatus == 1)?.Count() ?? 0,
                Replies = commentProto.Replies?
                    .Select(reply => GetAllIdeaBankCommentsProto(reply, userProto, processedComments))
                    .ToList() ?? new List<IdeaBankCommentsDto>(),
                IsDisliked= commentProto.IsDisliked,
                IsLiked= commentProto.IsLiked,        
                ParentCommentId= Guid.TryParse(commentProto.ParentCommentId, out Guid ParentCommentId) ? ParentCommentId : default(Guid),
            };

            processedComments[commentProto.Id] = dto;

            return dto;
        }
    }
}
