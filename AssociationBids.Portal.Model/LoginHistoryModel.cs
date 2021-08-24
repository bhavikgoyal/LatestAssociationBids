using System;

namespace AssociationBids.Portal.Model
{
    public class LoginHistoryModel : BaseModel
    {
        public int LoginHistoryKey { get; set; }
        public int UserKey { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
