using System;

namespace AssociationBids.Portal.Model
{
    public class GroupMemberFilterModel : BaseFilterModel
    {
        public int GroupKey { get; set; }
        public int ResourceKey { get; set; }
    }
}
