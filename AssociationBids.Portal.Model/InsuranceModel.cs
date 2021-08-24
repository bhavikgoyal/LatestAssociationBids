using System;

namespace AssociationBids.Portal.Model
{
    public class InsuranceModel : BaseModel
    {
        public int InsuranceKey { get; set; }
        public int VendorKey { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public Decimal InsuranceAmount { get; set; }
        public string AgentName { get; set; }
        public string Email { get; set; }
        public string Work { get; set; }
        public string CellPhone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartDates { get; set; }
        public string EndDates { get; set; }
        public DateTime RenewalDate { get; set; }
        public string RenewalDates { get; set; }
        public int Status { get; set; }

        public int TotalRecords { get; set; }

        public int priority { get; set; }
        public string NotificationId { get; set; }
        public string NotificationType { get; set; }
    }
}
