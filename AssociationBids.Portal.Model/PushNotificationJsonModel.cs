using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model
{
    public class PushNotificationJsonModel
    {
        public long bid_vendor_id { get; set; }
        public string by_company_name { get; set; }
        public long by_resource_key { get; set; }
        public string by_vendor_name { get; set; }
        public string date_added { get; set; }
        public long for_resource_key { get; set; }
        public string last_modification_date { get; set; }
        public int module_key { get; set; }
        public string notification_id { get; set; }
        public string notification_text { get; set; }
        public string notification_type { get; set; }
        public long object_key { get; set; }
        public string res_link { get; set; }
        public string status { get; set; }
        public int status_key { get; set; }
        public string title { get; set; }

        public string BidTitle { get; set; }

        public string BidDueDate { get; set; }
        public string ResponseDueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string bid_vendor_status { get; set; }

    }
}
