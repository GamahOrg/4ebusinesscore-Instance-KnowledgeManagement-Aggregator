namespace KnowledgeManagement.Aggregator.Dtos.IdeaBank
{
	public class GetAllIdeaCommentsDTO
	{
		public Guid Id { get; set; }
		public string CommentText { get; set; }
		public DateTime CreatedOn { set; get; }
		public Guid CreatedBy { set; get; }
		public Guid IdeaBankId { get; set; }
		public Guid? ParentCommentId { get; set; }
		public List<GetAllIdeaCommentsDTO> Replies { get; set; }
        public int CommentLikesCount { set; get; }
        public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string ThirdName { get; set; }
		public string FourthName { get; set; }
		public string UserImgUrl { get; set; }
	}
}