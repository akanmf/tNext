using System;

namespace tNext.Microservices.Campaign.Api.Model
{
    [Serializable]
    public class UX_CampaignMaster
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string HtmlContent { get; set; }
        public virtual string Image { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string SearchKeyword { get; set; }
        public virtual int CampaignShowTypeId { get; set; }
        public virtual int OrderNumber { get; set; }
        public virtual int PairId { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string CampaignLink { get; set; }
        public virtual bool IsWeb { get; set; }
        public virtual bool IsMobile { get; set; }
    }
}