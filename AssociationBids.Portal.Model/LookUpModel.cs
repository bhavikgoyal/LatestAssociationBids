using System;

namespace AssociationBids.Portal.Model
{
    public class LookUpModel : BaseModel
    {
        public int LookUpKey { get; set; }
        public int LookUpTypeKey { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public double SortOrder { get; set; }
        public string LookUpKey1 { get; set; }
    }
}
