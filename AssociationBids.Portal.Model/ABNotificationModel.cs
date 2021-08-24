using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model
{
    public class ABNotificationModel
    {
        public long Id { get; set; }
        public long NotificationId { get; set; }
        public string NotificationType { get; set; }
        public int ModuleKey { get; set; }
        public long ObjectKey { get; set; }
        public long ByResource { get; set; }
        public long ForResource { get; set; }
        public string NotificationText { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModificationDate { get; set; }
        public string Status { get; set; }
        public string resLink { get; set; }
        public string Title { get; set; }
        public string ObjectName { get; set; }
        public string ByVendorName { get; set; }
        public string ByCompanyName { get; set; }

        public int NewNotificationCount { get; set; }
        public int TotalCount { get; set; }
        public long BidVendorId { get; set; }
        public long ByResourceKey { get; set; }
        public long ForResourceKey { get; set; }
    }
}
