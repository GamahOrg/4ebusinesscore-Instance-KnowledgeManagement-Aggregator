namespace KnowledgeManagement.Aggregator.Dtos.Knowledge
{
    public class GetAllKnowledgeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int  KnowledgeType { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        //public Guid CategoryId { get; set; }
        public List<Guid>? OrgHierarchyId { set; get; }
    }
}
