using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AssociationBids.Portal.Model
{
    public class VendorModel : BaseModel
    {

        public InsuranceModel Insurance { get; set; }
        public DocumentModel Document { get; set; }

        [Required(ErrorMessage = "Please select type.")]
        public int CompanyKey { get; set; }
        public int PropertyKey { get; set; }
        public string[] img { get; set; }
        public bool checkimg { get; set; }
        public int VendorKey { get; set; }
        public int BidVendorKey { get; set; }
        public int NoofBid { get; set; }
        public string star { get; set; }
        public string VendorName { get; set; }
        public string ServiceTitle { get; set; }
        public string Service { get; set; }
        public string ResponseDueDates { get; set; }
        public string BidReqStatus { get; set; }

        public string invited { get; set; }
        public string Duplicate { get; set; }
        public string Message { get; set; }
        public bool starvenor { get; set; }
        public decimal BidAmount { get; set; }
        public bool invitevendor { get; set; }
        public int RatingOne { get; set; }
            public int RatingTwo { get; set; }
            public int RatingThree { get; set; }
            public int RatingFour { get; set; }
            public int RatingFive { get; set; }
        public string lastmodtime { get; set; }
        public int BidRequestKey { get; set; }
        public string CompanyName { get; set; }
        public int InsuranceKey { get; set; }
        public string ServiceTitle1 { get; set; }
        public string PropertyTitle1 { get; set; }
    
        public string LegalName { get; set; }
        public string Work { get; set; }
        public string Work2 { get; set; }
        public string Fax { get; set; }
        public string Propertyname { get; set; }
        public string Website { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; } 
        public string State { get; set; }
        public string Zip { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string ContactPerson { get; set; }
        public string PolicyNumber { get; set; }
        public int InsuranceAmount { get; set; }
        public int Phone { get; set; }
        public DateTime StartDate { get; set; }
        public string sstartddate { get; set; }
        public string  BidDueDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RenewalDate { get; set; }
        public string Description { get; set; }        
        public string StateKey { get; set; }     
        public Int32 TotalRecords { get; set; }
        public string Favorite { get; set; }
        public int Status { get; set; }
        public string BidRequestStatus { get; set; }
        public int ServiceKey { get; set; }
        public int ServiceKey1 { get; set; }
        public int ServiceKey2 { get; set; }
        public string FileName { get; set; }
        public Int32 DocumentId { get; set; }
        public string FileSize { get; set; }
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] Files { get; set; }

        public void Append(string v)
        {
            throw new NotImplementedException();
        }

    }

}

