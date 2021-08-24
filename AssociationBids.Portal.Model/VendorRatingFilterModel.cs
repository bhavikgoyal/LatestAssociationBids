using System;

namespace AssociationBids.Portal.Model
{
    public class VendorRatingFilterModel : BaseFilterModel
    {
        public int VendorKey { get; set; }
        public int ResourceKey { get; set; }
    }
}
