using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AssociationBids.Portal.Model;
using System.IO;
using System.Configuration;
using System.Web.Configuration;

namespace AssociationBids.Portal.Controllers
{
    public class AStaffDirectoryController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IAStaffDirectoryService _staffDirectoryservice;

        public AStaffDirectoryController(AssociationBids.Portal.Service.Base.IAStaffDirectoryService staffDirectoryService)
        {
            this._staffDirectoryservice = staffDirectoryService;
        }
        // GET: StaffDirectory
        public ActionResult AStaffDirectoryList()
        {
           
            return View();
        }

        public JsonResult IndexreStaffPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {

            string CompanyKey = "";
            CompanyKey = Convert.ToString(Session["CompanyKey"]);
            if(CompanyKey == "0")
            {
                CompanyKey = "15";
            }
            List<AStaffDirectoryModel> lstuser = null;
            lstuser = _staffDirectoryservice.SearchStaff(PageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexreStaffPagingForAdvancedSearch(Int64 PageSize, Int64 PageIndex, string Search, string Status, String Sort)
        {

            string CompanyKey = "";
            CompanyKey = Convert.ToString(Session["CompanyKey"]);
            if (CompanyKey == "0")
            {
                CompanyKey = "15";
            }
            List<AStaffDirectoryModel> lstuser = null;
            lstuser = _staffDirectoryservice.AdvancedSearchStaff(PageSize, PageIndex, Search.Trim(), Status.Trim(),Sort, CompanyKey);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AStaffDirectoryAdd()
        {
            IList<AStaffDirectoryModel> lststate = null;
            AStaffDirectoryModel lstGroup = new AStaffDirectoryModel();
            //list of state
            lststate = _staffDirectoryservice.GetAllState();
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }
            ViewBag.lststate = lststatelist;

            List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
            sli1.Text = "--Please Select--";
            sli1.Value = "0";
            list.Add(sli1);
            System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
            slii2.Text = "Property Manager Company";
            slii2.Value = "1001";
            list.Add(slii2);
            System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
            sli3.Text = "Admin";
            sli3.Value = "1000";
            list.Add(sli3);
           
            ViewBag.lst = list;
            //lstGroup.GroupList = _staffDirectoryservice.GetAllGroup().ToList();
            return View(lstGroup);
        }
        [HttpPost]
        public ActionResult AStaffDirectoryAdd(AStaffDirectoryModel staffDirectoryModel, HttpPostedFileBase UploadImage)
        {
            try
            {
                Int32 st = 0;
                Int64 value = 0;
                string filename = "";
                staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                staffDirectoryModel.Password = "12345";
                string lst = staffDirectoryModel.lst;
                bool ErrorMsg = _staffDirectoryservice.CheckDuplicatedEmail(staffDirectoryModel.Email);
                if (ErrorMsg != true)
                {
                    if (UploadImage != null)
                    {
                        filename = Path.GetFileName(UploadImage.FileName);
                        staffDirectoryModel.FileName = filename;
                        staffDirectoryModel.FileSize = UploadImage.ContentLength;
                    }
                  
                       staffDirectoryModel.Status = 101;
                       
              
                       
                 
                  
                    value = _staffDirectoryservice.Insert(staffDirectoryModel,st);
                    if (UploadImage != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images/StaffDirectory/"), value + " " + filename);
                        UploadImage.SaveAs(path);
                    }
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Record has been inserted successfully.";
                    }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                    }


                    return RedirectToAction("AStaffDirectoryList");
                }
                else
                {
                    IList<AStaffDirectoryModel> lststate = null;
                    //list of state
                    lststate = _staffDirectoryservice.GetAllState();
                    List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                    System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                    sli2.Text = "--Please Select--";
                    sli2.Value = "0";
                    lststatelist.Add(sli2);
                    for (int i = 0; i < lststate.Count; i++)
                    {
                        System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                        sli.Text = Convert.ToString(lststate[i].State);
                        sli.Value = Convert.ToString(lststate[i].StateKey);
                        lststatelist.Add(sli);
                    }
                    ViewBag.lststate = lststatelist;

                    List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();
                    System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                    sli1.Text = "--Please Select--";
                    sli1.Value = "0";
                    list.Add(sli1);
                    System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
                    slii2.Text = "Property Manager Company";
                    slii2.Value = "1001";
                    list.Add(slii2);
                    System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
                    sli3.Text = "Admin";
                    sli3.Value = "1000";
                    list.Add(sli3);

                    ViewBag.lst = list;

                    //staffDirectoryModel.GroupList = _staffDirectoryservice.GetAllGroup().ToList();

                    ViewBag.ErrorMessage = "Email are Already Exists";

                    return View(staffDirectoryModel);
                }

            }
            catch (Exception ex)
            {
                return View("StaffDirectoryList");
            }
        }

