using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace AssociationBids.Portal.Model
{
    public class APIModel : BaseModel
    {
        public int BidRequestKey { get; set; }
        public int BidVendorKey { get; set; }
        public int BidRequestResponseDays { get; set; }
        public int BidSubmitDays { get; set; }
        public int Amount { get; set; }
        public string BidvendorId { get; set; }
        public string bdate { get; set; }
        public string PropertyKey { get; set; }
        public string propertyTitle { get; set; }
        public string stripeEmail { get; set; }
        public string stripeToken { get; set; }

        public string PaymentMethodId { get; set; }

        public string CardHoldername { get; set; }
        public string Line1 { get; set; }
        public string PostalCode { get; set; }
        public string country { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CardExpiryYear { get; set; }
        public string CVV { get; set; }
        public int  PaymentTypeKey { get; set; }
        public string CardNumber { get; set; }
        public string cardExpirydate { get; set; }
        public string Renewaldate { get; set; }
        public int ResourceKey { get; set; }
        public int ServiceKey { get; set; }
        public decimal Amountdecimal { get; set; }
        public string Title { get; set; }
        public DateTime BidDueDate { get; set; }
        public string BidDueDates { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDates { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int ModuleKey { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int BidRequestStatus { get; set; }
        public int WCount { get; set; }
        public string BidReqStatus { get; set; }
        public string Property { get; set; }
        public int NewMessageCount { get; set; }
        public string Service { get; set; }
        public string ResponseDueDates { get; set; }
        public DateTime ResponseDueDate { get; set; }
        public decimal BidAmount { get; set; }
        public string BidAmounts { get; set; }
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] Files { get; set; }

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
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public  string  Rnumber { get; set; }
        public string VendorName { get; set; }
        public int VendorKey { get; set; }
        public string LastWorkDate { get; set; }
        public int CompanyKey { get; set; }
        public string Address { get; set; }
        public string Work { get; set; }
        public string Work2 { get; set;}

        public string FileName { get; set; }
        public string ServiceTitle { get; set; }
        public int InsuranceKey { get; set; }
        public string PolicyNumber { get; set; }
        public int InsuranceAmount { get; set; }
        public string InsuranceDate { get; set; }
        public string InsuranceExprie { get; set; }

        public string NotesDescription { get; set; }
        public string Notesdatetime { get; set; }
        public int NoteKey { get; set; }

        [NotMapped]
        public string TotalApceptRecord { get; set; }
        public string RNumber { get; set; }

        public string FirstName { get; set; }
        public string EmailId { get; set; }
        public string BidName { get; set; }
    }
}
