using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
namespace AssociationBids.Portal.Model
{
    public class SupportModel : BaseModel
    {
        public int BidRequestKey { get; set; }
        public int BidVendorKey { get; set; }
        public string BidVendorStatus { get; set; }
        public string Descrip { get; set; }
        public string Type { get; set; }
        public int BidRequestResponseDays { get; set; }
        public int BidSubmitDays { get; set; }
        public string PropertyKey { get; set; }
        public int ResourceKey { get; set; }
        public int ServiceKey { get; set; }
        public string Title { get; set; }
        public DateTime BidDueDate { get; set; }
        public string VendorStartdates { get; set; }
        public string BidDueDates { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDates { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDates { get; set; }
        public string Description { get; set; }
        public int ModuleKey { get; set; }
        public DateTime DateAdded { get; set; }
        public string DateAddeds { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int BidRequestStatus { get; set; }
        public int WCount { get; set; }
        public string BidReqStatus { get; set; }
        public string Property { get; set; }
        public int NewMessageCount { get; set; }
        public string Service { get; set; }
        public string ResponseDueDates { get; set; }
        public string ResponseDueDatess { get; set; }
        public string DefaultResponseDueDates { get; set; }
        public DateTime ResponseDueDate { get; set; }
        public decimal BidAmount { get; set; }
        public string BidAmounts { get; set; }
        public decimal WinFee { get; set; }
        public string WinFees { get; set; }

        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] Files { get; set; }
        //
        public string VendorBidDueDate { get; set; }
        public bool isExpired { get; set; }
        public bool IsAssigned { get; set; }

        public int NoofBids { get; set; }
        public int TotalRecords { get; set; }

        public List<string> DocPath { get; set; }
        public string[] img { get; set; }
        public bool checkimg { get; set; }
        public string CompanyName { get; set; }
        public string WorkPhone1 { get; set; }
        public string WorkPhone2 { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Name { get; set; }
        public string VendorUsername { get; set; }

        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }

        public string VendorName { get; set; }
        public int VendorKey { get; set; }
        public string LastWorkDate { get; set; }
        public int CompanyKey { get; set; }
        public string Address { get; set; }
        public string Work { get; set; }
        public string Work2 { get; set; }

        public string FileName { get; set; }
        public string ServiceTitle { get; set; }
        public int InsuranceKey { get; set; }
        public string PolicyNumber { get; set; }
        public double InsuranceAmount { get; set; }
        public string InsuranceAmounts { get; set; }
        public string InsuranceDate { get; set; }
        public string InsuranceExprie { get; set; }

        public string NotesDescription { get; set; }
        public string Notesdatetime { get; set; }
        public int NoteKey { get; set; }

        [NotMapped]
        public string TotalApceptRecord { get; set; }


        public string FirstName { get; set; }
        public string EmailId { get; set; }
        public string BidName { get; set; }

        public int priority { get; set; }
        public string NotificationId { get; set; }
        public string NotificationType { get; set; }
        public bool ispriorityrecord { get; set; }
    }
}