        public JsonResult IsEmailExists(string emailId)
        {
            bool ErrorMsg = _staffDirectoryservice.CheckDuplicatedEmail(emailId);
            if (ErrorMsg != true)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllGroup()
        {
            List<AStaffDirectoryModel> lstuser = null;
            lstuser = _staffDirectoryservice.GetAllGroup();
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AStaffDirectoryView(int ResourceKey)
        {
            List<AStaffDirectoryModel> staffDirectoryList = null;
            AStaffDirectoryModel staffDirectory = new AStaffDirectoryModel();
            staffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ResourceKey);

            staffDirectory.ResourceKey = staffDirectoryList[0].ResourceKey;
            staffDirectory.UserKey = staffDirectoryList[0].UserKey;
            staffDirectory.FirstName = staffDirectoryList[0].FirstName;
            staffDirectory.LastName = staffDirectoryList[0].LastName;
            staffDirectory.Email = staffDirectoryList[0].Email;
            staffDirectory.Email2 = staffDirectoryList[0].Email2;
            staffDirectory.CellPhone = staffDirectoryList[0].CellPhone;
            staffDirectory.Work = staffDirectoryList[0].Work;
            staffDirectory.Work2 = staffDirectoryList[0].Work2;
            staffDirectory.Fax = staffDirectoryList[0].Fax;
            staffDirectory.Address = staffDirectoryList[0].Address;
            staffDirectory.Address2 = staffDirectoryList[0].Address2;
            staffDirectory.City = staffDirectoryList[0].City;
            staffDirectory.State = staffDirectoryList[0].State;
            staffDirectory.Zip = staffDirectoryList[0].Zip;
            staffDirectory.PrimaryContact = staffDirectoryList[0].PrimaryContact;
            staffDirectory.Description = staffDirectoryList[0].Description;
            staffDirectory.Description = staffDirectory.Description.Replace("\r\n", "<br>");            
            staffDirectory.UserName = staffDirectoryList[0].UserName;
            staffDirectory.Password = staffDirectoryList[0].Password;
            staffDirectory.Status = staffDirectoryList[0].Status;
            if (staffDirectory.Status == 2 || staffDirectory.Status == 101)
            {
                staffDirectory.Statuss = true;
            }
            else
            {
                staffDirectory.Statuss = false;
            }
            for (int i = 0; i < staffDirectoryList.Count; i++)
            { 
                staffDirectory.GroupResponseId += staffDirectoryList[i].GroupId + ",";
            }
            return View(staffDirectory);
        }

        public ActionResult AStaffDirectoryEditStaff(int ResourceKey)
        {

            IList<AStaffDirectoryModel> lststate = null;
            AStaffDirectoryModel lstGroup = new AStaffDirectoryModel();
            //list of state
            lststate = _staffDirectoryservice.GetAllState();
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }
            ViewBag.lststate = lststatelist;

           


            List<AStaffDirectoryModel> staffDirectoryList = null;
            AStaffDirectoryModel staffDirectory = new AStaffDirectoryModel();
            staffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ResourceKey);

            staffDirectory.ResourceKey = staffDirectoryList[0].ResourceKey;
            staffDirectory.ResourceTypeKey = staffDirectoryList[0].ResourceTypeKey;

