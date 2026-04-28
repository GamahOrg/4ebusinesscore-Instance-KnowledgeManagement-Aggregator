namespace KnowledgeManagement.Aggregator.Dtos.SubKnowledges
{
	public class AllSubKnowledgeDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Details { get; set; }
		public DateTime CreatedOn { get; set; }
        public Guid KnowledgeId { get; set; }
        public string KnowledgeName { get; set; }
        public DateTime? Date { get; set; }
        //public string CategoryNameAr { get; set; }
        //public string CategoryNameEn { get; set; }
        public string OrgNameAr { set; get; }
		public string OrgNameEn { set; get; }
		public double KnowledgeRates { get; set; }
		public int KnowledgeViews { get; set; }
		public bool IsFavoirate { get; set; }
		public int KnowledgeLikes { get; set; }
		public int KnowledgeDislikes { get; set; }
		public bool IsLiked { set; get; }
		public bool IsDisliked { set; get; }
	}
}
