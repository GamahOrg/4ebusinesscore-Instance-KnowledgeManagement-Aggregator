namespace KnowledgeManagement.Aggregator.Dtos.BlogComments
{
	public class BlogCommentsDto
	{
		public Guid Id { get; set; }
		public string Body { get; set; }
		public Guid BlogId { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string ThirdName { get; set; }
		public string FourthName { get; set; }
		public string UserImgUrl { get; set; }
		public Guid CreatedBy { get; set; }
		public DateTime CreatedOn { set; get; }
		public int LikeCount { get; set; }
		public int DisLikeCount { get; set; }
		public List<BlogCommentsDto> Replies { get; set; }
		public bool IsLiked { set; get; }
		public bool IsDisliked { set; get; }
        public Guid ParentId { get; set; }
    }
}
