using System;

namespace AssociationBids.Portal.Model
{
    public class GroupMemberModel : BaseModel
    {
        public int GroupMemberKey { get; set; }
        public int GroupKey { get; set; }
        public int ResourceKey { get; set; }
    }
}
