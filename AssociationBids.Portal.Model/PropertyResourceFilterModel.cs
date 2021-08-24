using System;

namespace AssociationBids.Portal.Model
{
    public class PropertyResourceFilterModel : BaseFilterModel
    {
        public int PropertyKey { get; set; }
        public int ResourceKey { get; set; }
        public int Status { get; set; }
    }
}
