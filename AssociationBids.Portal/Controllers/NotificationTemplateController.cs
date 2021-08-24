using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Controllers
{
    public class NotificationTemplateController : Controller
    {
        public enum NotificationType
        {
            BidReqMsg,
            BidReqStatus,
            BidVendorStatus,
            BidReqStatusAccept,
            BidReqStatusReject,
            BidReqStatusRejByAcceptOther,
            VendorReg,
            RefundReq,
            RefundReqMsg,
            CCExpiry,
            CCExpired,
            MembershipExpiry,
            MembershipExpired,
            InsuranceExpiry,
            InsuranceExpired,
        }

        private readonly string[] varNames = new string[]
        {
            "[CompanyName] = Company Name",
            "[ContactPersonName] = Contact Person Name",
            "[BidName] = Bid/Wordorder Title",
            "[Status] = Bid Staus",
            "[ResponseDueDate] = Bid Response Due Date",
            "[BidDueDate] = Bid Due Date",
            "[CCNumber] = Credit Card Masked Number",
            "[PolicyNumber] = Policy Number",
            "[MembershipExpiryDate] = Membership Expiry Date",
            "[CCExpiryDate] = Credit Card Expiry Date",
            "[InsuranceExpiryDate] = Insurance Expiry Date",
            "[BidVendorStatus] = Bid Vendor Status"
        };

        private readonly AssociationBids.Portal.Service.Base.Interface.INotificationTemplateServices _nOtificationTemplateServices;
        public NotificationTemplateController(AssociationBids.Portal.Service.Base.Interface.INotificationTemplateServices NotificationTemplateServices)
        {
            this._nOtificationTemplateServices = NotificationTemplateServices;
        }
        public ActionResult NotifiCationTemplateList()
        {
                IList<NotificationTmpModel> lNotificationTmp = null;
            lNotificationTmp = _nOtificationTemplateServices.GetAll();
            for(int i = 0; i < lNotificationTmp.Count; i++)
            {
                NotificationType type;
                Enum.TryParse<NotificationType>(lNotificationTmp[i].Title, out type);
                string title = GetTitlForNotificationType(type);
                if(type.ToString() == lNotificationTmp[i].Title)
                    lNotificationTmp[i].Title = title;
            }
                return View(lNotificationTmp);
          
        }
        public JsonResult IndexreStaffPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {

            List<NotificationTmpModel> lstNotificationTemplate = null;
            lstNotificationTemplate = _nOtificationTemplateServices.SearchUser(PageSize, PageIndex, Search.Trim(), Sort);
            return Json(lstNotificationTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexreStaffPagingForAdvancedSearch(int PageSize, int PageIndex, string Search, string TitleType, string Sort)
        {

            List<NotificationTmpModel> lstNotificationTemplate = null;
            lstNotificationTemplate = _nOtificationTemplateServices.AdvancedSearchNotificationTemplate(PageSize, PageIndex, Search.Trim(), TitleType, Sort);
            for (int i = 0; i < lstNotificationTemplate.Count; i++)
            {
                NotificationType type;
                Enum.TryParse<NotificationType>(lstNotificationTemplate[i].Title, out type);
                string title = GetTitlForNotificationType(type);
                if (type.ToString() == lstNotificationTemplate[i].Title)
                    lstNotificationTemplate[i].Title = title;
            }
            return Json(lstNotificationTemplate, JsonRequestBehavior.AllowGet);
        }

        // GET: AddNotificationTmp
        public ActionResult NotificationTmpAdd()
        {
            FillNotificationType();
            return View();
        }
        [HttpPost]
        public ActionResult NotificationTmpAdd(NotificationTmpModel  collection, HttpPostedFileBase[] files)
        {
            try
            {
              
                    Int64 value = 0;
                    value = _nOtificationTemplateServices.Insert(collection);
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Record has been inserted successfully.";
                    }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                    }
                
               
                return RedirectToAction("NotifiCationTemplateList");

            }
            catch (Exception ex)
            {

                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("NotifiCationTemplateList");

            }
        }
        public  void FillNotificationType()
        {
            try
            {
                IList<LookUpModel> lLookUp = null;
                lLookUp = _nOtificationTemplateServices.GetAllTitleForNotification();
                List<System.Web.Mvc.SelectListItem> lLookUpList = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lLookUpList.Add(sli2);
                for (int i = 0; i < lLookUp.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lLookUp[i].Title);
                    NotificationType type;
                    Enum.TryParse<NotificationType>(lLookUp[i].Title, out type);
                    string title = GetTitlForNotificationType(type);
                    sli.Text = title;
                    sli.Value = Convert.ToString(lLookUp[i].LookUpKey);
                    if(title.Trim() != "")
                        lLookUpList.Add(sli);
                }
                ViewBag.lstlookUp = lLookUpList;
            }
            catch (Exception ex)
            {

                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
        }
        private string GetTitlForNotificationType(NotificationType type)
        {
            string title = "";
            switch (type)
            {
                case NotificationType.BidReqMsg: title = "Messages";
                    break;
                case NotificationType.BidReqStatus: title = "Bid Request Status";
                    break;
                case NotificationType.BidReqStatusAccept: title = "Bid Request Accept";
                    break;
                case NotificationType.BidReqStatusRejByAcceptOther: title = "Bid request status reject when accept other";
                    break;
                case NotificationType.BidReqStatusReject: title = "Bid request status reject";
                    break;
                case NotificationType.BidVendorStatus: title = "Bid Vendor Status";
                    break;
                case NotificationType.CCExpiry: title = "Credit card about to expire";
                    break;
                case NotificationType.InsuranceExpired: title = "Insurance expired";
                    break;
                case NotificationType.InsuranceExpiry: title = "Insurance about to expire";
                    break;
                case NotificationType.MembershipExpiry: title = "Membership about to expire";
                    break;
                case NotificationType.RefundReq: title = "Refund Request";
                    break;
                case NotificationType.RefundReqMsg: title = "Refund Request Message";
                    break;
                case NotificationType.CCExpired: title = "Credit Card has expired";
                    break;
                case NotificationType.MembershipExpired: title = "Membership has expired";
                    break;
            }

            return title;
        }
        public ActionResult NoticationTemplateView(int PushNotificaionTemplateKey)
        {
            try
            {
                List<NotificationTmpModel> NotificationList = null;
                NotificationTmpModel NotiFicationDirectory = new NotificationTmpModel();
                NotiFicationDirectory = _nOtificationTemplateServices.GetDataViewEdit(PushNotificaionTemplateKey);
                NotificationType type;
                Enum.TryParse<NotificationType>(NotiFicationDirectory.Title, out type);
                string title = GetTitlForNotificationType(type);
                if (type.ToString() == NotiFicationDirectory.Title)
                    NotiFicationDirectory.Title = title;

                return View(NotiFicationDirectory);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return View();

        }
        [HttpPost]
        public ActionResult NoticationTemplateView(NotificationTmpModel  collection)
        {
            try
            {
                Int64 value = 0;


                value = _nOtificationTemplateServices.NotificationEdit(collection);
                if (value != 0)
                {
                    TempData["Sucessmessage"] = "Record has been  successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("NotifiCationTemplateList");
            }
            catch (Exception ex)
            {
                return View("NotifiCationTemplateList");
            }

        }

        // GET: EditNotification
        public ActionResult NoticationTemplateEdit(int PushNotificaionTemplateKey)
        {
            FillNotificationType();

            NotificationTmpModel resources = null;
            resources = _nOtificationTemplateServices.GetDataViewEdit(PushNotificaionTemplateKey);
            return View(resources);
        }

        [HttpPost]
        public ActionResult NoticationTemplateEdit(int PushNotificaionTemplateKey, NotificationTmpModel  collection)
        {
            try
            {
               

                    Int64 value = 0;
                    value = _nOtificationTemplateServices.NotificationEdit(collection);
                    if (value == 0)
                    {
                        TempData["SuccessMessage"] = "Record has been  updated successfully.";
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Error";
                    }
                return RedirectToAction("NoticationTemplateView", new { PushNotificaionTemplateKey });

        
            }
            catch (Exception ex)
            {
                return View("NotifiCationTemplateList");
            }
        }
        
        public ActionResult Notificationupdates(int PushNotificaionTemplateKey, NotificationTmpModel collection)
        {
            try
            {
                Int64 value = 0;
                value = _nOtificationTemplateServices.Notificationupdates(collection);
                if (value != 0)
                {
                    TempData["Sucessmessage"] = "Record has been inserted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("NotifiCationTemplateList");

            }
            catch (Exception ex)
            {
                return View("NotifiCationTemplateList");
            }
        }
        
        public ActionResult Delete(Int32 PushNotificaionTemplateKey)
        {
            try
            {
                bool Status = false;
                Status = _nOtificationTemplateServices.Remove(PushNotificaionTemplateKey);
                if (Status == true)
                {
                    TempData["Sucessmessage"] = "Record has been deleted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return RedirectToAction("NotifiCationTemplateList");

        }
        public JsonResult GetVariablesForNotification(string type)
        {
            if (type.Contains("--Please Select--"))
                return Json("", JsonRequestBehavior.AllowGet);
            return Json(GetVarsByType(type), JsonRequestBehavior.AllowGet);
        }

        public string GetVarsByType(string type)
        {
            string vars = varNames[0] + ",</br>" + varNames[1] + ",</br>";
            type = type.ToLower();
            if (type == "messages" || type == "bid request status reject" || 
                type == "bid request status reject when accept other")
            {
                vars += varNames[2] + ",</br>";
            }
            else if (type == "bid request status" || type == "bid vendor status"
                || type == "bid request accept")
            {
                vars += varNames[2] + ",</br>" + varNames[3] + ",</br>" + varNames[4] + ",</br>" + varNames[5] + ",</br>";
                if (type == "bid vendor status")
                    vars += varNames[11] + ",</br>";
            }
            else if (type == "credit card about to expire" || type == "credit Card has expired")
            {
                vars = varNames[6] + ",</br>" + varNames[9] + ",</br>";
            }
            else if (type == "insurance expired" || type == "insurance about to expire")
            {
                vars = varNames[7] + ",</br>" + varNames[10] + ",</br>";
            }
            else if (type == "membership about to expire" || type == "membership has expired")
            {
                vars += varNames[8] + ",</br>";
            }
            return vars;
        }
    }
}
