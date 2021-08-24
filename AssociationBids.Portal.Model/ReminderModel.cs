using System;

namespace AssociationBids.Portal.Model
{
    public class ReminderModel : BaseModel
    {
        public int ReminderKey { get; set; }
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int ObjectKey { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int Status { get; set; }
    }
}
