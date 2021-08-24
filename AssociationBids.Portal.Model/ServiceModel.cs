using System;

namespace AssociationBids.Portal.Model
{
    public class ServiceModel : BaseModel
    {
        public int ServiceKey { get; set; }
        public int ParentServiceKey { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int TotalRecords { get; set; }
    }
}
