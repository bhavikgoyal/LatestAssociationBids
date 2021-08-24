using System;

namespace AssociationBids.Portal.Model
{
    public class TaskFilterModel : BaseFilterModel
    {
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int AssignedToKey { get; set; }
        public int ObjectKey { get; set; }
        public int TaskStatus { get; set; }
        public int TaskPriority { get; set; }
    }
}
