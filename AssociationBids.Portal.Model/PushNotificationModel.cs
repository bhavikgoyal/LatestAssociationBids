using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model
{
    public class PushNotificationModel
    {
        public long PushNotificationKey { get; set; }
        public long ResourceKey { get; set; }
        public long ForResourceKey { get; set; }
        public long ForUserKey { get; set; }
        public int ModuleKey { get; set; }
        public long ObjectKey { get; set; }
        public string ObjectTitle { get; set; }
        public string RegistrationToken { get; set; }
        public string Type { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateSent { get; set; }
        public string ByVendorName { get; set; }
        public string ByCompanyName { get; set; }
        public long BidVendorKey { get; set; }
        public long NotificationId { get; set; }
        public string BidVendorStatus { get; set; }
        public string BidVendorStatusKey { get; set; }
        public string BidDueDate { get; set; }
        public string ResponseDueDate { get; set; }
        public string ExpiryDate { get; set; }
    }

    public class PushResponse
    {
        public string multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public string canonical_ids { get; set; }
        public List<Result> results { get; set; }

    }

    public class Result
    {
        public Result()
        {

        }
        public Result(string message)
        {
            message_id = message;
        }

        public string message_id { get; set; }

        public string error { get; set; }
    }


}
