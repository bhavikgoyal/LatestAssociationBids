using System;

namespace AssociationBids.Portal.Model
{
    public class CalendarModel : BaseModel
    {
        public int CalendarKey { get; set; }
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int ObjectKey { get; set; }
        public string Subject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AllDayEvent { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int Status { get; set; }
    }
}
