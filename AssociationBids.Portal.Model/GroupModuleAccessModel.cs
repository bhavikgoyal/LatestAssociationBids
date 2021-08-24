using System;

namespace AssociationBids.Portal.Model
{
    public class GroupModuleAccessModel : BaseModel
    {
        public int GroupModuleAccessKey { get; set; }
        public int PortalKey { get; set; }
        public int GroupKey { get; set; }
        public int ModuleKey { get; set; }
        public int Access { get; set; }
    }
}
