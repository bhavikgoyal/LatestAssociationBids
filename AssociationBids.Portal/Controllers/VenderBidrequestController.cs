using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Service.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class VenderBidrequestController : Controller
    {
        private IABNotificationService __notification;
        private readonly AssociationBids.Portal.Service.Base.IBidRequestService _bidRequestservice;
        private readonly IBidRequestService _venderbidrequestservice;
        private IVendorPolicyService _vendorPolicy;
        private readonly AssociationBids.Portal.Service.Base.IAStaffDirectoryService _staffDirectoryservice;
        public VenderBidrequestController(IABNotificationService notificationService, IBidRequestService registrationService, AssociationBids.Portal.Service.Base.IAStaffDirectoryService staffDirectoryService, IVendorPolicyService policyService, AssociationBids.Portal.Service.Base.IBidRequestService bidRequestService)
        {
            this._staffDirectoryservice = staffDirectoryService;
            this._bidRequestservice = bidRequestService;
            this._venderbidrequestservice = registrationService;
            _vendorPolicy = policyService;
            __notification = notificationService;
        }
        // GET: Bidrequest
        public ActionResult Index()
        {   
            var c = Session["companykey"];
            int CompanyKey = Convert.ToInt32(c);
            List<InsuranceModel> itemList = _vendorPolicy.GetInsurancePaging(CompanyKey, 10, 1, "", "order by PolicyNumber asc");
            if (itemList.Count == 0)
            {
                ViewBag.inskey = 0;
            }
            else
            {
                ViewBag.inskey = itemList.Count;
            }
            return View();
        }
        public ActionResult BidRequests(int BidReuestKey = 0)
        {

            if (BidReuestKey != 0)
            {
                int ForResource = Convert.ToInt32(Session["resourceid"]);
                bool status = __notification.UpdateStatsVendordash(ForResource, "read", BidReuestKey);
            }

            ViewBag.StatusListForDDL = this._venderbidrequestservice.GetStatusListForDDL("", "");
            ViewBag.userid = Convert.ToInt64(Session["userid"]);
            return View();
        }

        public ActionResult ViewBidrequest(int BidVendorKey)
        {
            ViewBag.BidVendorKey = BidVendorKey;
            BidVendorModel vendorModel = new BidVendorModel();
            vendorModel = _venderbidrequestservice.GetBidVendorByBidVendorKey(BidVendorKey);
            int Modulekey = 100;
            int BidRequestKey = vendorModel.BidRequestKey;
            List<BidRequestModel> lstVendor = null;
            BidRequestModel staffDirectory = new BidRequestModel();

            staffDirectory = _venderbidrequestservice.GetDataBidRequestViewEdit(BidRequestKey);
            lstVendor = _venderbidrequestservice.SearchVendorByBidRequest(BidRequestKey, 100, Convert.ToInt32(Session["resourceid"]));
            int resourcekey = Convert.ToInt32(Session["resourceid"]);
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
            Modulekey = 100;
            IList<BidRequestModel> Documentlist = null;
            Documentlist = _venderbidrequestservice.GetbindDocument(BidRequestKey, Modulekey);
            List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
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
            //staffDirectory.img = doc;
            ViewBag.img = doc;
            ViewBag.bidreqky = vendorModel.BidRequestKey;
            //Session["img"] = staffDirectory;
            return View(vendorModel);
            //return View();
        }


        public JsonResult bindBidRequestDocument(int BidRequestKey, int Modulekey)
        {
            try
            {
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                //BidVendorModel vendorModel = new BidVendorModel();
                //vendorModel = _venderbidrequestservice.GetBidVendorByBidVendorKey(BidVendorKey);
                //BidRequestKey = vendorModel.BidRequestKey;
                IList<BidRequestModel> Documentlist = null;
                Documentlist = _venderbidrequestservice.GetbindDocument(BidRequestKey, Modulekey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
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
                        string imagelist1 = "~/Document/Properties/" + BidRequestKey + " " + Documentlist[i].FileName;
                        if (Documentlist[i].FileName.Contains(BidRequestKey + " "))
                            imagelist1 = "~/Document/Properties/" + Documentlist[i].FileName;
                        doc[i] = imagelist1;
                    }
                }
                return Json(doc, JsonRequestBehavior.AllowGet);


            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult bindVendorRequestDocument(int BidVendorKey)
        {
            try
            {
                //int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                //IList<BidRequestModel> Documentlist = null;
                //Documentlist = _bidRequestservice.GetbindDocument(BidRequestKey, Modulekey);
                //List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                //string[] doc = new string[Documentlist.Count];
                //for (int i = 0; i < Documentlist.Count; i++)
                //{
                //    var Text = Convert.ToString(Documentlist[i].FileName);
                //    string imagelist = "../Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
                //    if (Documentlist[i].FileName.Contains(BidRequestKey + " "))
                //        imagelist = "../Document/Properties/" + Documentlist[i].FileName;
                //    var path = Server.MapPath(imagelist);

                //    string input = Documentlist[i].FileName;
                //    string[] values = input.Split('.');
                //    var checkext = values[1];

                //    doc[i] = imagelist;

                //}
                //return Json(doc, JsonRequestBehavior.AllowGet);
                List<BidVendorModel> lstVendor = null;
                BidVendorModel staffDirectory = new BidVendorModel();
                staffDirectory = _venderbidrequestservice.GetBidVendorByBidVendorKey(BidVendorKey); int Modulekey = 100;
                IList<BidRequestModel> Documentlist = null;
                Documentlist = _venderbidrequestservice.GetbindDocument(BidVendorKey, Modulekey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                string[] doc = new string[Documentlist.Count];
                var a = BidVendorKey;
                for (int i = 0; i < Documentlist.Count; i++)
                {

                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "~/Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
                    if (Documentlist[i].FileName.Contains(BidVendorKey + " "))
                        imagelist = "~/Document/Properties/" + Documentlist[i].FileName;
                    var path = Server.MapPath(imagelist);
                    string input = Documentlist[i].FileName;
                    string[] values = input.Split('.');
                    var checkext = values[1];
                    if (checkext == "png" || checkext == "jpg")
                    {
                        staffDirectory.checkimg = true;
                        string imagelist1 = "~/Document/Properties/" + BidVendorKey + " " + Documentlist[i].FileName;
                        if (Documentlist[i].FileName.Contains(BidVendorKey + " "))
                            imagelist1 = "~/Document/Properties/" + Documentlist[i].FileName;
                        doc[i] = imagelist1;
                    }

                    else
                    {
                        staffDirectory.checkimg = false;
                    }
                }
                return Json(doc, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        public ActionResult ViewBidrequestMore(Int64 BidVendorKey, Int64 ResourceKey, string BidStartDate)
        {
            ViewBag.BidVendorKey = BidVendorKey;
            ViewBag.ResourceKey = ResourceKey;
            ViewBag.BidStartDate = BidStartDate;

            return View();
        }
        public ActionResult ViewBidrequestMoreClone(int BidVendorKey, int ResourceKey, string BidStartDate)
        {
            ViewBag.BidVendorKey = BidVendorKey;
            ViewBag.ResourceKey = ResourceKey;
            ViewBag.BidStartDate = BidStartDate;
            List<BidVendorModel> lstVendor = null;
            BidVendorModel staffDirectory = new BidVendorModel();
            staffDirectory = _venderbidrequestservice.GetBidVendorByBidVendorKey(BidVendorKey);
            int Modulekey = 100;
            IList<BidRequestModel> Documentlist = null;
            Documentlist = _venderbidrequestservice.GetbindDocument(BidVendorKey, Modulekey);
            List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
            //int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            string[] doc = new string[Documentlist.Count];
            var a = BidVendorKey;
            for (int i = 0; i < Documentlist.Count; i++)
            {

                var Text = Convert.ToString(Documentlist[i].FileName);
                string imagelist = "~/Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
                if (Documentlist[i].FileName.Contains(BidVendorKey + " "))
                    imagelist = "~/Document/Properties/" + Documentlist[i].FileName;
                var path = Server.MapPath(imagelist);
                string input = Documentlist[i].FileName;
                string[] values = input.Split('.');
                var checkext = values[1];
                if (checkext == "png" || checkext == "jpg")
                {
                    staffDirectory.checkimg = true;
                    string imagelist1 = "~/Document/Properties/" + BidVendorKey + " " + Documentlist[i].FileName;
                    if (Documentlist[i].FileName.Contains(BidVendorKey + " "))
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
        public JsonResult GetVenderBidRequestListJson(Int32 PageSize, Int32 PageIndex, string Sort, string Search, Int64 BidVendorKey, string BidRequestStatus, string BiddueDateFrom, string BiddueDateTo, string ModuleController)
        {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            //IList<BidRequestModel> bidRequestModel   =  this._venderbidrequestservice.SearchBidRequestVender(PageSize,  PageIndex,    Search, Sort, BidRequestKey);
            var bidRequestModel = this._venderbidrequestservice.SearchBidRequestVenderjson(PageSize, PageIndex, Search, Sort, BidVendorKey, Convert.ToInt64(Session["CompanyKey"]), Convert.ToInt64(Session["userid"]), BidRequestStatus, BiddueDateFrom, BiddueDateTo, ModuleController, ResourceKey);
            var newbidstring = bidRequestModel.Replace(",\"NewMsg\"", ",\"priorityrecord\":\"false\",\"NewMsg\"");
            return Json(newbidstring, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVenderBidRequestListJsonPriority(Int32 PageSize, Int32 PageIndex, string Sort, string Search, Int64 BidVendorKey, string BidRequestStatus, string BiddueDateFrom, string BiddueDateTo, string ModuleController)
         {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            //IList<BidRequestModel> bidRequestModel   =  this._venderbidrequestservice.SearchBidRequestVender(PageSize,  PageIndex,    Search, Sort, BidRequestKey);
            var bidRequestModel = this._venderbidrequestservice.SearchBidRequestVenderjsonPriority(PageSize, PageIndex, Search, Sort, BidVendorKey, Convert.ToInt64(Session["CompanyKey"]), Convert.ToInt64(Session["userid"]), BidRequestStatus, BiddueDateFrom, BiddueDateTo, ModuleController, ResourceKey);
            var newbidstring = bidRequestModel.Replace(",\"NewMsg\"", ",\"priorityrecord\":\"true\",\"NewMsg\"");
            return Json(newbidstring, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVenderBidRequestDocumentListJson(Int32 PageSize, Int32 PageIndex, string Sort, string Search, Int64 BidVendorKey, string TableName)
        {
            var bidRequestModel = this._venderbidrequestservice.SearchBidRequestVenderDocjson(PageSize, PageIndex, Search, Sort, BidVendorKey, TableName);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApceptRejectVenderBidrequest(Int64 BidVendorKey, string Status, string IsAssigned)
        {
            var bidRequestModel = this._venderbidrequestservice.ApceptRejectVenderBidrequest(BidVendorKey, Status, IsAssigned);
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            IABNotificationService notificationService = new ABNotificationService();
            IBidVendorService bid = new BidVendorService();
            IBidRequestService bidRequest = new BidRequestService();
            var vendor = bid.Get(Convert.ToInt32(BidVendorKey));
            var bidreq = bidRequest.GetDataBidRequestViewEdit(vendor.BidRequestKey);
            notificationService.InsertNotification("BidVendorStatus", bidreq.ModuleKey, vendor.BidRequestKey, ResourceKey, "Bid Vendor Status " + Status);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title)
        {
            var bidRequestModel = this._venderbidrequestservice.SendInsertMessage(Body, Status, ObjectKey, ResourceKey, ModuleKeyName, Title);
            long ByResourceKey = Convert.ToInt64(Session["resourceid"]);
            int portalKey = Convert.ToInt32(Session["portalkey"]);
            int modulekey = 106;
            if (ModuleKeyName == "BidRequest")
                modulekey = 100;
            IABNotificationService notificationService = new ABNotificationService();
            if (portalKey == 3)
            {
                IBidVendorService bid = new BidVendorService();
                var vendor = bid.Get(Convert.ToInt32(ObjectKey));
                notificationService.InsertNotification("BidReqMsg", modulekey, vendor.BidRequestKey, ByResourceKey, "New Message For Bid");
            }
            else if (portalKey == 2)
            {
                notificationService.InsertNotification("BidReqMsg", modulekey, ObjectKey, ByResourceKey, "New Message For Bid");
            }
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus)
        {
            var bidRequestModel = this._venderbidrequestservice.SendInsertMessageList(ObjectKey, ResourceKey, ModuleKeyName, UpdatemsgStatus);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DocumentUploadDynmc()
        {
            string ObjectKey = "";
            try
            {
                bool value = false;
                if (Request.Form.Count > 0)
                {
                    ObjectKey = Request.Form["ObjectKey"].ToString();
                    string Title = Request.Form["FileName"].ToString();
                    string Description = Request.Form["FileSize"].ToString();
                    string ControllerName = Request.Form["ControllerName"].ToString();
                    string ActionName = Request.Form["ActionName"].ToString();

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
                    Int64 DocumentKey = _venderbidrequestservice.DocumentInsertDynamic(ObjectKey, FileName.ToString(), FileSize.ToString(), ControllerName, ActionName);


                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Document/Properties/"), DocumentKey + " " + fileName);
                        file.SaveAs(path);
                        DocumentKey++;
                    }
                }
                return Json(ObjectKey, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        public JsonResult GetDocumentListDynmc(Int64 ObjectKey, string ControllerName, string ActionName)
        {
            var bidRequestModel = this._venderbidrequestservice.GetDocumentListDynmc(ObjectKey, ControllerName, ActionName);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteDocumentListDynmc(Int64 DocumentKey)
        {
            var bidRequestModel = this._venderbidrequestservice.DeleteDocumentListDynmc(DocumentKey);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubmitVenderBidJson(Int64 BidVendorKey, string Status, Int64 ResourceKey, string Title, string Total, string Description, string LastModificationTime)
        {
            var bidRequestModel = this._venderbidrequestservice.SubmitVenderBid(BidVendorKey, Status, ResourceKey, Title, Total, Description, LastModificationTime);
            if (Status != "getid")
            {
                long ResourceKey1 = Convert.ToInt64(Session["resourceid"]);
                IABNotificationService notificationService = new ABNotificationService();
                IBidVendorService bid = new BidVendorService();
                IBidRequestService bidRequest = new BidRequestService();
                var vendor = bid.Get(Convert.ToInt32(BidVendorKey));
                var bidreq = bidRequest.GetDataBidRequestViewEdit(vendor.BidRequestKey);
                notificationService.InsertNotification("BidVendorStatus", bidreq.ModuleKey, vendor.BidRequestKey, ResourceKey1, "Bid Vendor Status " + Status);
            }
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVenderBidListJson(Int64 BidVendorKey, Int64 ResourceKey)
        {
            var bidRequestModel = this._venderbidrequestservice.GetVenderBidList(BidVendorKey, ResourceKey);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName)
        {
            var bidRequestModel = this._venderbidrequestservice.MessageNewCount(0, 0, ModuleKeyName, Convert.ToInt64(Session["userid"]));
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadBidStatus()
        {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            var bidStatusModel = _venderbidrequestservice.LoadBidStatus(ResourceKey);
            return Json(bidStatusModel, JsonRequestBehavior.AllowGet);
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
                value = _venderbidrequestservice.DocumentDelete(BidRequestKey, Docname);
                return RedirectToAction("ViewBidrequest", new { BidVendorKey = BidRequestKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewBidrequest", new { BidVendorKey = BidRequestKey });
            }
        }

        public JsonResult DateExtension(string BidName, string ManagerName, string ManagerCompanyName, string ManagerEmail,
            string Body, string Status, int ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title, int BidRequestKey, int BidVendorKey

            )
        {
            long ResourceKey1 = Convert.ToInt64(Session["resourceid"]);
            string VendorName = Session["username"].ToString();
            IABNotificationService notificationService = new ABNotificationService();

            string bidNotificationModel = notificationService.RequestSendOrNot(ResourceKey1, BidName);
            bool bidStatusModel = false;
            if (bidNotificationModel == "True")
            {
                bidStatusModel = _venderbidrequestservice.DateExtensionRequest(BidName, ManagerName, ManagerCompanyName, ManagerEmail, VendorName);
            }
            if (bidStatusModel == true)
            {
                int ByResourceKey = Convert.ToInt32(Session["resourceid"]);
                BidRequestModel staffDirectory = new BidRequestModel();
                staffDirectory = _bidRequestservice.GetDataBidRequestViewEdit(BidRequestKey);
                BidRequestModel bid1 = new BidRequestModel();
                Int32 Comapanykey = Convert.ToInt32(Session["CompanyKey"]);
                bid1 = _bidRequestservice.getbiddate(staffDirectory.CompanyKey);
                List<AStaffDirectoryModel> AstaffDirectoryList = null;
                AStaffDirectoryModel AstaffDirectory = new AStaffDirectoryModel();
                AstaffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ByResourceKey);
                Body = Body + " " + AstaffDirectoryList[0].FirstName + AstaffDirectoryList[0].LastName;
                int portalKey = Convert.ToInt32(Session["portalkey"]);
                int modulekey = 106;
                if (ModuleKeyName == "BidRequest")
                    modulekey = 100;

                IABNotificationService notificationService1 = new ABNotificationService();

                if (portalKey == 3)
                {
                    IBidVendorService bid = new BidVendorService();
                    var vendor = bid.Get(Convert.ToInt32(ObjectKey));
                    notificationService1.InsertNotification("BidReqDate", modulekey, vendor.BidRequestKey, ByResourceKey, Body, BidVendorKey);
                }
                else if (portalKey == 2)
                {
                    notificationService1.InsertNotification("BidReqDate", modulekey, ObjectKey, ByResourceKey, Body, BidVendorKey);
                }


            }



            return Json(bidStatusModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendInsertMessageNoti(string Body, string Status, int ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title, DateTime dateadded, int BidRequestKey,int BidVendorKey)
        {
            //var bidRequestModel = this._venderbidrequestservice.SendInsertMessage(Body, Status, ObjectKey, ResourceKey, ModuleKeyName, Title);
            int ByResourceKey = Convert.ToInt32(Session["resourceid"]);
            BidRequestModel staffDirectory = new BidRequestModel();
            staffDirectory = _bidRequestservice.GetDataBidRequestViewEdit(BidRequestKey);
            BidRequestModel bid1 = new BidRequestModel();
            Int32 Comapanykey = Convert.ToInt32(Session["CompanyKey"]);
            bid1 = _bidRequestservice.getbiddate(staffDirectory.CompanyKey);
            List<AStaffDirectoryModel> AstaffDirectoryList = null;
            AStaffDirectoryModel AstaffDirectory = new AStaffDirectoryModel();
            AstaffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ByResourceKey);
            Body = Body +" " + AstaffDirectoryList[0].FirstName + AstaffDirectoryList[0].LastName;
            int portalKey = Convert.ToInt32(Session["portalkey"]);
            int modulekey = 106;
            if (ModuleKeyName == "BidRequest")
                modulekey = 100;

            IABNotificationService notificationService = new ABNotificationService();

            if (portalKey == 3)
            {
                IBidVendorService bid = new BidVendorService();
                var vendor = bid.Get(Convert.ToInt32(ObjectKey));
                notificationService.InsertNotification("BidReqDate", modulekey, vendor.BidRequestKey, ByResourceKey, Body, BidVendorKey);
            }
            else if (portalKey == 2)
            {
                notificationService.InsertNotification("BidReqDate", modulekey, ObjectKey, ByResourceKey, Body, BidVendorKey);
            }
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}