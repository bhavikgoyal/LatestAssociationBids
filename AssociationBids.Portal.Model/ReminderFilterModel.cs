using System;

namespace AssociationBids.Portal.Model
{
    public class ReminderFilterModel : BaseFilterModel
    {
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int ObjectKey { get; set; }
        public int Status { get; set; }
    }
}
