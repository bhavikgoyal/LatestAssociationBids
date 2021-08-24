using System;

namespace AssociationBids.Portal.Model
{
    public class PropertyVendorDistanceModel : BaseModel
    {
        public int PropertyVendorDistanceKey { get; set; }
        public int PropertyKey { get; set; }
        public int VendorKey { get; set; }
        public double Distance { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
