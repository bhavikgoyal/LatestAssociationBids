using System;

namespace AssociationBids.Portal.Model
{
    public class InvoiceFilterModel : BaseFilterModel
    {
        public int VendorKey { get; set; }
        public int Status { get; set; }
    }
}
