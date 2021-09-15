using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class ABNotificationController : Controller
    {
        public IABNotificationService __notification;
      
        public ABNotificationController()
        {
            
        }
        public ABNotificationController(IABNotificationService notificationService)
        {
            __notification = notificationService;
        }
        // GET: ABNotification
        public ActionResult Index()
        {
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            ViewBag.Notification = __notification.GetABNotificationsFive(ResourceKey);
            return View();
        }
        public PartialViewResult Notification()
        {
            List<ABNotificationModel> model = new List<ABNotificationModel>();
            
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            //model = __notification.GetABNotificationsFive(ResourceKey);
            model = __notification.GetABNotificationsModule(ResourceKey);
            ViewBag.Notification = model;
            return PartialView("_NotificationPartial", model);
        }
        public JsonResult UpdateStatus(int NotiId,string Status)
        {
            bool status = __notification.UpdateStatus(NotiId, Status);

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NotificationUpdate(int NotiId, string Status,string NotiType,bool ByObj = false)
        {
            bool status = false;
            var noti = new List<ABNotificationModel>();
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            if (!ByObj)
            {
                noti.Add(new ABNotificationModel());
                noti[0] = __notification.GetABNotificationByNotificationId(ResourceKey, NotiId);
            }
            else
            {
                noti = __notification.GetABNotificationsAll(ResourceKey);
                if (NotiType.Contains("_PM"))
                {
                    NotiType = NotiType.Replace("_PM", "");
                    Service.Base.IBidRequestService bid = new Service.Base.BidRequestService();
                    BidVendorModel obj = bid.GetBidVendorByBidVendorKey(NotiId);
                    noti = noti.Where(w => w.ObjectKey == obj.BidRequestKey && w.ByResource == Convert.ToInt32(obj.ForResourceKey) && w.NotificationType == NotiType && w.Status == "900").ToList();
                }
                else if(NotiType == "RejectedBidNoti")
                {
                    noti = noti.Where(w => (w.NotificationType == "BidReqStatusReject" || w.NotificationType == "BidReqStatusRejByAcceptOther") && w.Status == "900").ToList();
                }
                else
                {
                    noti = noti.Where(s => s.ObjectKey == NotiId && s.NotificationType == NotiType && s.Status == "900").ToList();
                }
            }
            if (noti != null && noti.Count > 0 && noti[0].Id > 0)
            {
                if (NotiType == "RejectedBidNoti")
                {
                    for (int k = 0; k < noti.Count; k++)
                    {
                        status = __notification.UpdateStatusByObjectKey(noti[k].ObjectKey, "read", noti[k].NotificationType);
                    }
                }
                else
                {
                    for (int i = 0; i < noti.Count; i++)
                    {
                        if (noti[i].NotificationType == NotiType)
                        {
                            status = __notification.UpdateStatus(noti[i].Id, Status);
                        }
                    }
                }

                //Notification();
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNotificationCount()
        {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            var noti = __notification.GetABNotificationsModule(ResourceKey);
           
            return Json(noti, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFiveNotificationsForVendor()
        {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            var noti = __notification.GetABNotificationsFive(ResourceKey);

            return Json(noti, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFiveNotificationsForVendorNew()
        {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            var noti = __notification.GetFiveNotificationsForVendorFiveNew(ResourceKey);

            return Json(noti, JsonRequestBehavior.AllowGet);
        }   

        public JsonResult GetFiveNotificationsForManagerNew()
        {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            
               
             
            var noti = __notification.GetFiveNotificationsForManagerFiveNew(ResourceKey, 0);

            return Json(noti, JsonRequestBehavior.AllowGet);
        }
    }
}