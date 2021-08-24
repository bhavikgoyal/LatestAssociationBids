using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Service.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace AssociationBids.Portal.Controllers
{
    public class PushNotificationController : Controller
    {
        // GET: PushNotification
        public ActionResult Index()
        {
            return View();
        }


        private static PushResponse sendpush(string UserToken, object NotificationPayLoad)
        {
            PushResponse obj_p = new PushResponse();
            string FirebaseApiID = ConfigurationManager.AppSettings["FirebaseApiID"];
            string FirebaseSenderID = ConfigurationManager.AppSettings["FirebaseSenderID"];

            if (FirebaseSenderID == null || FirebaseSenderID == null)
            {
                obj_p.failure = 1;
                obj_p.results.Add(new Result("No Firebase Sender ID or API key found"));
                return obj_p;
            }

            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            //WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/v1/projects/association-bids-415e0/messages:send");
            string DeviceType = "";
            string Token = "";
            try
            {
                Token = Regex.Split(UserToken,"#DeviceType#")[0].Trim();
                DeviceType = Regex.Split(UserToken, "#DeviceType#")[1].Trim();
            }
            catch { }

            tRequest.Method = "POST";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", FirebaseApiID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", FirebaseSenderID));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = Token,  /*"eg8t-PgE5zQ:APA91bFdDqeoQUoM0UXji-aSbBathyHH0YVRkLmLjKLTEOBpiD00ahpGwq_ORiczMQ2lVNAMKTvc9oJHg-T_LAQyCsWbgUgdPp0tJPe9oCMiEzZsCd17eQ6mG1I45sunRXBa-TJ5QNlT",*/
                priority = "high",
                notification = new { content_available = true },
                data = NotificationPayLoad,
                project_id = "association-bids-415e0",
            };
            var n = new PushNotificationJsonModel();
            try
            {
                n = (PushNotificationJsonModel)NotificationPayLoad;
            }
            catch { n = null; }
            var noti = new
            {
                to = Token,
                priority = "high",
                data = new
                {
                    title = "New Notification",
                    text = "You have new notification",
                    content_available = true,
                    sound = "enabled",
                    id = "AB",
                    data = NotificationPayLoad
                },
                NotificationPayLoad,
                project_id = "association-bids-415e0",
            };
            var notiIos = new
            {
                to = Token,
                priority = "high",
                notification = new
                {
                    title = "New Notification",
                    text = "You have new notification",
                    body = "You have new notification",
                    content_available = true,
                    sound = "enabled",
                    id = "AB",
                    data = NotificationPayLoad
                },
                project_id = "association-bids-415e0",
            };
            if (n != null)
            {
                string Notititle = n.BidTitle;
                if (n.module_key == 100)
                    Notititle = "Bid Request " + Notititle;
                else if (n.module_key == 106)
                    Notititle = "Work Order " + Notititle;
                noti = new
                {
                    to = Token,
                    priority = "high",
                    data = new
                    {
                        title = Notititle,
                        text = n.notification_text.Replace('.',' ')+" "+n.by_vendor_name,
                        content_available = true,
                        sound = "enabled",
                        id = "AB",
                        data = NotificationPayLoad
                    },
                    NotificationPayLoad,
                    project_id = "association-bids-415e0",
                };
                if (DeviceType.ToLower().Contains("ios"))
                {
                    notiIos = new
                    {
                        to = Token,
                        priority = "high",
                        notification = new
                        {
                            title = Notititle,
                            text = "Demo Text",
                            body = n.notification_text.Replace('.', ' ') + " " + n.by_vendor_name,
                            content_available = true,
                            sound = "enabled",
                            id = "AB",
                            data = NotificationPayLoad
                        },
                        project_id = "association-bids-415e0",
                    };
                }
            }
            string postbody = "";
            if(DeviceType.ToLower().Contains("ios"))
                postbody = JsonConvert.SerializeObject(notiIos).ToString();
            else
                postbody = JsonConvert.SerializeObject(noti).ToString();

            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                obj_p = JsonConvert.DeserializeObject<PushResponse>(sResponseFromServer);
                            }
                    }
                }
            }
            return obj_p;
        }

        [HttpGet]
        public JsonResult CheckPushNotification(long UserKey = 0)
        {
            List<long> Users = new List<long>();
            IPushNotificationService service = new PushNotificationService();
            List<PushResponse> pushResponses = new List<PushResponse>();
            List<PushNotificationModel> pushNotifications = new List<PushNotificationModel>();
            PushNotificationJsonModel notiPayload = new PushNotificationJsonModel();
            pushNotifications = service.SelectALLUnreadPushNotification(UserKey);
            Users.AddRange(pushNotifications.Select(s => s.ForUserKey));
            int ii = 0;
            foreach (var noti in pushNotifications)
            {
                if (ii > 0)
                    break;
                //if (noti.ForUserKey != 10453)
                //    continue;
                List<string> usertokens = new List<string>();
                //var notiPayload = new
                //{
                //    title = noti.ObjectTitle,
                //    body = noti.Body,
                //    objectkey = noti.ObjectKey,
                //    modulekey = noti.ModuleKey
                //};
                notiPayload = new PushNotificationJsonModel();
                notiPayload.date_added = noti.DateAdded.ToString("M/d/yyyy hh:mm:ss tt");
                notiPayload.last_modification_date = new DateTime().ToString("M/d/yyyy hh:mm:ss tt"); ;
                notiPayload.for_resource_key = noti.ForResourceKey;
                //notiPayload.Id = noti.PushNotificationKey;
                notiPayload.notification_type = noti.Type;
                notiPayload.object_key = noti.ObjectKey;
                notiPayload.title = noti.ModuleKey == 100 ? "Bid Request" : noti.ModuleKey == 106 ? "Work Order" : noti.ObjectTitle;
                notiPayload.BidTitle = noti.ObjectTitle;
                //notiPayload.Title = noti.ObjectTitle;
                notiPayload.by_company_name = noti.ByCompanyName;
                notiPayload.by_vendor_name = noti.ByVendorName;
                notiPayload.notification_text = noti.Body;
                notiPayload.module_key = noti.ModuleKey;
                notiPayload.bid_vendor_id = noti.BidVendorKey;
                notiPayload.status = noti.Status;
                notiPayload.bid_vendor_status = noti.BidVendorStatus;
                notiPayload.BidDueDate = noti.BidDueDate;
                notiPayload.ResponseDueDate = noti.ResponseDueDate;
                notiPayload.ExpiryDate = noti.ExpiryDate;
                notiPayload.notification_id = Convert.ToString(noti.NotificationId);
                service.LoadPushNotification(notiPayload);
                var lists = service.GetUserTokensByUserKey(noti.ForUserKey);
                foreach (var l in lists)
                {
                    var p = sendpush(l, notiPayload);
                    //string multicastId = "0";
                    //string messageId = "0";
                    //string errorText = "";
                    //try
                    //{
                    //    multicastId = p.multicast_id;
                    //    messageId = p.results[0].message_id;
                    //    if (p.failure == 1)
                    //        errorText = p.results[0].error;
                    //}
                    //catch {
                    //    try
                    //    {
                    //        if (p.failure == 1)
                    //            errorText = p.results[0].error;
                    //    }
                    //    catch { }
                    //}
                    //service.UpdateStatus(noti.PushNotificationKey, multicastId, messageId, errorText);
                    ii++;
                }
                
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SendPushNotificationById(long PushNotificationKey)
        {
            List<long> Users = new List<long>();
            IPushNotificationService service = new PushNotificationService();
            List<PushResponse> pushResponses = new List<PushResponse>();
            List<PushNotificationModel> pushNotifications = new List<PushNotificationModel>();
            PushNotificationJsonModel notiPayload = new PushNotificationJsonModel();
            pushNotifications = service.PushNotification_SelectById(PushNotificationKey);
            Users.AddRange(pushNotifications.Select(s => s.ForUserKey));
            int ii = 0;
            foreach (var noti in pushNotifications)
            {
                if (ii > 0)
                    break;
                
                List<string> usertokens = new List<string>();
                
                notiPayload = new PushNotificationJsonModel();
                notiPayload.date_added = noti.DateAdded.ToString("M/d/yyyy hh:mm:ss tt");
                notiPayload.last_modification_date = new DateTime().ToString("M/d/yyyy hh:mm:ss tt");
                notiPayload.for_resource_key = noti.ForResourceKey;
                notiPayload.notification_type = noti.Type;
                notiPayload.object_key = noti.ObjectKey;
                notiPayload.title = noti.ModuleKey == 100 ? "Bid Request" : noti.ModuleKey == 106 ? "Work Order" : noti.ObjectTitle;
                notiPayload.BidTitle = noti.ObjectTitle;
                notiPayload.by_company_name = noti.ByCompanyName;
                notiPayload.by_vendor_name = noti.ByVendorName;
                notiPayload.notification_text = noti.Body;
                notiPayload.module_key = noti.ModuleKey;
                notiPayload.bid_vendor_id = noti.BidVendorKey;
                notiPayload.status = noti.Status;
                notiPayload.bid_vendor_status = noti.BidVendorStatus;
                notiPayload.BidDueDate = noti.BidDueDate;
                notiPayload.ResponseDueDate = noti.ResponseDueDate;
                notiPayload.ExpiryDate = noti.ExpiryDate;

                service.LoadPushNotification(notiPayload);
                notiPayload.notification_id = Convert.ToString(noti.NotificationId);
                
                var lists = service.GetUserTokensByUserKey(noti.ForUserKey);
                PushResponse p = new PushResponse();
                if (lists.Count > 0)
                {
                    foreach (var l in lists)
                    {
                        p = sendpush(l, notiPayload);
                        //string multicastId = "0";
                        //string messageId = "0";
                        //string errorText = "";
                        //try
                        //{
                        //    multicastId = p.multicast_id;
                        //    messageId = p.results[0].message_id;
                        //    if (p.failure == 1)
                        //        errorText = p.results[0].error;
                        //}
                        //catch {
                        //    try
                        //    {
                        //        if (p.failure == 1)
                        //            errorText = p.results[0].error;
                        //    }
                        //    catch { }
                        //}
                        //service.UpdateStatus(noti.PushNotificationKey, multicastId, messageId, errorText);
                        ii++;
                    }
                }
                else
                {
                    p.results = new List<Result>(1);
                    p.results[0].error = "Custome Error: RegistrationToken not found";
                    p.results[0].message_id = "0";
                    p.failure = 1;
                    p.success = 0;
                    p.multicast_id = "0";
                }
                return Json(p, JsonRequestBehavior.AllowGet);
            }
            return Json(new PushResponse(), JsonRequestBehavior.AllowGet);
        }
    }
}