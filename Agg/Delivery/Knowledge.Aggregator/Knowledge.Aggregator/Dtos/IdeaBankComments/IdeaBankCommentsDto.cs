namespace KnowledgeManagement.Aggregator.Dtos.IdeaBankComments
{
    public class IdeaBankCommentsDto
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedOn { set; get; }
        public Guid CreatedBy { set; get; }
        public Guid IdeaBankId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public List<IdeaBankCommentsDto> Replies { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public string UserImgUrl { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public bool IsLiked { set; get; }
        public bool IsDisliked { set; get; }
    }
}
