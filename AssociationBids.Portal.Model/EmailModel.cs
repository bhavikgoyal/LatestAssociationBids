using System;

namespace AssociationBids.Portal.Model
{
    public class EmailModel : BaseModel
    {
        public int EmailKey { get; set; }
        public int ModuleKey { get; set; }
        public int ResourceKey { get; set; }
        public int ObjectKey { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateSent { get; set; }
        public int EmailStatus { get; set; }
    }
}
