using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model.Login
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public int ResourceId { get; set; }
        public int GroupKey { get; set; }
        public int PortalKey { get; set; }
        public string UserName { get; set; }
        public string Companyname { get; set; }
        public string password { get; set; }
        public bool FirstTimeAccess { get; set; }
        public bool Termaccpected { get; set; }
        public int companyTypeKey { get; set; }
        public int companyKey { get; set; }
        public int InsauranceKey { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }

    }
}
