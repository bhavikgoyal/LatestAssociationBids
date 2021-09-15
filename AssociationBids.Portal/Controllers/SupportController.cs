using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class SupportController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IBidRequestService _bidRequestservice;
        private readonly AssociationBids.Portal.Service.Base.ISupportService _Supportservice;
        // GET: Support
        public SupportController(AssociationBids.Portal.Service.Base.ISupportService Supportservice, AssociationBids.Portal.Service.Base.IBidRequestService bidRequestservice)
        {
            this._Supportservice = Supportservice;
            this._bidRequestservice = bidRequestservice;
        }
        public ActionResult SupportView()
        {
            //lstCompany lstProperty lstVendor lstBidStatus lstStatus
            
            var lstBidStatus = new List<SelectListItem>()
            {
              //new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="Both",Selected=true},
              new SelectListItem{ Value="2",Text="Bid Requests"},
              new SelectListItem{ Value="3",Text="Work Orders"},
            };
            ViewBag.lstBidStatus = lstBidStatus;           
            IList<LookUpModel> lstBidReqStatus = _bidRequestservice.GetBidRequetStatusFilter();
            List<System.Web.Mvc.SelectListItem> lstStatus = new List<System.Web.Mvc.SelectListItem>();
            lstStatus.Add(new SelectListItem() { Text = "Show All", Value = "0,0" });
            for (int i = 0; i < lstBidReqStatus.Count; i++)
            {
                lstStatus.Add(new SelectListItem() { Text = Convert.ToString(lstBidReqStatus[i].Title), Value = Convert.ToString(lstBidReqStatus[i].LookUpKey1) });
            }
            ViewBag.lstStatus = lstStatus;
            return View();
        }

        public JsonResult IndexreStaffPaging(Int64 PageSize, Int64 PageIndex, string PropertyName, string VendorName, string CompanyName, String Sort, Int32 BidStatus,
            string BidRequestStatus, int Modulekey, string StartDate, string BidDueDate)
        {
            List<SupportModel> lstuser = null;
           
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            if (StartDate != "")
            {
                FromDate = Convert.ToDateTime(StartDate);
            }
            if (BidDueDate != "")
            {
                ToDate = Convert.ToDateTime(BidDueDate);
            }
            //Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            Int32 resourcekey = 0;
            lstuser = _Supportservice.SearchBidRequest(PageSize, PageIndex, PropertyName, VendorName, CompanyName, Sort, BidStatus, resourcekey, BidRequestStatus, Modulekey, FromDate, ToDate);


                lstuser.ForEach(f => f.ispriorityrecord = false);

                return Json(lstuser, JsonRequestBehavior.AllowGet);
          
           

        }

        public ActionResult SupportViews(int BidRequestKey)
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

                return Json(lstAllVendor);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult SearchVendorByBidRequest()
        {
            List<BidRequestModel> lstVendor = null;
            //long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());
            lstVendor = _bidRequestservice.SearchVendorByBidRequest(BidRequestKey, modulekey);
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
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

        public ActionResult SupportWorkOrdersView(int BidRequestKey)
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

        public JsonResult PMWorkOrdersCheckComeFromBIdRequest(string BidRequestKey)
        {
            int value = 0;

            value = _bidRequestservice.PMWorkOrdersCheckComeFromBIdRequest(BidRequestKey);

            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}