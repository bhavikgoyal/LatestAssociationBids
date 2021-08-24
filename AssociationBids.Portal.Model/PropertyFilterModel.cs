using System;

namespace AssociationBids.Portal.Model
{
    public class PropertyFilterModel : BaseFilterModel
    {
        public int CompanyKey { get; set; }
        public string State { get; set; }
        public int Status { get; set; }
    }
}
