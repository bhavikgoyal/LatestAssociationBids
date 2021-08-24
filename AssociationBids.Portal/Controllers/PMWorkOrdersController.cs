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
    public class PMWorkOrdersController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IBidRequestService _bidRequestservice;
        private readonly AssociationBids.Portal.Service.Base.Interface.IVendorManagerService __vendorManagerservice;

        public PMWorkOrdersController(AssociationBids.Portal.Service.Base.IBidRequestService bidRequestService, IVendorManagerService vendorManagerService)
        {
            this.__vendorManagerservice = vendorManagerService;
            this._bidRequestservice = bidRequestService;
        }
        // GET: PMBidRequests
        public ActionResult PMWorkOrdersList()
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
            lstBidReqStatuslist.Add(new SelectListItem() { Text = "--All--", Value = "0,0" });
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

        public JsonResult InsertRating(string Message, int Rating1, int Rating2, int Rating3, int BidrequestKey)
        {



            try
            {

                int ResourceKey = Convert.ToInt32(Session["resourceid"]);

                bool value = false;
                value = _bidRequestservice.InsertRating(Message, Rating1, Rating2, Rating3, ResourceKey, BidrequestKey);

                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
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
                if (st || value)
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult PMWorkOrdersAdd(string propertykey = "0")
        {
            BidRequestModel bid = new BidRequestModel();
            Session.Remove("BidRequestAddDocSession");
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

            Int32 Comapanykey = Convert.ToInt32(Session["CompanyKey"]);
            bid = _bidRequestservice.getbiddate(Comapanykey);

            if (bid.BidRequestResponseDays != 0)
            {

                bid.ResponseDueDate = Common.Utility.AddBusinessDays(DateTime.Now, bid.BidRequestResponseDays);

            }
            if (bid.BidSubmitDays != 0)
            {

                bid.BidDueDate = Common.Utility.AddBusinessDays(DateTime.Now, bid.BidSubmitDays);
            }
            bid.PropertyKey = propertykey;

            if (bid.PropertyKey == "0")
            {
                bid.PropertyKey = "no";
            }
            return View(bid);
        }
        // GET: Property
        public ActionResult PMWorkOrdersView(int BidRequestKey)
        {
            List<BidRequestModel> lstVendor = null;
            BidRequestModel staffDirectory = new BidRequestModel();

            staffDirectory = _bidRequestservice.GetDataBidRequestViewEdit(BidRequestKey);
            lstVendor = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey, 106, Convert.ToInt32(Session["resourceid"]));
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            staffDirectory.ResourceKey = resourcekey;
            if (Convert.ToDateTime(staffDirectory.BidDueDate.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")))
            {
                staffDirectory.isExpired = true;
            }
            else
            {
                staffDirectory.isExpired = false;
            }
            IList<BidRequestModel> Documentlist = null;
            Documentlist = _bidRequestservice.GetbindDocument(BidRequestKey, 106);
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
                    string imagelist1 = "../Document/Properties/" + BidRequestKey + " " + Documentlist[i].FileName;
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
        public ActionResult DownloadFile(int DocumentKey, int CompanyKey, int InsuranceKey, int bidRequestKey)
        {
            var referal = HttpContext.Request.UrlReferrer.AbsolutePath.Split('/');
            string refView = referal[referal.Length - 1];
            try
            {
                var doc = __vendorManagerservice.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                string ff = doc.Where(w => w.Document.DocumentKey == DocumentKey).FirstOrDefault().Document.FileName;

                string filename = Server.MapPath("~/Document/Insurance/" + CompanyKey + "/" + InsuranceKey + "/" + ff);
                string fname = Server.MapPath("~/Document/Insurance/" + InsuranceKey+ff);
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

                TempData["ErrorMessage"] =  "File not found";
                System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
                return RedirectToAction("PMWorkOrdersView", new { bidRequestKey });
            }
            catch
            {


                return RedirectToAction("PMWorkOrdersView", new { bidRequestKey });
            }


        }

        public JsonResult bindBidRequestDocument(int BidVendorKey)
        {
            try
            {
                BidVendorModel vendorModel = new BidVendorModel();
                vendorModel = _bidRequestservice.GetBidVendorByBidVendorKey(BidVendorKey);
                int Modulekey = 100;
                IList<BidRequestModel> Documentlist = null;
                Documentlist = _bidRequestservice.GetbindWorkOrderDocument(BidVendorKey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                //int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                string[] doc = new string[Documentlist.Count];
                var a = BidVendorKey;
                for (int i = 0; i < Documentlist.Count; i++)
                {

                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "~/Document/Properties/" + Documentlist[i].BidVendorKey + " " + Documentlist[i].FileName;
                    //if (Documentlist[i].FileName.Contains(Documentlist[i].BidVendorKey + " "))
                    //    imagelist = "~/Document/Properties/" + Documentlist[i].FileName;
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
                Documentlist = _bidRequestservice.GetbindWorkOrderDocument(BidVendorKey1);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                //int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                string[] doc = new string[Documentlist.Count];
                var a = BidVendorKey1;
                for (int i = 0; i < Documentlist.Count; i++)
                {

                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "~/Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
                    if (Documentlist[i].FileName.Contains(Documentlist[i].BidVendorKey + " "))
                        imagelist = "~/Document/Properties/" + Documentlist[i].FileName;
                    var path = Server.MapPath(imagelist);
                    string input = Documentlist[i].FileName;
                    string[] values = input.Split('.');
                    var checkext = values[1];
                    if (checkext == "png" || checkext == "jpg")
                    {
                        vendorModel.checkimg = true;
                        string imagelist1 = "~/Document/Properties/" + Documentlist[i].BidVendorKey + " " + Documentlist[i].FileName;
                        if (Documentlist[i].FileName.Contains(Documentlist[i].BidVendorKey + " "))
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
        public ActionResult PMWorkOrdersEdit(int BidRequestKey)
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
        public ActionResult Delete(int BidRequestKey)
        {

            bool Status = false;
            Status = _bidRequestservice.Delete(BidRequestKey);
            if (Status == true)
            {
                TempData["Sucessmessage"] = "Record has been deleted successfully.";
                return RedirectToAction("PMWorkOrderslist", "PMWorkOrders");
            }
            else
            {
                ViewData["Errormessage"] = "Error";
                return RedirectToAction("PMWorkOrderslist", "PMWorkOrders");
            }

        }
        public ActionResult DocumentDelete(int BidRequestKey, string Docname)
        {
            try
            {
                string Directory = Server.MapPath("~/Document/Properties/");
                string fileName = Docname;
                string path = Directory + BidRequestKey + "" + Docname;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                bool value = false;
                value = _bidRequestservice.DocumentDelete(BidRequestKey, Docname);
                return RedirectToAction("PMWorkOrdersView", new { BidRequestKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("PMWorkOrdersView", new { BidRequestKey });
            }
        }

        public JsonResult PMWorkOrdersCheckComeFromBIdRequest(string BidRequestKey)
        {
            int value = 0;

            value = _bidRequestservice.PMWorkOrdersCheckComeFromBIdRequest(BidRequestKey);

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PMBidStatusUpdate(string status, int BidVendorKey, int BidRequestKey,int moduleKey)
        {
            try
            {
                long ResourceKey = Convert.ToInt64(Session["resourceid"]);
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
                   
                    if (status == "Submitted")
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
                return View();

            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
    }
}
