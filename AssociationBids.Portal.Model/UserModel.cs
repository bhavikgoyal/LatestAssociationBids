using System;

namespace AssociationBids.Portal.Model
{
    public class UserModel : BaseModel
    {
        public int UserKey { get; set; }
        public int ResourceKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TokenReset { get; set; }
        public DateTime ResetExpirationDate { get; set; }
        public bool AccountLocked { get; set; }
        public bool FirstTimeAccess { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int Status { get; set; }
    }
}
