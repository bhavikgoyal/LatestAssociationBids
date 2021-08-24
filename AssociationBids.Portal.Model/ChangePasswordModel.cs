using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AssociationBids.Portal.Model
{
    public class ChangePasswordModel : BaseModel
    {
        public string  UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Old Password.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Please Enter New Password.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please Enter Confirm Password.")]
        public string ConfirmPassword { get; set; }
        public int UserKey { get; set; }
        public string BindAgreementDetails { get; set; }
        public int AggrementKey { get; set; }
        public string TokenReset { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }
}