            staffDirectory.FirstName = staffDirectoryList[0].FirstName;
            staffDirectory.LastName = staffDirectoryList[0].LastName;
            staffDirectory.Email = staffDirectoryList[0].Email;
            staffDirectory.OldEmail = staffDirectoryList[0].Email;
            staffDirectory.Email2 = staffDirectoryList[0].Email2;
            staffDirectory.CellPhone = staffDirectoryList[0].CellPhone;
            staffDirectory.Work = staffDirectoryList[0].Work;
            staffDirectory.Work2 = staffDirectoryList[0].Work2;
            staffDirectory.Fax = staffDirectoryList[0].Fax;
            staffDirectory.Address = staffDirectoryList[0].Address;
            staffDirectory.Address2 = staffDirectoryList[0].Address2;
            staffDirectory.City = staffDirectoryList[0].City;
            staffDirectory.State = staffDirectoryList[0].State;
            staffDirectory.Status = staffDirectoryList[0].Status;
            staffDirectory.Zip = staffDirectoryList[0].Zip;
            staffDirectory.PrimaryContact = staffDirectoryList[0].PrimaryContact;
            staffDirectory.Description = staffDirectoryList[0].Description;


            if (staffDirectory.Status == 2 || staffDirectory.Status == 101)
            {
                staffDirectory.Statuss = true;
            }
            else
            {
                staffDirectory.Statuss = false;
            }

         


            if (staffDirectory.ResourceTypeKey == 1000)
            {

                List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();

                System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
                slii2.Text = "Admin";
                slii2.Value = "1000";
                list.Add(slii2);
                System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
                sli3.Text = "Property Manager Company";
                sli3.Value = "1001";
                list.Add(sli3);
               

                ViewBag.lst = list;

            }
             else if (staffDirectory.ResourceTypeKey == 1001)
            {
                List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();


                System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
                slii2.Text = "Property Manager Company";
                slii2.Value = "1001";
                list.Add(slii2);
                System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
                sli3.Text = "Admin";
                sli3.Value = "1000";
                list.Add(sli3);

                ViewBag.lst = list;
            }
      else
            {
                List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                sli1.Text = "--Please Select--";
                sli1.Value = "0";
                list.Add(sli1);
                System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
                slii2.Text = "property manager company";
                slii2.Value = "1001";
                list.Add(slii2);
                System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
                sli3.Text = "admin";
                sli3.Value = "1000";
                list.Add(sli3);

                ViewBag.lst = list;
            }


