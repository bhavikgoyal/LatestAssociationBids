using System;

namespace AssociationBids.Portal.Model
{
    public class CompanyVendorFilterModel : BaseFilterModel
    {
        public int CompanyKey { get; set; }
        public int VendorKey { get; set; }
        public int Status { get; set; }
    }
}
