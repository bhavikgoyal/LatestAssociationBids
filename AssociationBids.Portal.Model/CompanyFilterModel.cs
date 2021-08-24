using System;

namespace AssociationBids.Portal.Model
{
    public class CompanyFilterModel : BaseFilterModel
    {
        public int ParentCompanyKey { get; set; }
        public int RelatedCompanyKey { get; set; }
        public int CompanyTypeKey { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
