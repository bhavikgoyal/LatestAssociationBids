using System;

namespace AssociationBids.Portal.Model
{
    public class EmailFilterModel : BaseFilterModel
    {
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int ObjectKey { get; set; }
        public int EmailStatus { get; set; }
       

    }
}
