using System;

namespace AssociationBids.Portal.Model
{
    public class ModuleModel : BaseModel
    {
        public int ModuleKey { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
