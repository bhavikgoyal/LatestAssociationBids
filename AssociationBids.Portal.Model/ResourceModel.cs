using System;
using System.ComponentModel.DataAnnotations;

namespace AssociationBids.Portal.Model
{
    public class ResourceModel : BaseModel
    {

        //[Required]
        
        public VendorManagerVendorModel Vendor { get; set; }
        public ResourceModel Resource { get; set; }
        public VendorServiceModel ServiceModel { get; set; }
        public int  VendorKey { get; set; }
        public DocumentModel Document { get; set; }
        public InsuranceModel Insurance { get; set; }
        public UserModel UserModel { get; set; }
        public BidRequestModel BidRequest { get; set; }
        public int ObjectKey { get; set; }
        public string FileName { get; set; }
        public string Username { get; set; }                        
        public string ServiceTitle1 { get; set; }                        
        public int ResourceKey { get; set; }
        public int ServiceKey { get; set; }
        public int ServiceKey1 { get; set; }
        public int ServiceKey2 { get; set; }
        public int CompanyKey { get; set; }
        public int ResourceTypeKey { get; set; }
        public bool AccountLocked { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }
        public string Title { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email2 is required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter a valid email address")]
        public string Email2 { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string HomePhone2 { get; set; }
        public string Work { get; set; }
        public string Work2 { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateKey { get; set; }
        public string MaskedCCNumber { get; set; }
        public string Company { get; set; }
        public string Zip { get; set; }
        public bool PrimaryContact { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModificationTime { get; set; }
       // [Required(ErrorMessage = "Please Select Status.")]
        public int Status { get; set; }
        public string StatusValue { get; set; }
        public string CssClassName { get; set; }

        public Int32 TotalRecords { get; set; }
        //ServiceArea
        public int Radius { get; set; }
        public string RadiusKey { get; set; }
        public string ServiceAddress { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}
