
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class VenderWorkOrdersController : Controller
    {
        private readonly IBidRequestService _venderbidrequestservice;
        public VenderWorkOrdersController(IBidRequestService registrationService)
        {
            this._venderbidrequestservice = registrationService;
        }
        // GET: WorkOrders
        public ActionResult VenderWorkorder()
        {
            ViewBag.StatusListForDDL = this._venderbidrequestservice.GetStatusListForDDL("", "");

            return View();
        }
        public ActionResult VenderWorkorderView(int BidVendorKey, int ResourceKey, string BidStartDate)
        {
            ViewBag.BidVendorKey = BidVendorKey;
            ViewBag.ResourceKey = ResourceKey;
            ViewBag.BidStartDate = BidStartDate;
            List<BidVendorModel> lstVendor = null;
            BidVendorModel staffDirectory = new BidVendorModel();
            //int abc = Convert.ToInt32(BidVendorKey);
            staffDirectory = _venderbidrequestservice.GetBidVendorByBidVendorKey(BidVendorKey);
            //lstVendor = _venderbidrequestservice.SearchVendorByBidRequest(BidVendorKey, 100);
            //int resourcekey = Convert.ToInt32(Session["resourceid"]);
            //staffDirectory.ResourceKey = resourcekey;
            //if (staffDirectory.BidDueDates != null && staffDirectory.BidDueDates != "")
            //{
            //    if (Convert.ToDateTime(staffDirectory.BidDueDates) < Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")))
            //        staffDirectory.isExpired = true;
            //    else
            //        staffDirectory.isExpired = false;
            //}
            //else
            //    staffDirectory.isExpired = true;
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
            staffDirectory.BidStartDate = BidStartDate;
            Session["img"] = staffDirectory;
            return View();
        }
        public ActionResult VenderWorkorderViewMoreClone(Int64 BidVendorKey, Int64 ResourceKey, string BidStartDate)
        {
            ViewBag.BidVendorKey = BidVendorKey;
            ViewBag.ResourceKey = ResourceKey;
            ViewBag.BidStartDate = BidStartDate;
            BidVendorModel vendorModel = new BidVendorModel();
            vendorModel = (BidVendorModel)Session["img"];
            return View(vendorModel);
            //return View();
        }

        public ActionResult DocumentDelete(int BidRequestKey, string Docname, int ResourceKey, string BidStartDate)
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
                return RedirectToAction("VenderWorkorderView", new { BidVendorKey = BidRequestKey, ResourceKey = ResourceKey, BidStartDate = BidStartDate });
            }
            catch (Exception ex)
            {
                return RedirectToAction("VenderWorkorderView", new { BidVendorKey = BidRequestKey, ResourceKey = ResourceKey, BidStartDate = BidStartDate });
            }
        }
    }
}