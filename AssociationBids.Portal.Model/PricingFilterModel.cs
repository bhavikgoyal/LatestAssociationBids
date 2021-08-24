using System;

namespace AssociationBids.Portal.Model
{
    public class PricingFilterModel : BaseFilterModel
    {
        public int CompanyKey { get; set; }
        public int PricingTypeKey { get; set; }
    }
}
