using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter email.")]
        //[Required]
        //[RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        //[StringLength(254, ErrorMessage = "Max 254 characters")]
        public string email { get; set; }

        //[Required(ErrorMessage = "Please enter password.")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Password { get; set; }
        public int isinsurance { get; set; }

    }
}
