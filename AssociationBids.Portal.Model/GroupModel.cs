using System;

namespace AssociationBids.Portal.Model
{
    public class GroupModel : BaseModel
    {
        public int GroupKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
