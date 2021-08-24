using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AssociationBids.Portal.Model
{
   public  class NotificationTmpModel
    {
        public int PushNotificaionTemplateKey { get; set; }
        public string PushNotificationTitle { get; set; }
        public Int32 PushNotificationType { get; set; }
        public string NTSubject { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public Int32 TotalRecords { get; set; }
        public string DateAdded { get; set; }
        public Int32 lookUpType { get; set; }
        public string Title { get; set; }

    }
}
