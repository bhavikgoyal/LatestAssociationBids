using System;

namespace AssociationBids.Portal.Model
{
    public class MessageModel : BaseModel
    {
        public int MessageKey { get; set; }
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int ObjectKey { get; set; }
        public string Body { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int MessageStatus { get; set; }
    }
}
