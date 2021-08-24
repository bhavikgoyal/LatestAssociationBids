using System;

namespace AssociationBids.Portal.Model
{
    public class ServiceAreaModel : BaseModel
    {
        public int ServiceAreaKey { get; set; }
        public int VendorKey { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Radius { get; set; }
    }
}
