using System;

namespace AssociationBids.Portal.Model
{
    public class ResourceFilterModel : BaseFilterModel
    {
        public int CompanyKey { get; set; }
        public int ResourceTypeKey { get; set; }
        public string State { get; set; }
        public int Status { get; set; }
    }
}
