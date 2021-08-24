using System;

namespace AssociationBids.Portal.Model
{
    public class PaymentFilterModel : BaseFilterModel
    {
        public int VendorKey { get; set; }
        public int PaymentTypeKey { get; set; }
    }
}
