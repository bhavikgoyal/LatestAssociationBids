using System;

namespace AssociationBids.Portal.Model
{
    public class BidVendorModel : BaseModel
    {
        public bool checkimg { get; set; }

        public int BidVendorKey { get; set; }
        public int BidRequestKey { get; set; }
        public int VendorKey { get; set; }
        public int ResourceKey { get; set; }
        public string BidVendorID { get; set; }
        public string ForResourceKey { get; set; }
        public bool IsAssigned { get; set; }
        public DateTime RespondByDate { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int BidVendorStatus { get; set; }
        public string[] img { get; set; }
        public string BidStartDate { get; set; }
        //public bool canDelete { get; set; }
    }
}
