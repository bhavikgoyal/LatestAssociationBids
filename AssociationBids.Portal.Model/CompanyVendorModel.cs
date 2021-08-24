using System;

namespace AssociationBids.Portal.Model
{
    public class CompanyVendorModel : BaseModel
    {
        public int CompanyVendorKey { get; set; }
        public int CompanyKey { get; set; }
        public int VendorKey { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int Status { get; set; }
    }
}
