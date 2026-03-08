namespace KnowledgeManagement.Aggregator.Dtos.BlogComments
{
	public class QuestionCommentsDto
	{
		public Guid Id { get; set; }
		public string Body { get; set; }
		public Guid QuestionId { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string ThirdName { get; set; }
		public string FourthName { get; set; }
		public string UserImgUrl { get; set; }
		public string Attachment { get; set; }
		public Guid CreatedBy { get; set; }
		public DateTime CreatedOn { set; get; }
		public List<QuestionCommentsDto> Replies { get; set; }
	}
}
