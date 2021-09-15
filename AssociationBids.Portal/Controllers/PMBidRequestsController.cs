using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class PMBidRequestsController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IBidRequestService _bidRequestservice;
        private readonly AssociationBids.Portal.Service.Base.Interface.IVendorManagerService __vendorManagerservice;
        private readonly AssociationBids.Portal.Service.Base.Interface.IPMPropertiesService __pMPropertiesService;
        public PMBidRequestsController(AssociationBids.Portal.Service.Base.Interface.IPMPropertiesService PMPropertiesService, AssociationBids.Portal.Service.Base.IBidRequestService bidRequestService, IVendorManagerService vendorManagerService)
        {
            this.__pMPropertiesService = PMPropertiesService;
            this._bidRequestservice = bidRequestService;
            this.__vendorManagerservice = vendorManagerService;
        }
        // GET: PMBidRequests
        public ActionResult PMBidRequestList()
        {

            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            Int32 companyKey = Convert.ToInt32(Session["CompanyKey"]);
            //list of Property
            IList<BidRequestModel> lstProperty = _bidRequestservice.GetAllProperty(resourcekey, companyKey);
            List<System.Web.Mvc.SelectListItem> lstPropertylist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--All--";
            sli2.Value = "0";
            lstPropertylist.Add(sli2);
            for (int i = 0; i < lstProperty.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstProperty[i].Property);
                sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                lstPropertylist.Add(sli);
            }
            ViewBag.lstProperty = lstPropertylist;

            //list of Bid Request Status
            IList<LookUpModel> lstBidReqStatus = _bidRequestservice.GetBidRequetStatusFilter();
            List<System.Web.Mvc.SelectListItem> lstBidReqStatuslist = new List<System.Web.Mvc.SelectListItem>();
            lstBidReqStatuslist.Add(new SelectListItem() { Text = "Show All", Value = "0,0" });
            for (int i = 0; i < lstBidReqStatus.Count; i++)
            {
                lstBidReqStatuslist.Add(new SelectListItem() { Text = Convert.ToString(lstBidReqStatus[i].Title), Value = Convert.ToString(lstBidReqStatus[i].LookUpKey1) });
            }
            ViewBag.lstBidReqStatus = lstBidReqStatuslist;

            return View();
        }


        public JsonResult checkPortal()
        {



            try
            {

                int Portalkey = Convert.ToInt32(Session["Portalkey"]);
                if (Portalkey == 0 || Portalkey == 1)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

                else if (Portalkey == 3)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }


            }
            catch
            {
                return Json(null);
            }
        }
        public JsonResult IndexreStaffPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 PropertyKey, string BidRequestStatus, int Modulekey)
        {
            List<BidRequestModel> lstuser = null;
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            lstuser = _bidRequestservice.SearchBidRequest(PageSize, PageIndex, Search.Trim(), Sort, resourcekey, PropertyKey, BidRequestStatus, Modulekey);
            lstuser.ForEach(f => f.ispriorityrecord = false);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexreStaffPagingPriority(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 PropertyKey, string BidRequestStatus, int Modulekey)
        {
            List<BidRequestModel> lstuser = null;
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            lstuser = _bidRequestservice.SearchBidRequestPriority(PageSize, PageIndex, Search.Trim(), Sort, resourcekey, PropertyKey, BidRequestStatus, Modulekey);
            lstuser.ForEach(f => f.ispriorityrecord = true);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PMBidRequestAdd(string propertykey = "0")
        {
            IList<BidRequestModel> lstProperty = null;
            IList<BidRequestModel> lstService = null;
            BidRequestModel bid = new BidRequestModel();
            Session.Remove("BidRequestAddDocSession");
            //list of Property
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            Int32 companyKey = Convert.ToInt32(Session["CompanyKey"]);
            lstProperty = _bidRequestservice.GetAllProperty(resourcekey, companyKey);
            List<System.Web.Mvc.SelectListItem> lstPropertylist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lstPropertylist.Add(sli2);
            for (int i = 0; i < lstProperty.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstProperty[i].Property);
                sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                lstPropertylist.Add(sli);
            }
            ViewBag.lstProperty = lstPropertylist;

            //list of Service
            lstService = _bidRequestservice.GetAllService();
            List<System.Web.Mvc.SelectListItem> lstServicelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
            sli1.Text = "--Please Select--";
            sli1.Value = "0";
            lstServicelist.Add(sli1);
            for (int i = 0; i < lstService.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstService[i].Property);
                sli.Value = Convert.ToString(lstService[i].PropertyKey);
                lstServicelist.Add(sli);
            }
            ViewBag.lstService = lstServicelist;

            bid.PropertyKey = propertykey;


         
            Int32 Comapanykey = Convert.ToInt32(Session["CompanyKey"]);
            bid = _bidRequestservice.getbiddate(Comapanykey);

            if (bid.BidRequestResponseDays != 0 && bid.BidRequestResponseDays != null)
            {

                bid.ResponseDueDate = Common.Utility.AddBusinessDays(DateTime.Now, bid.BidRequestResponseDays);
                bid.ResponseDueDate.ToString("MM/dd/yyyy");


            }
            if (bid.BidSubmitDays != 0)
            {

                bid.BidDueDate = Common.Utility.AddBusinessDays(DateTime.Now, bid.BidSubmitDays);

                bid.BidDueDate.ToString("MM/dd/yyyy");
            }        
            return View(bid);
        }
        
        [HttpPost]
        public ActionResult PMBidRequestAdd(BidRequestModel bidRequestModel, HttpPostedFileBase[] File)
        {
            List<BidRequestModel> lstuser = null;
            return View();
        }
        
        public JsonResult BidReqRemoveAddedDocument(string fileName,string Title)
        {
            try
            {
                List<StringBuilder> BidRequestAddDocSession = new List<StringBuilder>();

                StringBuilder FileName = new StringBuilder();
                StringBuilder FileSize = new StringBuilder();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["BidRequestAddDocSession"])))
                {
                    BidRequestAddDocSession = (List<StringBuilder>)Session["BidRequestAddDocSession"];
                    if (BidRequestAddDocSession.Count > 0)
                    {
                        FileName = BidRequestAddDocSession[0];
                        FileSize = BidRequestAddDocSession[1];
                    }

                    StringBuilder newFileName = new StringBuilder();
                    StringBuilder newFileSize = new StringBuilder();
         
                 
                    int bidRequestKey = 0;
                    bidRequestKey = _bidRequestservice.DeleteDoc(Title, fileName);


                    for (Int32 ind = 0; ind < FileName.ToString().Trim().Trim(',').Trim().Split(',').Length; ind++)
                    {
                        if (fileName.Trim() == FileName.ToString().Trim().Trim(',').Trim().Split(',')[ind].Trim())
                        {
                            try
                            {
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Document/Properties/"), bidRequestKey + FileName.ToString().Trim().Trim(',').Trim().Split(',')[ind]));
                            }
                            catch (Exception)
                            { }
                        }
                        else
                        {
                            newFileName.Append(FileName.ToString().Trim().Trim(',').Trim().Split(',')[ind].Trim() + ",");
                            newFileSize.Append(newFileSize.ToString().Trim().Trim(',').Trim().Split(',')[ind].Trim() + ",");
                        }
                    }
                    FileName = newFileName;
                    FileSize = newFileSize;
                    if (BidRequestAddDocSession.Count > 0)
                    {
                        BidRequestAddDocSession[0] = (FileName);
                        BidRequestAddDocSession[1] = (FileSize);
                    }
                    else
                    {
                        BidRequestAddDocSession.Add(FileName);
                        BidRequestAddDocSession.Add(FileSize);
                    }
                }

                Session["BidRequestAddDocSession"] = BidRequestAddDocSession;
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Exception: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DocUpload()
        {
            try
            {
                bool value = false;
                List<BidRequestModel> lstVendor = null;
                BidRequestModel bidRequestModel = new BidRequestModel();
                if (Request.Form.Count > 0)
                {
                    int BidRequestKey = 0;
                    bidRequestModel.ResourceKey = Convert.ToInt32(Session["resourceid"]);
                    bidRequestModel.Property = Request.Form["Property"].ToString();
                    bidRequestModel.Service = Request.Form["Service"].ToString();
                    bidRequestModel.Title = Request.Form["Title"].ToString();
                    bidRequestModel.Description = Request.Form["Description"].ToString();
                    bidRequestModel.ModuleKey = Convert.ToInt32(Request.Form["ModuleKey"]);
                    string ResponseDueDate = Request.Form["ResponseDueDate"].ToString();
                    if (Request.Form["ResponseDueDate"].ToString() != "")
                    {
                        bidRequestModel.ResponseDueDate = Convert.ToDateTime(Request.Form["ResponseDueDate"].ToString());
                    }
                    if (Request.Form["BidDueDate"].ToString() != "")
                    {
                        bidRequestModel.BidDueDate = Convert.ToDateTime(Request.Form["BidDueDate"].ToString());
                    }
                    StringBuilder FileName = new StringBuilder();
                    StringBuilder FileSize = new StringBuilder();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        FileName.Append(Path.GetFileName(file.FileName));
                        FileName.Append(",");
                        FileSize.Append(file.ContentLength);
                        FileSize.Append(",");
                    }
                    if (Request.Files.Count != 0)
                    {
                        FileName.Remove(FileName.Length - 1, 1);
                        FileSize.Remove(FileSize.Length - 1, 1);
                    }

                    int bidRequestKey = 0;
                    bidRequestKey = _bidRequestservice.GetMaxBidRequestKey(bidRequestModel.Title);

                    if (bidRequestKey == 0)
                    {
                        value = _bidRequestservice.DocInsert(bidRequestModel, FileName.ToString(), FileSize.ToString());

                    }

                    if (bidRequestKey  != 0)
                    {
                        bidRequestModel.Property = "";
                        bidRequestModel.BidRequestKey = bidRequestKey;
                        value = _bidRequestservice.DocInsert(bidRequestModel, FileName.ToString(), FileSize.ToString());

                    }
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Document/Properties/"), bidRequestModel.BidRequestKey + " " + fileName);
                        file.SaveAs(path);
                    }

                    BidRequestKey = bidRequestModel.BidRequestKey;

                    lstVendor = _bidRequestservice.SearchVendorByBidRequest(bidRequestModel.BidRequestKey, bidRequestModel.ModuleKey, Convert.ToInt64(Session["resourceid"]));
                }

                return Json(bidRequestModel.BidRequestKey, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult PMBidStatusUpdate(string status, int BidVendorKey, int BidRequestKey)
        {
            try
            {
                long ResourceKey = Convert.ToInt64(Session["resourceid"]);
                var moduleKey = Convert.ToInt32(Request.Form["ModuleKey"]);
                var vendors = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey, moduleKey, ResourceKey);
                Service.Base.Interface.IABNotificationService notificationService = new Service.Base.Code.ABNotificationService();
                if (status == "Submitted")
                {
                    bool value = false;
                    if (vendors.Count <= 0)
                        return Json(new { status = "vendor is empty" }, JsonRequestBehavior.AllowGet);
                }
                BidRequestModel bidRequestModel = new BidRequestModel();
                DataTable dt = new DataTable();
                int WorkReportKey = 0;
                if (status != "Acceptp")
                {
                    if (status == "InProgress")
                    {
                        dt = _bidRequestservice.UpdateStatusDBReturb(status, BidRequestKey);
                    }
                    else if (status == "Submitted")
                    {
                        dt = _bidRequestservice.UpdateStatusDBReturb(status, BidRequestKey);
                    }
                    else if (status == "Closed")
                    {
                        dt = _bidRequestservice.UpdateStatusDBReturb(status, BidRequestKey);
                    }
                    else
                    {
                        if (status == "Submitted")
                            dt = _bidRequestservice.UpdateStatusDBReturb(status, BidRequestKey);
                        else
                            dt = _bidRequestservice.UpdateStatusDBReturb(status, BidVendorKey);
                    }

                    if (status == "InProgress")
                    {
                        notificationService.InsertNotification("BidReqStatus", moduleKey, BidRequestKey, ResourceKey, "New Bid Request.");
                    }
                    else if (status == "Rejectp")
                    {
                        notificationService.InsertNotification("BidReqStatusReject", moduleKey, BidVendorKey, ResourceKey, "Bid Request Rejected.");
                    }
                    else if (status == "Closed" || status == "Cancel" || status == "Submitted")
                    {
                        IABNotificationService service = new ABNotificationService();
                        List<ABNotificationModel> notifications = service.GetABNotificationsAllByObjectAndModule(0, moduleKey, BidRequestKey);
                        var noti = notifications.FindAll(f => f.Status == "900");
                        foreach (var n in noti)
                        {
                            service.UpdateStatus(n.Id, "read");
                        }
                    }
                }
                else
                {
                    WorkReportKey = _bidRequestservice.UpdateStatusDBReturb1(status, BidVendorKey, BidRequestKey);

                    if (status == "Acceptp")
                    {
                        if (moduleKey == 106)
                        {
                            notificationService.InsertNotification("BidReqStatusAccept", moduleKey, BidRequestKey, ResourceKey, "Bid Request Accepted.");
                        }
                        notificationService.InsertNotification("BidReqStatusAccept", moduleKey, WorkReportKey, ResourceKey, "Bid Request Accepted.");
                        if (moduleKey == 100)
                            notificationService.InsertNotification("BidReqStatusRejByAcceptOther", moduleKey, BidVendorKey, ResourceKey, "Bid Request Rejected By Accepting Other Vendor.", ResourceKey);
                    }


                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var FileCopyTo = dt.Rows[i]["FileCopyTo"];
                    var FilePastTo = dt.Rows[i]["FilePastTo"];
                    var path = Path.Combine(Server.MapPath("~/Document/Properties/")) + FileCopyTo;
                    var path1 = Path.Combine(Server.MapPath("~/Document/Properties/")) + WorkReportKey;

                    System.IO.File.Copy(path, path1);
                }
                if (status == "Acceptp")
                {
                    int Modulekey = 100;
                    IList<BidRequestModel> Documentlist = null;
                    Documentlist = _bidRequestservice.GetbindDocument(BidRequestKey, Modulekey);
                    List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();

                    string[] doc = new string[Documentlist.Count];
                    var a = BidRequestKey;
                    for (int i = 0; i < Documentlist.Count; i++)
                    {

                        var Text = Convert.ToString(Documentlist[i].FileName);
                        string imagelist = Path.Combine(Server.MapPath("~/Document/Properties/")) + "BidRequestKeyTemp " + Documentlist[i].FileName;
                        string imagelists = Path.Combine(Server.MapPath("~/Document/Properties/")) + "BidRequestKeyTemp " + Documentlist[i].FileName;
                        if (Documentlist[i].FileName.Contains(BidRequestKey + " "))
                        {
                            imagelist = Path.Combine(Server.MapPath("~/Document/Properties/")) + Documentlist[i].FileName;
                            imagelists = Path.Combine(Server.MapPath("~/Document/Properties/")) + Documentlist[i].FileName;
                        }
                        //var path = Server.MapPath(imagelist);
                        string input = Documentlist[i].FileName;
                        string[] values = input.Split('.');
                        var checkext = values[1];
                        //if (checkext == "png" || checkext == "jpg")
                        {
                            string imagelist1 = Path.Combine(Server.MapPath("~/Document/Properties/")) + BidRequestKey + " " + Documentlist[i].FileName;
                            string imagelist2 = Path.Combine(Server.MapPath("~/Document/Properties/")) + WorkReportKey + " " + Documentlist[i].FileName;
                            if (Documentlist[i].FileName.Contains(BidRequestKey + " "))
                            {
                                imagelist1 = Path.Combine(Server.MapPath("~/Document/Properties/")) + Documentlist[i].FileName;
                                imagelist2 = Path.Combine(Server.MapPath("~/Document/Properties/")) + Documentlist[i].FileName;
                            }
                            System.IO.File.Copy(imagelist1, imagelist2);
                        }
                        //System.IO.File.Copy(imagelist, imagelists);
                    }

                    if (status == "Acceptp")
                    {
                        bidRequestModel.BidRequestKey = WorkReportKey;
                    }
                    else
                    {
                        bidRequestModel.BidRequestKey = BidRequestKey;
                    }
                }
                return Json(bidRequestModel.BidRequestKey, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult PMBidStatusUpdateCancel(string status, int BidRequestKey)
        {
            try
            {
                long ResourceKey = Convert.ToInt64(Session["resourceid"]);
                var moduleKey = Convert.ToInt32(Request.Form["ModuleKey"]);
                var vendors = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey, moduleKey, ResourceKey);
                Service.Base.Interface.IABNotificationService notificationService = new Service.Base.Code.ABNotificationService();
                if (status == "Submitted")
                {
                    bool value = false;
                    if (vendors.Count <= 0)
                        return Json(new { status = "vendor is empty" }, JsonRequestBehavior.AllowGet);
                }
                BidRequestModel bidRequestModel = new BidRequestModel();
                DataTable dt = new DataTable();
                int WorkReportKey = 0;


                WorkReportKey = _bidRequestservice.UpdateStatusDBReturbCancel(status, BidRequestKey);

                var bidreq = _bidRequestservice.GetDataBidRequestViewEdit(BidRequestKey);
                if (status == "Acceptp")
                {
                    bidRequestModel.BidRequestKey = WorkReportKey;
                }
                else
                {
                    bidRequestModel.BidRequestKey = BidRequestKey;
                }
                if (status == "CancelBid")
                {
                    IABNotificationService service = new ABNotificationService();
                    List<ABNotificationModel> notifications = service.GetABNotificationsAllByObjectAndModule(0, bidreq.ModuleKey, BidRequestKey);
                    var noti = notifications.FindAll(f => f.Status == "900");
                    foreach (var n in noti)
                    {
                        service.UpdateStatus(n.Id, "read");
                    }
                }
                return Json(bidRequestModel.BidRequestKey, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

       
        public JsonResult Notessave()
        {
            try
            {
                bool value = false;
                if (Request.Form.Count > 0)
                {
                    string notes = "";
                    string description = "";
                    int BidRequestKey = 0;
                    int Resourcekey = 0;
                    notes = Request.Form["txtnotetitle"].ToString();
                    description = Request.Form["txtnotedescription"].ToString();
                    BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"]);
                    Resourcekey = Convert.ToInt32(Session["resourceid"]);
                    value = _bidRequestservice.InsertNotes(notes, description, BidRequestKey, Resourcekey);
                }
                if (value == true)
                {
                    return Json("Sucess", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("failed", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToLower().ToString());
                return Json(null);
            }
        }

        public JsonResult SearchAllVendor()
        {
            try
            {
                int IsStaredVendor = 0;
                bool value = false;
                List<BidRequestModel> lstAllVendor = null;
                BidRequestModel bidRequestModel = new BidRequestModel();
                string SearchVendorName = Request.Form["SearchVendorName"].ToString();
                string SearchCompanyName = Request.Form["SearchCompanyName"].ToString();
                IsStaredVendor = Convert.ToInt32(Request.Form["IsStaredVendor"].ToString());
                int LastWorkedBefore = Convert.ToInt32(Request.Form["LastWorkedBefore"].ToString());
                int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());

                lstAllVendor = _bidRequestservice.SearchAllVendor(BidRequestKey, SearchVendorName, SearchCompanyName, IsStaredVendor, LastWorkedBefore);
                //lstAllVendor = _bidRequestservice.SearchAllVendor();
                //BidRequestModel staffDirectory = new BidRequestModel();
                //staffDirectory = _bidRequestservice.GetDataBidRequestViewEdit(BidRequestKey);
                //PropertyModel resources = null;
                //resources = __pMPropertiesService.GetBidDataForProperties(BidRequestKey);
                //foreach (var a in lstAllVendor)
                //{
                //    if (a.MinimumInsAmount < resources.MinimumInsuranceAmount)
                //    {
                //        lstAllVendor.Remove(a);
                //    }
                //}
                return Json(lstAllVendor);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult VendorAddForService()
        {
            bool value = false;
            try
            {

                List<BidRequestModel> lstVendor = null;
                if (Request.Form.Count > 0)
                {
                    string btval = "";
                    DateTime ResponseDueDate = new DateTime();
                    int BidRequestKey = 0;
                    int VendorKey = Convert.ToInt32(Request.Form["VendorKey"]);
                    int ServiceKey = Convert.ToInt32(Request.Form["ServiceKey"]);
                    BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"]);
                    string ResponseDate = Request.Form["ResponseDueDate"].ToString();

                    int Modulekey = Convert.ToInt32(Request.Form["Modulekey"]);

                    if (Request.Form["btval"].ToString() != "")
                    {
                        btval = Request.Form["btval"].ToString();
                    }


                    if (Request.Form["ResponseDueDate"].ToString() != "")
                    {


                        BidRequestModel bid = new BidRequestModel();
                        Int32 Comapanykey = Convert.ToInt32(Session["CompanyKey"]);
                        bid = _bidRequestservice.getbiddate(Comapanykey);
                        if (bid.BidRequestResponseDays != 0)
                        {

                            bid.ResponseDueDate = Common.Utility.AddBusinessDays(DateTime.Now, bid.BidRequestResponseDays);
                           


                        }

                        ResponseDueDate = bid.ResponseDueDate;
                    }
                    int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                    string BidVendorID = "A1";
                    value = _bidRequestservice.VendorAddForService(VendorKey, ServiceKey, BidRequestKey, ResponseDueDate, ResourceKey, BidVendorID, btval);
                    var b = _bidRequestservice.Get(BidRequestKey);
                    if (b.BidRequestStatus == 601 && ResourceKey > 0)
                    {
                        Service.Base.Interface.IABNotificationService notificationService = new Service.Base.Code.ABNotificationService();
                        Service.Base.Interface.IVendorManagerService managerService = new Service.Base.Code.VendorManagerService();
                        var res = managerService.GetResourceForInviteVendor(VendorKey);
                        long ForResourceKey = res.ResourceKey;
                        notificationService.InsertNotification("BidReqStatus", Modulekey, BidRequestKey, ResourceKey, "New Bid Request", ForResourceKey);
                    }
                    //return View();
                    lstVendor = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey, Modulekey, ResourceKey);
                }
                return Json(lstVendor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(value, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("PMBidRequestAdd", new { VendorKey });
            }
        }

        public JsonResult DeleteBidVendorByBidVendorKey(int BidVendorKey)
        {
            try
            {


               
                var BidVendor = _bidRequestservice.GetBidVendorByBidVendorKey(BidVendorKey);
                if (BidVendor != null && BidVendor.IsAssigned == true)
                {
                    bool status = _bidRequestservice.DeleteBidVendorByBidVendorKey(BidVendorKey);
                    if (status)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InsertRatingNew(string Message, int Rating1, int BidrequestKey, string status)
        {

            try
            {

                int ResourceKey = Convert.ToInt32(Session["resourceid"]);

                bool value = false;
                var st = false;
                if (status == "Closed")
                    st = _bidRequestservice.BidRequestStatusUpdate(BidrequestKey, status);

                value = _bidRequestservice.InsertRatingNew(Message, Rating1, ResourceKey, BidrequestKey);

                IABNotificationService service = new ABNotificationService();
                List<ABNotificationModel> notifications = service.GetABNotificationsAll(ResourceKey);
                var noti = notifications.FindAll(f => (f.ModuleKey == 100 || f.ModuleKey == 106) && f.Status == "900" && f.ObjectKey == BidrequestKey);
                foreach (var n in noti)
                {
                    service.UpdateStatus(n.Id, "read");
                }
                if (st || value)
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }



        }

        public JsonResult GetDataView()
        {
            bool value = false;
            try
            {
                BidRequestModel VendorView = null;
                if (Request.Form.Count > 0)
                {
                    //int BidRequestKey = 0;
                    int VendorKey = Convert.ToInt32(Request.Form["VendorKey"].ToString());

                    VendorView = _bidRequestservice.GetDataViewEdit(VendorKey);
                }
                return Json(VendorView, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(value, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("PMBidRequestAdd", new { VendorKey });
            }
        }

        public JsonResult IndexinsurancePaging()
        {
            try
            {
                int VendorKey = Convert.ToInt32(Request.Form["VendorKey"].ToString());
                IList<BidRequestModel> itemList = null;
                itemList = _bidRequestservice.Searchinsurance(VendorKey);
                return Json(itemList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult bindservice()
        {
            try
            {
                int VendorKey = Convert.ToInt32(Request.Form["VendorKey"].ToString());
                IList<BidRequestModel> servicelist = null;
                servicelist = _bidRequestservice.Getbindservice(VendorKey);
                return Json(servicelist, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult bindDocument()
        {
            try
            {
                IList<BidRequestModel> Documentlist = null;
                int VendorKey = Convert.ToInt32(Request.Form["VendorKey"].ToString());
                int ModuleKey = Convert.ToInt32(Request.Form["ModuleKey"]);
                Documentlist = _bidRequestservice.GetbindDocument(VendorKey, ModuleKey);
                return Json(Documentlist, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult Noteslist(int BidRequestKey)
        {
            try
            {
                IList<BidRequestModel> Noteslist = null;
                Noteslist = _bidRequestservice.GetbindBidRequestNotes(BidRequestKey);
                return Json(Noteslist, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult NotesRemove(int Noteid)
        {
            try
            {
                bool val = false;
                val = _bidRequestservice.NotesRemove(Noteid);
                return Json(val, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        // GET: Property
        public ActionResult PMBidRequestView(int BidRequestKey)
        {
            List<BidRequestModel> lstVendor = null;
            BidRequestModel staffDirectory = new BidRequestModel();
            int resourcekey = Convert.ToInt32(Session["resourceid"]);

            staffDirectory = _bidRequestservice.GetDataBidRequestViewEdit(BidRequestKey);
            lstVendor = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey, 100, resourcekey);
            staffDirectory.ResourceKey = resourcekey;
            if (staffDirectory.BidDueDates != null && staffDirectory.BidDueDates != "")
            {
                if (Convert.ToDateTime(staffDirectory.BidDueDates) < Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")))
                    staffDirectory.isExpired = true;
                else
                    staffDirectory.isExpired = false;
            }
            else
                staffDirectory.isExpired = true;
            int Modulekey = 100;
            IList<BidRequestModel> Documentlist = null;
            Documentlist = _bidRequestservice.GetbindDocument(BidRequestKey, Modulekey);
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            string[] doc = new string[Documentlist.Count];
            var a = BidRequestKey;
            for (int i = 0; i < Documentlist.Count; i++)
            {

                var Text = Convert.ToString(Documentlist[i].FileName);
                string imagelist = "~/Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
                if (Documentlist[i].FileName.Contains(BidRequestKey + " "))
                    imagelist = "~/Document/Properties/" + Documentlist[i].FileName;
                var path = Server.MapPath(imagelist);
                string input = Documentlist[i].FileName;
                string[] values = input.Split('.');
                var checkext = values[1];
                if (checkext == "png" || checkext == "jpg")
                {
                    staffDirectory.checkimg = true;
                    string imagelist1 = "~/Document/Properties/" + BidRequestKey + " " + Documentlist[i].FileName;
                    if (Documentlist[i].FileName.Contains(BidRequestKey + " "))
                        imagelist1 = "~/Document/Properties/" + Documentlist[i].FileName;
                    doc[i] = imagelist1;
                }

                else
                {
                    staffDirectory.checkimg = false;
                }
            }
            staffDirectory.img = doc;




          
           
    

            return View(staffDirectory);
        }

        public JsonResult SearchVendorByBidRequest()
        {
            List<BidRequestModel> lstVendor = null;
            //long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());
            lstVendor = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey, modulekey);
            //BidRequestModel staffDirectory = new BidRequestModel();
            //staffDirectory = _bidRequestservice.GetDataBidRequestViewEdit(BidRequestKey);
            //PropertyModel resources = null;
            //resources = __pMPropertiesService.GetBidDataForProperties(BidRequestKey);
            //foreach (var a in lstVendor)
            //{
            //    if (a.MinimumInsAmount < resources.MinimumInsuranceAmount)
            //    {
            //        lstVendor.Remove(a);
            //    }
            //}
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateDocUpload()
        {
            try
            {
                bool value = false;
                List<BidRequestModel> lstVendor = null;
                BidRequestModel bidRequestModel = new BidRequestModel();
                if (Request.Form.Count > 0)
                {
                    int BidRequestKey = 0;
                    StringBuilder FileName = new StringBuilder();
                    StringBuilder FileSize = new StringBuilder();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        FileName.Append(Path.GetFileName(file.FileName));
                        FileName.Append(",");
                        FileSize.Append(file.ContentLength);
                        FileSize.Append(",");
                    }
                    if (Request.Files.Count != 0)
                    {
                        FileName.Remove(FileName.Length - 1, 1);
                        FileSize.Remove(FileSize.Length - 1, 1);
                    }

                    BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"]);
                    int modulekey = Convert.ToInt32(Request.Form["Modulekey"]);
                    value = _bidRequestservice.UpdateDocInsert(BidRequestKey, FileName.ToString(), FileSize.ToString(), modulekey);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Document/Properties/"), BidRequestKey + " " + fileName);
                        file.SaveAs(path);
                    }

                    bidRequestModel.BidRequestKey = BidRequestKey;
                    
                    lstVendor = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey, modulekey, Convert.ToInt64(Session["resourceid"]));
                }
                return Json(bidRequestModel.BidRequestKey, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        public ActionResult DocumentDelete(int BidRequestKey, string Docname)
        {
            try
            {
                string Directory = Server.MapPath("~/Document/Properties/");
                string fileName = Docname;
                string path = Directory + BidRequestKey + " " + Docname;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                bool value = false;
                value = _bidRequestservice.DocumentDelete(BidRequestKey, Docname);
                return RedirectToAction("PMBidRequestView", new { BidRequestKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("PMBidRequestView", new { BidRequestKey });
            }
        }

        public JsonResult bindBidRequestDocument(int BidRequestKey, int Modulekey)
        {
            try
            {
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                IList<BidRequestModel> Documentlist = null;
                Documentlist = _bidRequestservice.GetbindDocument(BidRequestKey, Modulekey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                string[] doc = new string[Documentlist.Count];
                for (int i = 0; i < Documentlist.Count; i++)
                {
                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "../Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
                    if (Documentlist[i].FileName.Contains(BidRequestKey + " "))
                        imagelist = "../Document/Properties/" + Documentlist[i].FileName;
                    var path = Server.MapPath(imagelist);

                    string input = Documentlist[i].FileName;
                    string[] values = input.Split('.');
                    var checkext = values[1];

                    doc[i] = imagelist;

                }
                return Json(doc, JsonRequestBehavior.AllowGet);


            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult bindvendorinformation(int BidVendorKey1)
        {
            try
            {
                BidRequestModel bidmodel = null;
                List<BidVendorModel> lstVendor = null;
                bidmodel = _bidRequestservice.bindvendorinformation(BidVendorKey1);

                BidVendorModel vendorModel = new BidVendorModel();
                vendorModel = _bidRequestservice.GetBidVendorByBidVendorKey(BidVendorKey1);
                int Modulekey = 100;
                IList<BidRequestModel> Documentlist = null;
                Documentlist = _bidRequestservice.GetbindDocument(BidVendorKey1, Modulekey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                //int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                string[] doc = new string[Documentlist.Count];
                var a = BidVendorKey1;
                for (int i = 0; i < Documentlist.Count; i++)
                {

                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "~/Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
                    if (Documentlist[i].FileName.Contains(BidVendorKey1 + " "))
                        imagelist = "~/Document/Properties/" + Documentlist[i].FileName;
                    var path = Server.MapPath(imagelist);
                    string input = Documentlist[i].FileName;
                    string[] values = input.Split('.');
                    var checkext = values[1];
                    if (checkext == "png" || checkext == "jpg")
                    {
                        vendorModel.checkimg = true;
                        string imagelist1 = "~/Document/Properties/" + BidVendorKey1 + " " + Documentlist[i].FileName;
                        if (Documentlist[i].FileName.Contains(BidVendorKey1 + " "))
                            imagelist1 = "~/Document/Properties/" + Documentlist[i].FileName;
                        doc[i] = imagelist1;
                    }

                    else
                    {
                        vendorModel.checkimg = false;
                    }
                }
                bidmodel.img = doc;
                return Json(bidmodel, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }

        public ActionResult DownloadFile(int DocumentKey, int CompanyKey, int InsuranceKey,int bidRequestKey)
        {
            var referal = HttpContext.Request.UrlReferrer.AbsolutePath.Split('/');
            string refView = referal[referal.Length - 1];
            try
            {
                var doc = __vendorManagerservice.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                string ff = doc.Where(w => w.Document.DocumentKey == DocumentKey).FirstOrDefault().Document.FileName;

                string filename = Server.MapPath("~/Document/Insurance/" + CompanyKey + "/" + InsuranceKey + "/" + ff);
                string fname = Server.MapPath("~/Document/Insurance/"+InsuranceKey+ff);

                string fpath = Server.MapPath("~/Document/Insurance/" + InsuranceKey+ff);

                string path = Server.MapPath("~/Document/Insurance/" + CompanyKey + "/" + InsuranceKey + "/" + ff);
                if (System.IO.File.Exists(filename))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                    if (fileBytes.Length > 0)
                    {
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ff);
                    }

                   
                }
               else if (System.IO.File.Exists(fname))
                {

                    byte[] fileBytes = System.IO.File.ReadAllBytes(fpath);
                    if (fileBytes.Length > 0)
                    {
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ff);
                    }
                }


                TempData["ErrorMessage"] = "File not found";
                System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();           
                return RedirectToAction("PMBidRequestView", new { bidRequestKey });
            }
            catch(Exception ex)
            {


                return RedirectToAction("PMBidRequestView", new { bidRequestKey });
            }


        }


        public ActionResult PMBidRequestEdit(int BidRequestKey)
        {
            IList<BidRequestModel> lstProperty = null;
            IList<BidRequestModel> lstService = null;
            //list of Property
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            Int32 companyKey = Convert.ToInt32(Session["CompanyKey"]);
            lstProperty = _bidRequestservice.GetAllProperty(resourcekey, companyKey);
            List<System.Web.Mvc.SelectListItem> lstPropertylist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lstPropertylist.Add(sli2);
            for (int i = 0; i < lstProperty.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstProperty[i].Property);
                sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                lstPropertylist.Add(sli);
            }
            ViewBag.lstProperty = lstPropertylist;

            //list of Service
            lstService = _bidRequestservice.GetAllService();
            List<System.Web.Mvc.SelectListItem> lstServicelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
            sli1.Text = "--Please Select--";
            sli1.Value = "0";
            lstServicelist.Add(sli1);
            for (int i = 0; i < lstService.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstService[i].Property);
                sli.Value = Convert.ToString(lstService[i].PropertyKey);
                lstServicelist.Add(sli);
            }
            ViewBag.lstService = lstServicelist;
            BidRequestModel staffDirectory = new BidRequestModel();
            staffDirectory = _bidRequestservice.GetDataBidRequestViewEdit(BidRequestKey);
            //lstVendor = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey);

         

            return View(staffDirectory);
        }

        [HttpPost]
        public ActionResult PMBidRequestEdit(BidRequestModel collection)
        {
            VendorModel vendorv = new VendorModel();
            try
            {
                collection.BidDueDate= Convert.ToDateTime(collection.BidDueDates);
                collection.ResponseDueDate= Convert.ToDateTime(collection.ResponseDueDates);
                bool value = false;
                value = _bidRequestservice.Update(collection);

                if (collection.ModuleKey == 106)
                {
                    if (value == true)
                    {
                        TempData["SuccessMessage"] = "Record has been updated successfully.";

                        return RedirectToAction("PMWorkOrdersView", "PMWorkOrders", new { collection.BidRequestKey });
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Error";

                        return RedirectToAction("PMWorkOrdersView", "PMWorkOrders", new { collection.BidRequestKey });

                    }
                }

                if (value == true)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";

                    return RedirectToAction("PMBidRequestView", new { collection.BidRequestKey });
                }
                else
                {
                    ViewData["ErrorMessage"] = "Error";

                    return RedirectToAction("PMBidRequestView", new { collection.BidRequestKey });

                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("PMBidRequestView", new { collection.BidRequestKey });

            }

        }
        public ActionResult Delete(int BidRequestKey)
        {

            bool Status = false;
            Status = _bidRequestservice.Delete(BidRequestKey);
            if (Status == true)
            {
                TempData["Sucessmessage"] = "Record has been deleted successfully.";
                return RedirectToAction("PMBidRequestlist", "PMBidRequests");
            }
            else
            {
                ViewData["Errormessage"] = "Error";
                return RedirectToAction("PMBidRequestlist", "PMBidRequests");
            }

        }

        public JsonResult ReloadBidPage(int BidRequestKey)
        {

            bool Status = false;
            Status = _bidRequestservice.Delete(BidRequestKey);
            return Json(Status, JsonRequestBehavior.AllowGet);

        }
        public JsonResult UpdateResponseDueDate(string ResponseDueDate, int BidRequestKey, int BidVendorKey)
        {
            var status = _bidRequestservice.UpdateResponseDueDate(ResponseDueDate, BidRequestKey, BidVendorKey);
            if (status)
            {
                return Json(true, JsonRequestBehavior.AllowGet);    
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
