using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model
{
    public class ForgotPasswordModel
    {
        public string email { get; set; }
        public string UserName { get; set; }
        public string ResetExpirationDate { get; set; }
        public string CompanyName { get; set; }
        public int UserKey { get; set; }
    }
}
