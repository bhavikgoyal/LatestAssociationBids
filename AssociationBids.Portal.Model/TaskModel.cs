using System;

namespace AssociationBids.Portal.Model
{
    public class TaskModel : BaseModel
    {
        public int TaskKey { get; set; }
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int AssignedToKey { get; set; }
        public int ObjectKey { get; set; }
        public string Subject { get; set; }
        public int TaskStatus { get; set; }
        public int TaskPriority { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
