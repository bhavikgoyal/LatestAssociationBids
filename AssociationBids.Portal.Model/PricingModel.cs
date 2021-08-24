using System;
using System.ComponentModel.DataAnnotations;

namespace AssociationBids.Portal.Model
{
    public class PricingModel : BaseModel
    {
        public int PricingKey { get; set; }
        public int CompanyKey { get; set; }
        public int PricingTypeKey { get; set; }
        public Decimal StartAmount { get; set; }
        public Decimal EndAmount { get; set; }

        [Required(ErrorMessage = "Fee is required")]
        public Decimal Fee { get; set; }
        public DateTime LastModificationTime { get; set; }
        public double SortOrder { get; set; }

        public string Company { get; set; }
        public string PricingType { get; set; }

        public Int32 TotalRecords { get; set; }

        public string FeeType { get; set; }
    }
}
