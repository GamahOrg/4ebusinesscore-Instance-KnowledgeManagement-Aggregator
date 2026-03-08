namespace KnowledgeManagement.Aggregator.Dtos.Challenges
{
	public class GetAllChallengeDto
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
		public Guid CreatedBy { get; set; }
		public DateTime CreatedOn { set; get; }
		public int CommentCount { get; set; }
		public int RequestStage { set; get; }
        public List<string>? ChallengeAttachments { get; set; }
    }
}
