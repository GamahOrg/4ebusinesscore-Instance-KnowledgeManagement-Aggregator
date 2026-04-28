namespace KnowledgeManagement.Aggregator.Dtos.IdeaBank
{
    public class GetAllIdeasPaginatedDTO
    {
        public Guid Id { get; set; }
        public int IdeaCategory { get; set; }
        public int? LK_InputTypeId { get; set; }
        public string? LK_InputTypeNameAr { get; set; }
        public string? LK_InputTypeNameEn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public string UserImgUrl { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { set; get; }
        public Guid CreatedBy { set; get; }
        public bool IsLikedByLoggedInUser { get; set; }
        public bool IsDisLikedByLoggedInUser { get; set; }
        public List<string> IdeaAttachments { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int CommentCount { get; set; }
        public int IdeaViews { get; set; }
    }
}
