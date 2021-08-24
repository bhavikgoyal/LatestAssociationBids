using System;

namespace AssociationBids.Portal.Model
{
    public class BidModel : BaseModel
    {
        public int BidKey { get; set; }
        public int BidVendorKey { get; set; }
        public int ResourceKey { get; set; }
        public string Title { get; set; }
        public Decimal Total { get; set; }
        public string Description { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int BidStatus { get; set; }
    }
}
