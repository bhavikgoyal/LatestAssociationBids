using System;

namespace AssociationBids.Portal.Model
{
    public class MembershipModel : BaseModel
    {
        public int MembershipKey { get; set; }
        public int VendorKey { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RenewalDate { get; set; }
        public bool AutomaticRenewal { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime LastModificationTime { get; set; }

        public string MaskedCCNumber { get; set; }

        public int priority { get; set; }
        public string NotificationId { get; set; }
        public string NotificationType { get; set; }
    }
}
