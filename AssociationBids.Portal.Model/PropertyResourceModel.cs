using System;

namespace AssociationBids.Portal.Model
{
    public class PropertyResourceModel : BaseModel
    {
        public int PropertyResourceKey { get; set; }
        public int PropertyKey { get; set; }
        public int ResourceKey { get; set; }
        public DateTime DateAdded { get; set; }
        public int Status { get; set; }
    }
}
