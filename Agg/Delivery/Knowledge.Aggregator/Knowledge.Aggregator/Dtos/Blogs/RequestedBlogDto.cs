namespace KnowledgeManagement.Aggregator.Dtos.Blogs
{
	public class RequestedBlogDto
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
		public string? Notes { set; get; }
		public int RequestStage { set; get; }
		public List<string> BlogAttachments { get; set; } = new List<string>();

    }
}
