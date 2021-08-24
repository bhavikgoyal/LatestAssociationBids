using System;

namespace AssociationBids.Portal.Model
{
    public class PromotionModel : BaseModel
    {
        public int PromotionKey { get; set; }
        public int CompanyKey { get; set; }



        public string Title { get; set; }
        public string PromotionCode { get; set; }
        public Decimal Amount { get; set; }
        public double Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
