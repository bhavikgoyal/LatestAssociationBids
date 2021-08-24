using System;

namespace AssociationBids.Portal.Model
{
    public class UserAgreementFilterModel : BaseFilterModel
    {
        public int UserKey { get; set; }
        public int AgreementKey { get; set; }
    }
}
