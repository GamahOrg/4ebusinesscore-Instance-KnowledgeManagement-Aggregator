namespace KnowledgeManagement.Aggregator.Dtos.Blogs
{
	public class QuestionDto
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
		public int CommentCount { get; set; }
		public int RequestStage { set; get; }
		public int QuestionStatus { get; set; }
		public string? OrgNameAr { get; set; }
        public string? OrgNameEn { get; set; }
		public Guid? OrgId { get; set; }
        public string?  SpecializationNameAr { get; set; }
        public string? SpecializationNameEn { get; set; }
		public int Type { get; set; }

		public List<ExpertInfo> Experts { get; set; }

    }
}
