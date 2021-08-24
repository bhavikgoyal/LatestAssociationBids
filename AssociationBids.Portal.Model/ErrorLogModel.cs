using System;

namespace AssociationBids.Portal.Model
{
    public class ErrorLogModel : BaseModel
    {
        public int ErrorLogKey { get; set; }
        public string Details { get; set; }
        public string Session { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
