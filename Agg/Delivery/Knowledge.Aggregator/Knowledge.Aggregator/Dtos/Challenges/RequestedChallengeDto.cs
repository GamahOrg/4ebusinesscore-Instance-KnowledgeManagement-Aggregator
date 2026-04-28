namespace KnowledgeManagement.Aggregator.Dtos.Challenges
{
	public class RequestedChallengeDto
	{
		public Guid Id { get; set; }
        public Guid challegeCategoryId { get; set; }
        public string LK_ChallengeCategoryNameAr { get; set; }
        public string LK_ChallengeCategoryNameEn { get; set; }
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
        public List<string>? ChallengeAttachments { get; set; }=new List<string>();
    }
}
