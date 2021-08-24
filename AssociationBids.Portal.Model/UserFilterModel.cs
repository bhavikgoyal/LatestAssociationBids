using System;

namespace AssociationBids.Portal.Model
{
    public class UserFilterModel : BaseFilterModel
    {
        public int ResourceKey { get; set; }
        public string Username { get; set; }
        public string TokenReset { get; set; }
        public int Status { get; set; }
    }
}
