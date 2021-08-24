using System;

namespace AssociationBids.Portal.Model
{
    public class MessageFilterModel : BaseFilterModel
    {
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int ObjectKey { get; set; }
        public int MessageStatus { get; set; }
    }
}
