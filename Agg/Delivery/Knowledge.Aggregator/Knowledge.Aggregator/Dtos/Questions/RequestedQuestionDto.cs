namespace KnowledgeManagement.Aggregator.Dtos.Blogs
{
	public class RequestedQuestionDto
	{
		public Guid Id { get; set; }
		public string Body { get; set; }
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
        public List<ExpertInfo>? Experts { get; set; }
    }
    public class ExpertInfo
    {
        public Guid ExpertId { get; set; }
        public string ExpertEmail { get; set; }
        public string ExpertName { get; set; }

    }
}


