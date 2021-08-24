using System;
using System.ComponentModel.DataAnnotations;

namespace AssociationBids.Portal.Model
{
    public class CompanyModel : BaseModel
    {
        public int CompanyKey { get; set; }
        public int ResourceKey { get; set; }
        public int ParentCompanyKey { get; set; }
        public int RelatedCompanyKey { get; set; }
        public int CompanyTypeKey { get; set; }
        public int PortalKey { get; set; }
        public string CompanyID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string LegalName { get; set; }
        public string TaxID { get; set; }
        public string Work { get; set; }
        public string Work2 { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateKey { get; set; }
        public string Zip { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public int BidRequestResponseDays { get; set; }
        public int BidSubmitDays { get; set; }
        public Decimal BidRequestAmount { get; set; }
        public int NotificationPreference { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModificationTime { get; set; }

        public int Status { get; set; }
       
        public Int32 TotalRecords { get; set; }

        //Resource
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
      ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        public string CellPhone { get; set; }
        [Display(Name = "Yes")]
        public bool PrimaryContact { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //property
        public string ServiceTitle1 { get; set; }
        public int ServiceKey { get; set; }
        public string Title { get; set; }

        public string StateName { get; set; }
        public string BidRequestAmounts { get; set; }
        public double BidRequestAmountss { get; set; }
    }
}
