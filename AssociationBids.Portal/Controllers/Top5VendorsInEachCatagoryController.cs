using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class Top5VendorsInEachCatagoryController : Controller
    {
        // GET: Top5VendorsInEachCatagory

        private readonly AssociationBids.Portal.Service.Base.IAStaffDirectoryService _staffDirectoryservice;

        public Top5VendorsInEachCatagoryController(AssociationBids.Portal.Service.Base.IAStaffDirectoryService staffDirectoryService)
        {
            this._staffDirectoryservice = staffDirectoryService;
        }
        // GET: StaffDirectory
        public ActionResult Top5VendorsInEachCatagoryList()
        {

            return View();
        }

        public JsonResult IndexreStaffPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {

            string CompanyKey = "";
            CompanyKey = Convert.ToString(Session["CompanyKey"]);
            if (CompanyKey == "0")
            {
                CompanyKey = "15";
            }
            List<AStaffDirectoryModel> lstuser = null;
            lstuser = _staffDirectoryservice.SearchStaff(PageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}