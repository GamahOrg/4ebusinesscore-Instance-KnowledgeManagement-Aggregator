namespace KnowledgeManagement.Aggregator.Dtos.IdeaBank
{
	public class RequestedIdeaDto
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
        public int Lk_InputTypeId { get; set; }
        public string Lk_InputTypeNameAr { get; set; }
        public string Lk_InputTypeNameEn { get; set; }
        public Guid CreatedBy { get; set; }
		public DateTime CreatedOn { set; get; }
		public string? Notes { set; get; }
		public int RequestStage { set; get; }
		public bool IsPublished { set; get; }
        public List<string> IdeaAttachments { get; set; } = new List<string>();
		public int IdeaCategory {  get; set; }

    }
}
