using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AssociationBids.Portal.Model
{
    public class StaffDirectoryModel : BaseModel
    {
        //[Required]
        public int ResourceKey { get; set; }

        public int CompanyKey { get; set; }
        public int ResourceTypeKey { get; set; }
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }
        public string GgroupKey { get; set; }
        
        [Required(ErrorMessage = "*")]
       
        public string Email { get; set; }
        public string OldEmail { get; set; }
        public string Email2 { get; set; }
        public string CellPhone { get; set; }
        //public string HomePhone { get; set; }
        //public string HomePhone2 { get; set; }
        public string Work { get; set; }
        public string Work2 { get; set; }
        public string Fax { get; set; }
        [Display(Name = "Primary Contact")]
        public bool PrimaryContact { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateKey { get; set; }
        public string Zip { get; set; }
        
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModificationTime { get; set; }
        // [Required(ErrorMessage = "Please Select Status.")]
        public int Status { get; set; }
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConformPassword { get; set; }
        public bool GroupSelected { get; set; }
        public List<StaffDirectoryModel> GroupList { get; set; }
        public string GroupName { get; set;}
        public string GroupId { get; set; }
        public int GroupKey { get; set; }
        public Int32 TotalRecords { get; set; }
        public int NumberOfUnits { get; set; }
        public HttpPostedFileBase UploadImage { get; set; }
        public string GroupResponseId { get; set; }
        public string GUID { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        //public bool Group8 { get; set; }
        public bool Statuss { get; set; }
        public int UserKey { get; set; }
        public string lst { get; set; }

        public string ResetExpirationDate { get; set; }
        public string CompanyName { get; set; }
    }
}
