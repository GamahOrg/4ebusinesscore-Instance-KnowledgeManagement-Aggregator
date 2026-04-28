
namespace KnowledgeManagement.Aggregator.Dtos.Knowledge
{
    public class UserAndIOrgIndicatorsDto
    {
        public int CountOfInteractiveUsers { get; set; }  // عدد المتفاعلين مع المحتوى
        public List<GetSubKnowledgeInfo>? SubKnowledges { get; set; }//
        public List<GetKnowledgeInfo>? Knowledges { get; set; }//
        public List<OrgInfo>? Orgs { get; set; }
        public List<UserInfo>? Users { get; set; }
    }

    public class GetSubKnowledgeInfo
    {
        public Guid SubKnowledgeId { get; set; }//
        public string SubKnowledgeTitle { get; set; }//

        public int CountOfOrgs { get; set; } //عدد المؤسسات المرتبطه مع كل محتوى 
        public int CountOfUsers { get; set; } //عدد مستخدمين  مع كل محتوى


    }

    public class GetKnowledgeInfo
    {
        public Guid KnowlegeId { get; set; }
        public string KnowlegeName { get; set; }
        public int type { get; set; }
        public int CountOfOrgs { get; set; }
    }

    public class OrgInfo
    {
        public Guid OrgId { get; set; }
        public string? OrgNameAr { get; set; }
        public string? OrgNameEn{ get; set; }
        public int CountOfSubKnowledges { get; set; }
    }

    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string? UserFullName { get; set; }
        public int CountOfContributions { get; set; } // المساهمات (عدد المشاهدات + عدد التقيمات+عدد الاعجابات + المفضلات
    }
}
