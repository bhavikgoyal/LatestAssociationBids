using System;

namespace AssociationBids.Portal.Model
{
    public class AgreementModel : BaseModel
    {
        public int AgreementKey { get; set; }
        public int PortalKey { get; set; }
        public string Title { get; set; }
        public DateTime AgreementDate { get; set; }
        public string AgreementDates { get; set; }
        public string Description { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int Status { get; set; }
        public Int32 TotalRecords { get; set; }
    }
}
