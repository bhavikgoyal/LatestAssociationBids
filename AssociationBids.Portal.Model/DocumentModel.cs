using System;

namespace AssociationBids.Portal.Model
{
    public class DocumentModel : BaseModel
    {
        public int DocumentKey { get; set; }
        public int ModuleKey { get; set; }
        public int ObjectKey { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
