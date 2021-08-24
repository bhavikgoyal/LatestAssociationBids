using System;

namespace AssociationBids.Portal.Model
{
    public class BidVendorFilterModel : BaseFilterModel
    {
        public int BidRequestKey { get; set; }
        public int VendorKey { get; set; }
        public int ResourceKey { get; set; }
        public int BidVendorStatus { get; set; }
    }
}
