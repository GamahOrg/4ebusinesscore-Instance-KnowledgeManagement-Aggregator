namespace KnowledgeManagement.Aggregator.Dtos.SubKnowledges
{
    public class SubKnowledgeMapper
    {
        #region FromAllSubKnowledgeProto
        public static AllSubKnowledgeDto FromAllSubKnowledgeProto(AllSubKnowledgeProto SubKnowledge, GetOrgHierarchyByIdsResponse orgProto)
        {
            return new AllSubKnowledgeDto
            {
                Id = Guid.TryParse(SubKnowledge.Id, out Guid parsedId) ? parsedId : default(Guid),
                OrgNameAr = orgProto?.Orgs?.Where(user => user.Id.ToLower() == SubKnowledge.OrgHierarchyId.ToLower())?.FirstOrDefault()?.NameAr ?? "",
                OrgNameEn = orgProto?.Orgs?.Where(user => user.Id.ToLower() == SubKnowledge.OrgHierarchyId.ToLower())?.FirstOrDefault()?.NameEn ?? "",
                Title = SubKnowledge.Title ?? "",
                Details = SubKnowledge.Details ?? "",
                CreatedOn = DateTime.TryParse(SubKnowledge.CreatedOn, out DateTime dateTime) ? dateTime : default(DateTime),
                KnowledgeRates = SubKnowledge.KnowledgeRates != null ? SubKnowledge.KnowledgeRates : 0,
                KnowledgeLikes = SubKnowledge.KnowledgeLikes !=null ? SubKnowledge.KnowledgeLikes : 0,
                KnowledgeDislikes = SubKnowledge.KnowledgeDislikes != null ? SubKnowledge.KnowledgeDislikes : 0,
                KnowledgeViews = SubKnowledge.KnowledgeViews != null ? SubKnowledge.KnowledgeViews : 0,
                IsFavoirate = SubKnowledge.IsFavoirate,
                IsDisliked = SubKnowledge.IsDisliked,
                IsLiked = SubKnowledge.IsLiked,
                KnowledgeId = SubKnowledge.KnowledgeId != string.Empty || SubKnowledge.KnowledgeId != null ? Guid.Parse(SubKnowledge.KnowledgeId) : Guid.Empty,
                KnowledgeName = SubKnowledge.KnowledgeName ?? "",
                Date = DateTime.TryParse(SubKnowledge.Date, out DateTime date) ? date : default(DateTime),
                
                //CategoryNameAr = SubKnowledge.CategoryNameAr ?? "",
                //CategoryNameEn = SubKnowledge.CategoryNameEn ?? "",
            };
        }
        #endregion

        #region FromSubKnowledgeById
        public static SubKnowledgeByIdDto FromSubKnowledgeById(ResponseSubKnowledgeById SubKnowledge, GetOrgHierarchyByIdsResponse orgsProto, ResponseUserDataList userProto, GetOrgHierarchyByIdResponse orgProto)
        {
            return new SubKnowledgeByIdDto
            {
                Id = Guid.TryParse(SubKnowledge.SubKnowledge.Id, out Guid parsedId) ? parsedId : default(Guid),
                OrgNameAr = orgProto?.NameAr ?? "",
                OrgNameEn = orgProto?.NameEn ?? "",
                Title = SubKnowledge.SubKnowledge.Title ?? "",
                Details = SubKnowledge.SubKnowledge.Details ?? "",
                Targets = SubKnowledge.SubKnowledge.Targets ?? "",
                Legislation = SubKnowledge.SubKnowledge.Legislation ?? "",
                CreatedOn = DateTime.TryParse(SubKnowledge.SubKnowledge.CreatedOn, out DateTime dateTime) ? dateTime : default(DateTime),
                KnowledgeRates = SubKnowledge.SubKnowledge.KnowledgeRates,
                KnowledgeLikes = SubKnowledge.SubKnowledge.KnowledgeLikes,
                KnowledgeDislikes = SubKnowledge.SubKnowledge.KnowledgeDislikes,
                KnowledgeViews = SubKnowledge.SubKnowledge.KnowledgeViews,
                IsFavoirate = SubKnowledge.SubKnowledge.IsFavoirate,
                IsDisliked = SubKnowledge.SubKnowledge.IsDisliked,
                IsLiked = SubKnowledge.SubKnowledge.IsLiked,
                //KnowledgeId = SubKnowledge.SubKnowledge.KnowledgeId != null ?  Guid.Parse(SubKnowledge.SubKnowledge.KnowledgeId) : Guid.Empty,
                KnowledgeId = SubKnowledge.SubKnowledge.KnowledgeId != "" ?  Guid.Parse(SubKnowledge.SubKnowledge.KnowledgeId) : Guid.Empty,
                KnowledgeName = SubKnowledge.SubKnowledge.KnowledgeName ?? "",
                Date = SubKnowledge.SubKnowledge.Date != "" ? DateTime.Parse(SubKnowledge.SubKnowledge.Date) : null,
                //CategoryNameAr = SubKnowledge.SubKnowledge.CategoryNameAr ?? "",
                //CategoryNameEn = SubKnowledge.SubKnowledge.CategoryNameEn ?? "",
                KnowledgeAttachments = SubKnowledge?.SubKnowledge?.Attachment?.Select(x => new AttachmentDto
                {
                    FileName = x.FileName ?? "",
                    FilePath = x.FilePath ?? ""
                }).ToList(),
                ParticipatingParties = orgsProto.Orgs.Select(x => new ParticipatingPartyDto
                {
                    NameAr = x.NameAr ?? "",
                    NameEn = x.NameEn ?? "",
                    Id = x.Id
                }).ToList(),
                WorkParticipants = userProto?.UserData?.Select(x => new WorkParticipantDto
                {
                    FirstName = x.FirstName ?? "",
                    SecondName = x.SecondName ?? "",
                    ThirdName = x.ThirdName ?? "",
                    FourthName = x.FourthName ?? "",
                    UserImgUrl = x.ImgUrl ?? "",
                    UserId = x.UserId.ToLower() ?? ""
                }).ToList()
            };
            
        }
        #endregion
    }
}
