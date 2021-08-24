using System;

namespace AssociationBids.Portal.Model
{
    public class BidFilterModel : BaseFilterModel
    {
        public int BidVendorKey { get; set; }
        public int ResourceKey { get; set; }
        public int BidStatus { get; set; }
    }
}