            return View(staffDirectory);
        }

       
        [HttpPost]
        public ActionResult AStaffDirectoryEditStaff(int ResourceKey, AStaffDirectoryModel staffDirectoryModel)
        {
            try
            {
                Int64 value = 0;
                bool ErrorMsg = false;
                staffDirectoryModel.CompanyKey = 15;
                if (staffDirectoryModel.OldEmail != staffDirectoryModel.Email) {
                    ErrorMsg = _staffDirectoryservice.CheckDuplicatedEmail(staffDirectoryModel.Email);
                }
                if (ErrorMsg != true)
                {
                    if (staffDirectoryModel.Statuss == true)
                    {
                        staffDirectoryModel.Status = 2;

                    }
                    else
                    {
                        staffDirectoryModel.Status = 1;
                    }
                    staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                    value = _staffDirectoryservice.StaffDirectoryEditStaff(staffDirectoryModel);
                    if (value == 0)
                    {
                        TempData["Sucessmessage"] = "Record has been updated successfully.";
                    }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                    }

                    return RedirectToAction("AStaffDirectoryView", new { ResourceKey });
                }
                else
                {
                    IList<AStaffDirectoryModel> lststate = null;
                    //list of state
                    lststate = _staffDirectoryservice.GetAllState();
                    List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                    System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                    sli2.Text = "--Please Select--";
                    sli2.Value = "0";
                    lststatelist.Add(sli2);
                    for (int i = 0; i < lststate.Count; i++)
                    {
                        System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                        sli.Text = Convert.ToString(lststate[i].State);
                        sli.Value = Convert.ToString(lststate[i].StateKey);
                        lststatelist.Add(sli);
                    }
                    ViewBag.lststate = lststatelist;

                    List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();
                    System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                    sli1.Text = "--Please Select--";
                    sli1.Value = "0";
                    list.Add(sli1);
                    System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
                    slii2.Text = "Property Manager Company";
                    slii2.Value = "1001";
                    list.Add(slii2);
                    System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
                    sli3.Text = "Admin";
                    sli3.Value = "1000";
                    list.Add(sli3);

                    ViewBag.lst = list;

                    //staffDirectoryModel.GroupList = _staffDirectoryservice.GetAllGroup().ToList();

                    ViewBag.ErrorMessage = "Email are Already Exists";

                    return View(staffDirectoryModel);
                }
            }
            catch (Exception ex)
            {
                return View("AStaffDirectoryList");
            }
        }


        public ActionResult AStaffDirectoryEditGroup(int ResourceKey)
        {
            List<AStaffDirectoryModel> staffDirectoryList = null;
            AStaffDirectoryModel staffDirectory = new AStaffDirectoryModel();
            staffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ResourceKey);

            for (int i = 0; i < staffDirectoryList.Count; i++)
            {
                staffDirectory.GroupResponseId += staffDirectoryList[i].GroupId + ",";
            }
            return View(staffDirectory);
        }

      

        [HttpPost]
        public ActionResult AStaffDirectoryEditGroup(int ResourceKey, AStaffDirectoryModel staffDirectoryModel)
        {
            try
            {
                Int64 value = 0;
                staffDirectoryModel.CompanyKey = 15;
                value = _staffDirectoryservice.StaffDirectoryEditGroup(staffDirectoryModel);
                if (value != 0)
                {
                    TempData["Sucessmessage"] = "Record has been inserted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("AStaffDirectoryView", new { ResourceKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("AStaffDirectoryView", new { ResourceKey });
            }
        }


        public ActionResult AStaffDirectoryEditUser(int ResourceKey)
        {
            List<AStaffDirectoryModel> staffDirectoryList = null;
            AStaffDirectoryModel staffDirectory = new AStaffDirectoryModel();
                
            staffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ResourceKey);

            staffDirectory.ResourceKey = staffDirectoryList[0].ResourceKey;
            staffDirectory.UserName = staffDirectoryList[0].UserName;
            staffDirectory.Password = staffDirectoryList[0].Password;

            return View(staffDirectory);
        }

        [HttpPost]
        public ActionResult AStaffDirectoryEditUser(int ResourceKey, AStaffDirectoryModel AstaffDirectoryModel, HttpPostedFileBase UploadImage)
        {
            try
            {
                Int64 value = 0;
                AstaffDirectoryModel.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                string filename = "";

                if (UploadImage != null)
                {
                    filename = Path.GetFileName(UploadImage.FileName);
                    AstaffDirectoryModel.FileName = filename;
                    AstaffDirectoryModel.FileSize = UploadImage.ContentLength;
                }
                AstaffDirectoryModel.CompanyKey = 15;
                value = _staffDirectoryservice.StaffDirectoryEditUser(AstaffDirectoryModel);
                if (UploadImage != null)
                {
                    //string path = Path.Combine(Server.MapPath("~/Images/StaffDirectory/"), value + " " + filename);
                    string path = Path.Combine(Server.MapPath("~/Documents/Users/"), value + " " + filename);

                    UploadImage.SaveAs(path);
                }
                if (value == 0 || value != 0)
                {
                    TempData["Sucessmessage"] = "Record has been updated successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }

                return RedirectToAction("AStaffDirectoryView", new { ResourceKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("AStaffDirectoryView", new { ResourceKey });
            }
        }

        public JsonResult ResetPassword(int UserKey)
        {
            bool Status = false;
            Status = _staffDirectoryservice.ResetPassword(UserKey);
            return Json(Status, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int id)
        {
            bool Status = false;
            Status = _staffDirectoryservice.Remove(id);
            if (Status == true)
            {
                TempData["Sucessmessage"] = "Record has been deleted successfully.";
                return View("AStaffDirectoryList");
            }
            else
            {
                ViewData["Errormessage"] = "Error";
                return View("AStaffDirectoryList");
            }

        }
    }
}
