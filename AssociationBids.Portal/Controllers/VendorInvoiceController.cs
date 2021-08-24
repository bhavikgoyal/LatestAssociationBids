using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PropertyModel = AssociationBids.Portal.Model.PropertyModel;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;

namespace AssociationBids.Portal.Controllers
{
    public class VendorInvoiceController : Controller
    {
        private readonly IVendorInvoiceService _VendorInvoiceService;

        public VendorInvoiceController(IVendorInvoiceService VendorInvoiceService)
        {
            this._VendorInvoiceService = VendorInvoiceService;
        }




        // GET: VendorInvoice
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult InvoiceListForVendor(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            IList<VendorInvoiceModal> itemList = null;
            if (Search == null)
            {
                Search = "";
            }

            itemList = _VendorInvoiceService.SearchUser(PageSize, PageIndex, Search, Sort, CompanyKey);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VendorInvoiceView(int InvoiceKey)
        {
            VendorInvoiceModal resources = null;
            resources = _VendorInvoiceService.GetDataViewEdit(InvoiceKey);


            if (resources.Amount != null)
            {
                //resources.Amount = resources.Amount.Substring(0, resources.Amount.Length - 2);


                if (resources.Amount != "$0.00")
                {
                    resources.PaymentApplied = "-" + resources.Amount;
                }
                else
                {
                    resources.PaymentApplied = resources.Amount;
                }
            }

            if (resources.Balance != null)
            {
                //resources.Balance = resources.Balance.Substring(0, resources.Balance.Length - 2);
            }
            else
                resources.Balance = "$0.00";

            if (resources.Balance == "$0.00")
                resources.AmtTotal = resources.Amount;
            else
                resources.AmtTotal = resources.Balance;

            if (resources.RefundAmount != null && resources.RefundAmount != "")

            {
                //resources.RefundAmount = resources.RefundAmount.Substring(0, resources.RefundAmount.Length - 2);
            }

            return View(resources);
        }

        public ActionResult RequestRefundView(int InvoiceKey)
        {
            VendorInvoiceModal resources = null;
            resources = _VendorInvoiceService.GetDataViewEdit(InvoiceKey);

            if (resources.Amount != null)
            {
                //resources.Amount = resources.Amount.Substring(0, resources.Amount.Length - 2);
            }

            if (resources.Balance != null)
            {
                //resources.Balance = resources.Balance.Substring(0, resources.Balance.Length - 2);
            }





            return View(resources);
        }


        public ActionResult RefundRequest(int InvoiceKey, string Reason)
        {
            try
            {
                bool value = false;

                VendorInvoiceModal resources = null;
                VendorInvoiceModal resources1 = null;
                resources = _VendorInvoiceService.GetDataViewEdit(InvoiceKey);
                resources1 = _VendorInvoiceService.CheckRefundRequest(resources.VendorKey, resources.InvoiceKey);
                if (resources1 == null)
                {

                    resources = _VendorInvoiceService.GetDataViewEdit(InvoiceKey);
                    int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                    value = _VendorInvoiceService.InsertRefundRequest(resources.VendorKey, resources.InvoiceKey, resources.RefrenceNumber, resources.TransDate, resources.Amount, Reason, resources.Stripe_tokenID, ResourceKey);
                    if (value == true)
                    {
                        IABNotificationService NotificationService = new AssociationBids.Portal.Service.Base.Code.ABNotificationService();
                        NotificationService.InsertNotification("RefundReq", 200, InvoiceKey, ResourceKey, "Refund Request");
                        string lookUpTitle = "RefundRequest";

                       
                        _VendorInvoiceService.RefundMailToAdmin(lookUpTitle, resources.Title, resources.Amount, Session["Name"].ToString());
                        lookUpTitle = "RefundRequest Acknowledgement";
                        _VendorInvoiceService.RefundMailToVendor(lookUpTitle, resources.Title, resources.Amount, Convert.ToInt32(Session["resourceid"].ToString()));
                        TempData["Sucessmessage"] = "Refund Request has been Sent successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Sucessmessage"] = "Error";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Sucessmessage"] = " Refund Request Already Sent .";
                    return RedirectToAction("VendorInvoiceView", new { InvoiceKey });

                }

            }

            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult vendorRefundIndex()
        {
            return View();
        }


        public JsonResult IndexvendorPagingRefund(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            IList<VendorInvoiceModal> itemList = null;
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            itemList = _VendorInvoiceService.RefundVendorList(PageSize, PageIndex, Search.Trim(), Sort, ResourceKey);
            if (itemList.Count > 0)
                itemList.ToList().ForEach(f => f.isPriority = false);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexvendorPagingRefundPriority(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            IList<VendorInvoiceModal> itemList = null;
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            itemList = _VendorInvoiceService.RefundVendorListPriority(PageSize, PageIndex, Search.Trim(), Sort, ResourceKey);
            if (itemList.Count > 0)
                itemList.ToList().ForEach(f => f.isPriority = true);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendInsertMessage(string Body, string Status, Int64 ObjectKey, string ModuleKeyName)
        {
            string Title = "";
            Int64 ResourceKey = Convert.ToInt32(Session["resourceid"]);
            var bidRequestModel = this._VendorInvoiceService.SendInsertMessage(Body, Status, ObjectKey, ResourceKey, ModuleKeyName, Title);
            IABNotificationService notificationService = new AssociationBids.Portal.Service.Base.Code.ABNotificationService();
            notificationService.InsertNotification("RefundReqMsg", 200, Convert.ToInt32(ObjectKey), Convert.ToInt32(ResourceKey), "Refund Request Message");
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName)
        {

            var bidRequestModel = this._VendorInvoiceService.MessageNewCount(0, 0, ModuleKeyName, Convert.ToInt64(Session["userid"]));
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendInsertMessageList(Int64 ObjectKey, string ModuleKeyName, string UpdatemsgStatus)
        {
            Int64 ResourceKey = Convert.ToInt32(Session["resourceid"]);
            var bidRequestModel = this._VendorInvoiceService.SendInsertMessageList(ObjectKey, ResourceKey, ModuleKeyName, UpdatemsgStatus);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RefundInvoiceView(int InvoiceKey)
        {
            VendorInvoiceModal resources = null;
            resources = _VendorInvoiceService.GetDataViewEdit(InvoiceKey);


            if (resources.Amount != null)
            {
                //resources.Amount = resources.Amount.Substring(0, resources.Amount.Length - 2);
                resources.Amount = Common.Utility.FormatNumberHelper(resources.Amt, true);
            }

            if (resources.Balance != null)
            {
                //resources.Balance = resources.Balance.Substring(0, resources.Balance.Length - 2);
                resources.Balance = Common.Utility.FormatNumberHelper(resources.BAl, true);
            }
            else
                resources.Balance = "$0.00";




            return View(resources);
        }

        public ActionResult RefundAmount(int InvoiceKey)
        {
            VendorInvoiceModal resources = null;
            resources = _VendorInvoiceService.GetDataViewEdit(InvoiceKey);

            if (resources.Amount != null)
            {
                resources.Amount = resources.Amount.Substring(0, resources.Amount.Length - 2);
            }

            if (resources.Balance != null)
            {
                resources.Balance = resources.Balance.Substring(0, resources.Balance.Length - 2);
            }
            return View(resources);
        }


        public async System.Threading.Tasks.Task<ActionResult> ProcessToRefund(int InvoiceKey, string Reason, string Strip_tokenId)
        {
            VendorInvoiceModal registrationmodel = new VendorInvoiceModal();
            string value1 = "";
            VendorInvoiceModal resources = null;
            bool value2 = false;
            try
            {
                value1 = await Common.Payment.RefundPayAsync(Strip_tokenId);

                if (value1.Contains("Success"))
                {
                    if (value1.Split('?')[1] != "")
                    {
                        registrationmodel.Stripe_tokenID = value1.Split('?')[1];

                        if (value1.Split('?')[0].Trim() == "Success")
                        {
                            value2 = _VendorInvoiceService.UpdateRefundRequest(InvoiceKey, Reason, registrationmodel.Stripe_tokenID);
                        }
                    }
                    resources = _VendorInvoiceService.GetDataViewEdit(InvoiceKey);
                    string lookUpTitle = "Refund Process";
                    _VendorInvoiceService.RefundMailToVendor(lookUpTitle, resources.Title, resources.Amount, resources.ResourceKey);

                    TempData["Sucessmessage"] = "Refund has been proceeded successfully";
                    return RedirectToAction("vendorRefundIndex");


                }
                else
                {
                    ViewData["Sucessmessage"] = "Error";
                    return RedirectToAction("vendorRefundIndex");
                }


            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return RedirectToAction("vendorRefundIndex");
            }

        }








    }
}