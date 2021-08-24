using System;

namespace AssociationBids.Portal.Model
{
    public class PortalModel : BaseModel
    {
        public int PortalKey { get; set; }
        public string PortalID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string SiteImage { get; set; }
        public string HomePageImage { get; set; }
        public string Stylesheet { get; set; }
        public string Description { get; set; }
        public int NotificationSetting { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int Status { get; set; }
    }
}
