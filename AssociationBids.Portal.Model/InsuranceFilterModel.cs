using System;

namespace AssociationBids.Portal.Model
{
    public class InsuranceFilterModel : BaseFilterModel
    {
        public int VendorKey { get; set; }
        public string State { get; set; }
        public int Status { get; set; }
    }
}
