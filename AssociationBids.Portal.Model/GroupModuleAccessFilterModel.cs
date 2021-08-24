using System;

namespace AssociationBids.Portal.Model
{
    public class GroupModuleAccessFilterModel : BaseFilterModel
    {
        public int GroupKey { get; set; }
        public int ModuleKey { get; set; }
    }
}
