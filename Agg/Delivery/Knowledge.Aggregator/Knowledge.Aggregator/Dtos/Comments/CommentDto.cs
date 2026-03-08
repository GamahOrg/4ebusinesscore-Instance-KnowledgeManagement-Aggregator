namespace KnowledgeManagement.Aggregator.Dtos.Comments
{
	public class CommentDto
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public Guid ChallengeId { get; set; }
		public string? CommentAttachment { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string ThirdName { get; set; }
		public string FourthName { get; set; }
		public string UserImgUrl { get; set; }
		public Guid CreatedBy { get; set; }
		public DateTime CreatedOn { set; get; }
		public List<CommentDto> Replies { get; set; }
	}
}
