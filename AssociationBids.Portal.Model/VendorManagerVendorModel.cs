using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AssociationBids.Portal.Model
{
    public class VendorManagerVendorModel : BaseModel
    {
        [Required(ErrorMessage = "Please select type.")]
        public int CompanyKey { get; set; }
        public int PropertyKey { get; set; }
        public int VendorKey { get; set; }
        public int InsuranceKey { get; set; }
        public string TaxID { get; set; }
        public string ServiceTitle1 { get; set; }
        public string Password { get; set; }
        public string PropertyTitle1 { get; set; }
        public string CompanyName { get; set; }
        public string LegalName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Work { get; set; }
        public string Work2 { get; set; }
        public string Fax { get; set; }
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
        public decimal InsuranceAmount { get; set; }
        public int Phone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RenewalDate { get; set; }
        public string Description { get; set; }
        public string StateKey { get; set; }
        public Int32 TotalRecords { get; set; }
        public string Favorite { get; set; }
        public int Status { get; set; }
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

        public int UserKey { get; set; }
        public string UserName { get; set; }
        public string ResetExpirationDate { get; set; }
        public int Radius { get; set; }
        //public string FileSize { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public int priority { get; set; }
        public long NotificationId { get; set; }
        public string NotificationType { get; set; }
        public bool isPriority { get; set; }
    }

}

