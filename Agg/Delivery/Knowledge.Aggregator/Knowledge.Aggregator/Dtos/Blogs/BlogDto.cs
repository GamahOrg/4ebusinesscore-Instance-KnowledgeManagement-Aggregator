namespace KnowledgeManagement.Aggregator.Dtos.Blogs
{
	public class BlogDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string ThirdName { get; set; }
		public string FourthName { get; set; }
		public string UserImgUrl { get; set; }
		public string Email { get; set; }
		public Guid CreatedBy { get; set; }
		public DateTime CreatedOn { set; get; }
		public int CommentCount { get; set; }
		public int LikeCount { get; set; }
		public int DisLikeCount { get; set; }
		public int RequestStage { set; get; }
		public bool IsLiked { set; get; }
		public bool IsDisliked { set; get; }
		public List<string>? BlogAttachments { get; set; }
	}
}
