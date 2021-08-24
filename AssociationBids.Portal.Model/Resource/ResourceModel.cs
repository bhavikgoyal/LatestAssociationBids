using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model.Resource
{
    public class ResourceModel
    {
        public int ResourceKey { get; set; }
        //public companyKey CompanyKey { get; set; }
        //public int CompanyKey { get; set; }
        public int ResourceTypeKey { get; set; }

        
        [Required(ErrorMessage = "Please enter User Name.")]
        //[Required]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Select Status.")]
        //[Required]
        public status Status { get; set; }

        [Required(ErrorMessage = "Please Select Company.")]
        //[Required]
        public companyKey CompanyKey { get; set; }

        //[Required(ErrorMessage = "Please enter First Name.")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Please enter Title.")]
        public string Title { get; set; }

        //[Required(ErrorMessage = "Please enter Email.")]
        public string Email { get; set; }

        public string Email2 { get; set; }

        //[Required(ErrorMessage = "Please enter CellPhone.")]
        public string CellPhone { get; set; }

        //[Required(ErrorMessage = "Please enter HomePhone.")]
        public string HomePhone { get; set; }

        public string HomePhone2 { get; set; }

        //[Required(ErrorMessage = "Please enter Work.")]
        public string Work { get; set; }

        public string Work2 { get; set; }

        //[Required(ErrorMessage = "Please enter Fax.")]
        public string Fax { get; set; }

        //[Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }

        public string Address2 { get; set; }

        //[Required(ErrorMessage = "Please enter City.")]
        public string City { get; set; }

        //[Required(ErrorMessage = "Please enter State.")]
        public state State { get; set; }
        //public string State { get; set; 

        //[Required(ErrorMessage = "Please enter Zip.")]
        public string Zip { get; set; }

        public bool PrimaryContact { get; set; }
        public string Description { get; set; }

        //[Required(ErrorMessage = "Please Select Status.")]
        //public status Status { get; set; }
    }
}
public enum state
{
    Choose_one,
    State_1,
    State_2,
    State_3
}
public enum companyKey
{
    Choose_one,
    Company_name_1,
    Company_name_2,
    Company_name_3
}
public enum status
{
    Choose_one,
    Pending,
    Approve,
    Unapprove
}

