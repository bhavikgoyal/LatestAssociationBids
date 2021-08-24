using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class StaffDirectoryController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IStaffDirectoryServices _staffDirectoryservice;

        public StaffDirectoryController(AssociationBids.Portal.Service.Base.IStaffDirectoryServices staffDirectoryService)
        {
            this._staffDirectoryservice = staffDirectoryService;
        }
        // GET: StaffDirectory
        public ActionResult StaffDirectoryList()
        {
            return View();
        }

        public JsonResult IndexreStaffPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            string CompanyKey = "";
            CompanyKey = Convert.ToString(Session["CompanyKey"]); 
            List<StaffDirectoryModel> lstuser = null;
            lstuser = _staffDirectoryservice.SearchStaff(PageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexreStaffPagingForAdvancedSearch(Int64 PageSize, Int64 PageIndex, string Search, string Status, String Sort)
        {
            string CompanyKey = "";
            CompanyKey = Convert.ToString(Session["CompanyKey"]);
            List<StaffDirectoryModel> lstuser = null;
            lstuser = _staffDirectoryservice.AdvancedSearchStaff(PageSize, PageIndex, Search.Trim(), Status.Trim(),Sort, CompanyKey);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult checkPortal()
        {



            try
            {

                int Portalkey = Convert.ToInt32(Session["Portalkey"]);
                if (Portalkey == 0 || Portalkey != 2)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
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


        public ActionResult StaffDirectoryAdd()
        {
            IList<StaffDirectoryModel> lststate = null;
            StaffDirectoryModel lstGroup = new StaffDirectoryModel();
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

         



            lstGroup.GroupList = _staffDirectoryservice.GetAllGroup().ToList();

            List<System.Web.Mvc.SelectListItem> Status = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sliii1 = new System.Web.Mvc.SelectListItem();
            sliii1.Text = "All";
            sliii1.Value = "0";
            Status.Add(sliii1);
            System.Web.Mvc.SelectListItem sliii2 = new System.Web.Mvc.SelectListItem();
            sliii2.Text = "Active";
            sliii2.Value = "1";
            Status.Add(sliii2);
            System.Web.Mvc.SelectListItem sliii3 = new System.Web.Mvc.SelectListItem();
            sliii3.Text = "InActive";
            sliii3.Value = "2";
            Status.Add(sliii3);

            ViewBag.Status = Status;
            return View(lstGroup);
        }
        [HttpPost]
        public ActionResult StaffDirectoryAdd(StaffDirectoryModel staffDirectoryModel, HttpPostedFileBase UploadImage)
        {
            try
            {
                Int64 value = 0;
                string filename = "";
                staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                staffDirectoryModel.Password = "12345";

                if (staffDirectoryModel.Statuss == true)
                {
                    staffDirectoryModel.Status = 2;
                }
                else
                {
                    staffDirectoryModel.Status = 2;
                }
                bool ErrorMsg = _staffDirectoryservice.CheckDuplicatedEmail(staffDirectoryModel.Email);
                if (ErrorMsg != true)
                {
                    if (UploadImage != null)
                    {
                        filename = Path.GetFileName(UploadImage.FileName);
                        staffDirectoryModel.FileName = filename;
                        staffDirectoryModel.FileSize = UploadImage.ContentLength;
                    }


                    if (!string.IsNullOrEmpty(staffDirectoryModel.GroupId))
                    {
                        staffDirectoryModel.GroupId = staffDirectoryModel.GroupId.Trim().Trim(',').Trim();
                    }
                    value = _staffDirectoryservice.Insert(staffDirectoryModel);
                    if (UploadImage != null)
                    {
                        //string path = Path.Combine(Server.MapPath("~/Images/StaffDirectory/"), value + " " + filename);
                        string path = Path.Combine(Server.MapPath("~/Documents/Users/"), value + " " + filename);
                        UploadImage.SaveAs(path);
                    }
                        if (value != 0)
                        {
                            TempData["Sucessmessage"] = "Record has been inserted successfully.";
                            return RedirectToAction("StaffDirectoryList");  
                        }
                        else
                        {
                            ViewData["Errormessage"] = "Error";
                            return RedirectToAction("StaffDirectoryAdd");
                        }
                }
                else
                {
                    IList<StaffDirectoryModel> lststate = null;
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

                    staffDirectoryModel.GroupList = _staffDirectoryservice.GetAllGroup().ToList();

                 

                    





                    ViewBag.ErrorMessage = "Email already exits.";
                    
                    return View(staffDirectoryModel);
                }
                
            }
            catch (Exception ex)
            {
                return View("StaffDirectoryAdd");
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

        public JsonResult checkUserRole()
        {
            try
            {
                Int64 status = 0;
           
                  int ResourceKey  = Convert.ToInt32(Session["resourceid"]);

                status = _staffDirectoryservice.checkUserrole(ResourceKey);
                if (status == 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
               
             

            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }




        public JsonResult GetAllGroup()
        {
            List<StaffDirectoryModel> lstuser = null;
            lstuser = _staffDirectoryservice.GetAllGroup();
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StaffDirectoryView(int ResourceKey)
        {
            List<StaffDirectoryModel> staffDirectoryList = null;
            StaffDirectoryModel staffDirectory = new StaffDirectoryModel();
            staffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ResourceKey);
            //staffDirectoryList = _staffDirectoryservice.GetDataviewGroupCheckbox(ResourceKey);

            staffDirectory.ResourceKey = staffDirectoryList[0].ResourceKey;
            staffDirectory.ResourceTypeKey = staffDirectoryList[0].ResourceTypeKey;
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
            staffDirectory.Status = staffDirectoryList[0].Status;
            staffDirectory.PrimaryContact = staffDirectoryList[0].PrimaryContact;
            staffDirectory.Description = staffDirectoryList[0].Description;
            staffDirectory.Description = staffDirectory.Description.Replace("\r\n", "<br>");
            staffDirectory.UserName = staffDirectoryList[0].UserName;
            staffDirectory.GroupKey = staffDirectoryList[0].GroupKey;
            staffDirectory.Password = staffDirectoryList[0].Password;


          







          
            if (staffDirectory.Status == 2 || staffDirectory.Status == 101)
            {
                staffDirectory.Statuss = true;
                
            }
            else
            {
                staffDirectory.Statuss = false;
            }

         
          

            for (int i = 0; i < staffDirectoryList.Count ; i++) {
                staffDirectory.GroupResponseId += staffDirectoryList[i].GroupId+ ",";
            }


            return View(staffDirectory);
        }

        public ActionResult StaffDirectoryEditStaff(int ResourceKey)
        {

            IList<StaffDirectoryModel> lststate = null;
            StaffDirectoryModel lstGroup = new StaffDirectoryModel();
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

            List<StaffDirectoryModel> staffDirectoryList = null;
            StaffDirectoryModel staffDirectory = new StaffDirectoryModel();
            staffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ResourceKey);
            staffDirectory.ResourceTypeKey = staffDirectoryList[0].ResourceTypeKey;
            staffDirectory.UserKey = staffDirectoryList[0].UserKey;
            staffDirectory.ResourceKey = staffDirectoryList[0].ResourceKey;
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
            staffDirectory.Zip = staffDirectoryList[0].Zip;
            staffDirectory.Status = staffDirectoryList[0].Status;
            staffDirectory.PrimaryContact = staffDirectoryList[0].PrimaryContact;
            staffDirectory.Status = staffDirectoryList[0].Status;
            staffDirectory.Description = staffDirectoryList[0].Description;

          

            if (staffDirectory.Status == 2 || staffDirectory.Status == 101)
            {
                staffDirectory.Statuss = true;

            }
            else
            {
                staffDirectory.Statuss = false;
            }


            return View(staffDirectory);
        }

        public ActionResult StaffDirectoryEditGroup(int ResourceKey)
        {
            List<StaffDirectoryModel> staffDirectoryList = null;
            StaffDirectoryModel staffDirectory = new StaffDirectoryModel();
            staffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ResourceKey);

            for (int i = 0; i < staffDirectoryList.Count; i++)
            {
                staffDirectory.GroupResponseId += staffDirectoryList[i].GroupId + ",";
            }
            return View(staffDirectory);
        }

        public ActionResult StaffDirectoryEditUser(int ResourceKey)
        {
            List<StaffDirectoryModel> staffDirectoryList = null;
            StaffDirectoryModel staffDirectory = new StaffDirectoryModel();
            staffDirectoryList = _staffDirectoryservice.GetDataViewEdit(ResourceKey);

            staffDirectory.ResourceKey = staffDirectoryList[0].ResourceKey;
            staffDirectory.UserName = staffDirectoryList[0].UserName;
            staffDirectory.Password = staffDirectoryList[0].Password;
            staffDirectory.FileName = staffDirectoryList[0].FileName;

            return View(staffDirectory);
        }

        [HttpPost]
        public ActionResult StaffDirectoryEditStaff(int ResourceKey, StaffDirectoryModel staffDirectoryModel)
        {
            try
            {
                Int64 value = 0;
                bool ErrorMsg = false;

               staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                if (staffDirectoryModel.OldEmail != staffDirectoryModel.Email)
                {
                    ErrorMsg = _staffDirectoryservice.CheckDuplicatedEmail(staffDirectoryModel.Email);
                }
                if (ErrorMsg != true)
                {
                    if (staffDirectoryModel.Statuss == true)
                    {
                        staffDirectoryModel.Status = 101;
                    }
                    else
                    {
                        staffDirectoryModel.Status = 1;
                    }
                    value = _staffDirectoryservice.StaffDirectoryEditStaff(staffDirectoryModel);
                    if (value == 0)
                    {
                        TempData["Sucessmessage"] = "Record has been updated successfully.";

                        return RedirectToAction("StaffDirectoryView", new { ResourceKey });
                    }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                        return RedirectToAction("StaffDirectoryView", new { ResourceKey });
                    }

                }
                else
                {
                    IList<StaffDirectoryModel> lststate = null;
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

                    staffDirectoryModel.GroupList = _staffDirectoryservice.GetAllGroup().ToList();

                    ViewBag.ErrorMessage = "Email already exits.";

                    return View(staffDirectoryModel);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("StaffDirectoryView", new { ResourceKey });
            }
         }

        [HttpPost]
        public ActionResult StaffDirectoryEditGroup(int ResourceKey, StaffDirectoryModel staffDirectoryModel)
        {
            try
            {
                Int64 value = 0;
                staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                value = _staffDirectoryservice.StaffDirectoryEditGroup(staffDirectoryModel);
                if (value == 0)
                {
                    TempData["Sucessmessage"] = "Record has been updated successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("StaffDirectoryView", new { ResourceKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("StaffDirectoryView", new { ResourceKey });
            }
        }

        [HttpPost]
        public ActionResult StaffDirectoryEditUser(int ResourceKey, StaffDirectoryModel staffDirectoryModel, HttpPostedFileBase UploadImage)
        {
            try
            {
                Int64 value = 0;
                staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                string filename = "";
                
                if (UploadImage != null)
                {
                    filename = Path.GetFileName(UploadImage.FileName);
                    staffDirectoryModel.FileName = filename;
                    staffDirectoryModel.FileSize = UploadImage.ContentLength;
                }
                value = _staffDirectoryservice.StaffDirectoryEditUser(staffDirectoryModel);
                if (UploadImage != null)
                {
                    //string path = Path.Combine(Server.MapPath("~/Images/StaffDirectory/"), value + " " + filename);
                    string path = Path.Combine(Server.MapPath("~/Documents/Users/"), value + " " + filename);
                    
                    UploadImage.SaveAs(path);
                }
                if (value == 0)
                {
                    TempData["Sucessmessage"] = "Record has been updated successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }

                return RedirectToAction("StaffDirectoryView", new { ResourceKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("StaffDirectoryView", new { ResourceKey });
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
                return View("StaffDirectoryList");
            }
            else
            {
                ViewData["Errormessage"] = "Error";
                return View("StaffDirectoryList");
            }
            
        }

    }
}
