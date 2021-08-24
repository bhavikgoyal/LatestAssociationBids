using System;

namespace AssociationBids.Portal.Model
{
    public class VendorServiceModel : BaseModel
    {
        public int VendorServiceKey { get; set; }
        public int VendorKey { get; set; }
        public int ServiceKey { get; set; }
        public int ServiceKey1 { get; set; }
        public int ServiceKey2 { get; set; }
        public double SortOrder { get; set; }
    }
}
