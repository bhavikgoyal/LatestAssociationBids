using System;

namespace AssociationBids.Portal.Model
{
    public class BidRequestFilterModel : BaseFilterModel
    {
        public int PropertyKey { get; set; }
        public int ResourceKey { get; set; }
        public int ServiceKey { get; set; }
        public int BidRequestStatus { get; set; }
    }
}
