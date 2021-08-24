using System;

namespace AssociationBids.Portal.Model
{
    public class UserAgreementModel : BaseModel
    {
        public int UserAgreementKey { get; set; }
        public int UserKey { get; set; }
        public int AgreementKey { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
