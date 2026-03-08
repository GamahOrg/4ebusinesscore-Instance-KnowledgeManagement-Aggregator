namespace KnowledgeManagement.Aggregator.Dtos.IdeaBank
{
    public class IdeaBankDetailsDTO
    {
        public Guid Id { get; set; }
        public int IdeaCategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public string UserImgUrl { get; set; }
        public string Email { get; set; }
        public int Lk_InputTypeId {  get; set; }
        public string Lk_InputTypeNameAr {  get; set; }
        public string Lk_InputTypeNameEn {  get; set; }
        public DateTime CreatedOn { set; get; }
        public Guid CreatedBy { set; get; }
        public bool IsLikedByLoggedInUser { get; set; }
        public bool IsDisLikedByLoggedInUser { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int CommentsCount { get; set; }
        public double Rates { get; set; }
        public List<string> IdeaAttachments { get; set; }
    }
}
