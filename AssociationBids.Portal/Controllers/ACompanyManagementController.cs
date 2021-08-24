using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class ACompanyManagementController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.Interface.IACompanymangService __companyservice;
        private readonly AssociationBids.Portal.Service.Base.Interface.IPMPropertiesService __pMPropertiesService;

        public ACompanyManagementController(AssociationBids.Portal.Service.Base.Interface.IACompanymangService compnyService, AssociationBids.Portal.Service.Base.Interface.IPMPropertiesService PMPropertiesService)
        {
            this.__pMPropertiesService = PMPropertiesService;
            this.__companyservice = compnyService;
        }

        // GET: ACompanyManagement
        public ActionResult ACompanylist()
        {
            try
            {
                IList<CompanyModel> lststate = null;
                //list of state
                lststate = __companyservice.GetAllStatee();
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

                //list of status
                List<System.Web.Mvc.SelectListItem> Status = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                sli1.Text = "All";
                sli1.Value = "0";
                Status.Add(sli1);
                System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
                slii2.Text = "Active";
                slii2.Value = "1";
                Status.Add(slii2);
                System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
                sli3.Text = "InActive";
                sli3.Value = "2";
                Status.Add(sli3);

                ViewBag.Status = Status;
                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("ACompanylist");
            }
        }

        public JsonResult IndexcompanyPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort,string State,int Status)
        {
            IList<CompanyModel> itemList = null;
            itemList = __companyservice.SearchCompany(PageSize, PageIndex, Search.Trim(), Sort, State, Status);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        // GET: AddVendor
        public ActionResult ACompanymangAdd()
        {
            try
            {
                IList<CompanyModel> lststate = null;
                //list of state
                lststate = __companyservice.GetAllStatee();
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
                ViewBag.DefaultValue = "2";
                ViewBag.BidSubmitValue = "10";
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult ACompanymangAdd(CompanyModel collection)
        {
            try
            {
                collection.PrimaryContact = true;
                UserModel value ;
                bool ErrorMsg = __companyservice.CheckDuplicatedEmaill(collection.Email);
                if (ErrorMsg != true)
                {
                    collection.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                    value = __companyservice.InsertedCompanyUser(collection);
                    if (value.UserKey > 0)
                    {
                        try
                        {
                            string lookUptitle = "Email for reset password";

                     
                          __companyservice.WinFeeMain(collection.Email, lookUptitle, value.UserKey, value.Username, value.ResetExpirationDate, collection.Name);
                        }
                        catch (Exception ex)
                        {
                        }

                        TempData["Sucessmessage"] = "Record has been inserted successfully.";
                    }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                    }
                    return RedirectToAction("ACompanylist");
                }
                else
                {
                    IList<CompanyModel> lststatee = null;
                    //list of state
                    lststatee = __companyservice.GetAllStatee();
                    List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                    System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                    sli2.Text = "--Please Select--";
                    sli2.Value = "0";
                    lststatelist.Add(sli2);
                    for (int i = 0; i < lststatee.Count; i++)
                    {
                        System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                        sli.Text = Convert.ToString(lststatee[i].State);
                        sli.Value = Convert.ToString(lststatee[i].StateKey);
                        lststatelist.Add(sli);
                    }
                    ViewBag.lststate = lststatelist;
                    ViewBag.ErrorMessage = "Email are Already Exists";

                    return View(collection);
                }
            }
            catch (Exception ex)
            {
                IList<CompanyModel> lststate = null;
                //list of state
                lststate = __companyservice.GetAllStatee();
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
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("ACompanylist");
            }
        }

        // GET: Editcompany
        public ActionResult CompanySettingEdit(int CompanyKey)
        {
            try
            {
                
                int CcompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                IList<CompanyModel> lststate = null;

                //list of state
                lststate = __companyservice.GetAllStatee();
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
                CompanyModel resources = null;
                resources = __companyservice.GetDataViewEditt(CcompanyKey);
                return View(resources);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ACompanylist");
            }
        }

        [HttpPost]
        public ActionResult CompanySettingEdit(CompanyModel collection)
        {

            int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            CompanyModel companyv = new CompanyModel();
            try
            {
                collection.CompanyKey = CompanyKey;
                   Int64 value = 0;
                value = __companyservice.CompanyEdit(collection);
                if (value == 0)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("CompanySetting");
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                    return RedirectToAction("CompanySetting");
                }
            }
            catch (Exception ex)
            {
                ViewData["Errormessage"] = ex.Message;
                TempData["Errormessage"] = ex.Message;
                IList<CompanyModel> lststate = null;

                //list of state
                lststate = __companyservice.GetAllStatee();
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
                return View(collection);
            }
        }

        public ActionResult CompanyDefaultEdit(int CompanyKey)
        {
            try
            {
                int CcompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                CompanyModel resources = null;
                resources = __companyservice.GetDataViewEditt(CcompanyKey);
                return View(resources);
            }
            catch (Exception ex)
            {
                return RedirectToAction("CompanySetting");
            }
        }

        [HttpPost]
        public ActionResult CompanyDefaultEdit(int CompanyKey, CompanyModel collection)
        {
            CompanyModel companyv = new CompanyModel();
            try
            {
                int CcompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                Int64 value = 0;
                //CompanyModel resources = null;
                if(collection.BidRequestResponseDays <= 0)
                {
                    TempData["Errormessage"] = "Please Enter Valid Bid Request Respoonse Due Days";
                    return RedirectToAction("CompanySetting");
                }
                else if (collection.BidSubmitDays <= 0)
                {
                    TempData["Errormessage"] = "Please Enter Valid Bid Request sDue Day";
                    return RedirectToAction("CompanySetting");
                }
                //resources = __companyservice.GetDataViewEditt(CompanyKey);
                //resources.Description = collection.Description;
                //resources.BidRequestResponseDays = collection.BidRequestResponseDays;
                //resources.BidSubmitDays = collection.BidSubmitDays;
                //resources.BidRequestAmount = collection.BidRequestAmount;
                collection.CompanyKey = CcompanyKey;
                value = __companyservice.CompanydefaultEdit(collection);
                if (value == 0)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("CompanySetting");
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                    return RedirectToAction("CompanySetting");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CompanySetting");
            }
        }






        // GET: Editcompany
        public ActionResult ACompanyEdit(int CompanyKey)
        {
            try
            {
                 IList<CompanyModel> lststate = null;
               
                //list of state
                lststate = __companyservice.GetAllStatee();
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
                CompanyModel resources = null;
            resources = __companyservice.GetDataViewEditt(CompanyKey);
                return View(resources);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ACompanylist");
            }
        }

        [HttpPost]
        public ActionResult ACompanyEdit(int CompanyKey, CompanyModel collection)
        {
            CompanyModel companyv = new CompanyModel();
            try
            {
                Int64 value = 0;
                value = __companyservice.CompanyEdit(collection);
                if (value == 0)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("companydefaultView","ACompanyManagement",new { CompanyKey = collection.CompanyKey});
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                    return RedirectToAction("ACompanyEdit");
                }
            }
            catch (Exception ex)
            {
                ViewData["Errormessage"] = ex.Message;
                TempData["Errormessage"] = ex.Message;
                IList<CompanyModel> lststate = null;

                //list of state
                lststate = __companyservice.GetAllStatee();
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
                return View(collection);
            }
        }

        //GET: Editcompany
        public ActionResult ACompanyDefaultEdit(int CompanyKey)
        {
            try
            {
                CompanyModel resources = null;
                resources = __companyservice.GetDataViewEditt(CompanyKey);
                return View(resources);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ACompanylist");
            }
        }

        [HttpPost]
        public ActionResult ACompanyDefaultEdit(int CompanyKey, CompanyModel collection)
        {
            CompanyModel companyv = new CompanyModel();
            try
            {
                Int64 value = 0;
                //CompanyModel resources = null;

                //resources = __companyservice.GetDataViewEditt(CompanyKey);
                //resources.Description = collection.Description;
                //resources.BidRequestResponseDays = collection.BidRequestResponseDays;
                //resources.BidSubmitDays = collection.BidSubmitDays;
                //resources.BidRequestAmount = collection.BidRequestAmount;
                
                value = __companyservice.CompanydefaultEdit(collection);
                if (value == 0)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("companydefaultView", "ACompanyManagement", new { CompanyKey = collection.CompanyKey });
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                    return RedirectToAction("companydefaultView", "ACompanyManagement", new { CompanyKey = collection.CompanyKey });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("companyv");
            }
        }

        //GET: Editcompany
        public ActionResult PrimarycontactEdit(int CompanyKey)
        {
            try
            {
                CompanyModel resources = null;
                resources = __companyservice.GetDataViewEditt(CompanyKey);
                return View(resources);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ACompanylist");
            }
        }

        [HttpPost]
        public ActionResult PrimarycontactEdit(int CompanyKey, CompanyModel collection)
        {
            CompanyModel companyv = new CompanyModel();
            try
            {
                Int64 value = 0;
                //CompanyModel resources = null;

                //resources = __companyservice.GetDataViewEditt(CompanyKey);
                //resources.PrimaryContact = collection.PrimaryContact;
                //resources.FirstName = collection.FirstName;
                //resources.Email = collection.Email;
                //resources.Work = collection.Work;
                //resources.Work2 = collection.Work2;
                //resources.CellPhone = collection.CellPhone;
                value = __companyservice.APrimarycontactEdit(collection);
                if (value == 0)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("companydefaultView","ACompanyManagement",new { CompanyKey = collection.CompanyKey});
                }
                else
                {
                    throw new Exception("Record has not updated.");
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(collection);
            }
        }

        // GET: Viewcompany
        public ActionResult companyView(int CompanyKey)
        {
            CompanyModel companyr = new CompanyModel();
            try
            {
                CompanyModel company = new CompanyModel();
                company = __companyservice.GetDataViewEditt(CompanyKey);
                if (company != null)
                {
                    return View(company);
                }
                else
                {
                    return View(companyr);
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(companyr);
            }
        }

        // GET: Viewcompany
        public ActionResult companydefaultView(int CompanyKey)
        {
            Session["ViewCompanyFromList"] = CompanyKey;
            CompanyModel companyr = new CompanyModel();
            try
            {
                CompanyModel company = new CompanyModel();
          
                company = __companyservice.GetDataViewEditt(CompanyKey);
                if (company != null)
                {
                    company.Description = company.Description.Replace("\r\n", "<br>");
                    return View(company);
                }
                else
                {
                    return View(companyr);
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(companyr);
            }
        }
        public ActionResult Deletee(int CompanyKey ,int ResourceKey)
        {
            Int64 Status = 0;
           
            Status = __companyservice.Removee(CompanyKey, ResourceKey);
            if (Status == 1)
            {
                TempData["Sucessmessage"] = "Record has been deleted successfully.";
                return RedirectToAction("ACompanylist");
            }
            else
            {
                TempData["Errormessage"] = "This company has associated properties or Users, so cannot be deleted";
                return RedirectToAction("companydefaultView", "ACompanyManagement", new { CompanyKey = Convert.ToString(Session["ViewCompanyFromList"]) });
            }
        }

        //property module:
        public JsonResult IndexrePropertyPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            try
            {
                int ResourceId = Convert.ToInt32(Session["resourceid"]);
                Int32 companyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
                IList<PropertyModel> itemList = null;
                itemList = __companyservice.SearchCompanyViewProperty(PageSize, PageIndex, Search.Trim(), Sort,companyKey);
                return Json(itemList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json("Exception: "+ ex.Message,JsonRequestBehavior.AllowGet);
            }
        }

        //Users module:
        public JsonResult IndexreUserPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            IList<PropertyModel> itemList = null;
            itemList = __companyservice.SearchUser(PageSize, PageIndex, Search.Trim(), Sort);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CompanySetting()

        {
            int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
       
            CompanyModel companyr = new CompanyModel();
            try
            {
                CompanyModel company = new CompanyModel();
                company = __companyservice.GetDataViewEditt(CompanyKey);
                if (company != null)
                {
                    return View(company);
                }
                else
                {
                    return View(companyr);
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(companyr);
            }
        }





        // GET: AddPMProperty
        public ActionResult APMPropertyAdd()
        {
            Session.Remove("PropertiesAddDocSession");

            IList<PropertyModel> lstManagerlist = null;
            IList<PropertyModel> lstcompanylist = null;
            IList<PropertyModel> lststate = null;
            //list of state
            var lststatus = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="Pending"},
              new SelectListItem{ Value="2",Text="Approve"},
              new SelectListItem{ Value="3",Text="Unapprove"},
            };
            ViewBag.lststatus = lststatus;

            //list of company
            //List<System.Web.Mvc.SelectListItem> lstcompany = new List<System.Web.Mvc.SelectListItem>();
            //lstcompanylist = __pMPropertiesService.GetAllCompany();
            //System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
            //sli1.Text = "--Please Select--";
            //sli1.Value = "0";
            //lstcompany.Add(sli1);
            //for (int i = 0; i < lstcompanylist.Count; i++)
            //{
            //    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
            //    sli.Value = Convert.ToString(lstcompanylist[i].CompanyKey);
            //    sli.Text = Convert.ToString(lstcompanylist[i].Company);
            //    lstcompany.Add(sli);
            //}
            //ViewBag.lstcompany = lstcompany;


            //list of state
            lststate = __pMPropertiesService.GetAllState();
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

            //list of Manager
            List<System.Web.Mvc.SelectListItem> lstmanager = new List<System.Web.Mvc.SelectListItem>();
            int ResourceId = Convert.ToInt32(Session["resourceid"]);
            int CompnyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
            lstManagerlist = __pMPropertiesService.GetPropertyManagerToAddProperty(ResourceId, CompnyKey);
            for (int i = 0; i < lstManagerlist.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Value = Convert.ToString(lstManagerlist[i].ResourceKey);
                sli.Text = Convert.ToString(lstManagerlist[i].Name);
                lstmanager.Add(sli);
            }
            ViewBag.lstmanager = lstmanager;

            string rootFolderPath = Server.MapPath("~/Document/Properties/propertiesImages/");
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            System.IO.DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Document/Properties/propertiesImages/"));
            foreach (FileInfo file in di.GetFiles())
            {
                string firstCharOfString = file.Name;
                string strModified = firstCharOfString.Substring(0, 4);
                if (strModified == Convert.ToString(ResourceKey))
                {
                    FileInfo currentFile = new FileInfo(rootFolderPath + firstCharOfString);
                    currentFile.Delete();
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult APMPropertyAdd(PropertyModel collection, HttpPostedFileBase[] files)
        {
            try
            {
                List<StringBuilder> PropertiesAddDocSession = new List<StringBuilder>();

                StringBuilder FileName = new StringBuilder();
                StringBuilder FileSize = new StringBuilder();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["PropertiesAddDocSession"])))
                {
                    PropertiesAddDocSession = (List<StringBuilder>)Session["PropertiesAddDocSession"];
                    if (PropertiesAddDocSession.Count > 0)
                    {
                        FileName = PropertiesAddDocSession[0];
                        FileSize = PropertiesAddDocSession[1];
                    }
                }

                collection.CompanyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
                collection.ResourceKey = Convert.ToString((Request.Form["ManagerChoosen"] == null ? "" : Request.Form["ManagerChoosen"])).Trim().Trim(',').Trim();
                var value = __pMPropertiesService.Insert(collection, FileName.ToString().Trim(','), FileSize.ToString().Trim(','));

                string rootFolderPath = Server.MapPath("~/Document/Properties/propertiesImages/");
                string destinationPath = Server.MapPath("~/Document/Properties/");
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                if (Directory.Exists(rootFolderPath))
                {
                    //var v = rootFolderPath.GetFiles();
                    //var firstCharOfString = StringInfo.GetNextTextElement(v, 0);

                    foreach (var file in new DirectoryInfo(rootFolderPath).GetFiles())
                    {
                        string firstCharOfString = file.Name;
                        string strModified = firstCharOfString.Substring(0, 4);
                        if (strModified == Convert.ToString(ResourceKey))
                        {
                            file.MoveTo($@"{destinationPath}\{+value + firstCharOfString}");
                        }
                    }
                }
                TempData["SuccessMessage"] = "Record has been inserted successfully.";

                return RedirectToAction("companydefaultView", "ACompanyManagement", new { @CompanyKey = Convert.ToString(Session["ViewCompanyFromList"]) });

            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View("APMPropertyAdd");
            }

        }

        public JsonResult PropertyRemoveAddedDocument(string fileName)
        {
            try
            {
                List<StringBuilder> PropertiesAddDocSession = new List<StringBuilder>();

                StringBuilder FileName = new StringBuilder();
                StringBuilder FileSize = new StringBuilder();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["PropertiesAddDocSession"])))
                {
                    PropertiesAddDocSession = (List<StringBuilder>)Session["PropertiesAddDocSession"];
                    if (PropertiesAddDocSession.Count > 0)
                    {
                        FileName = PropertiesAddDocSession[0];
                        FileSize = PropertiesAddDocSession[1];
                    }

                    StringBuilder newFileName = new StringBuilder();
                    StringBuilder newFileSize = new StringBuilder();
                    for (Int32 ind = 0; ind < FileName.ToString().Trim().Trim(',').Trim().Split(',').Length; ind++)
                    {
                        if (fileName.Trim() == FileName.ToString().Trim().Trim(',').Trim().Split(',')[ind].Trim())
                        {
                            try
                            {
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Document/Properties/"), "0 " + FileName.ToString().Trim().Trim(',').Trim().Split(',')[ind]));
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
                    if (PropertiesAddDocSession.Count > 0)
                    {
                        PropertiesAddDocSession[0] = (FileName);
                        PropertiesAddDocSession[1] = (FileSize);
                    }
                    else
                    {
                        PropertiesAddDocSession.Add(FileName);
                        PropertiesAddDocSession.Add(FileSize);
                    }
                }

                Session["PropertiesAddDocSession"] = PropertiesAddDocSession;
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Exception: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult PropertyAddDocumentPost()
        {
            try
            {
                List<StringBuilder> PropertiesAddDocSession = new List<StringBuilder>();

                StringBuilder FileName = new StringBuilder();
                StringBuilder FileSize = new StringBuilder();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["PropertiesAddDocSession"])))
                {
                    PropertiesAddDocSession = (List<StringBuilder>)Session["PropertiesAddDocSession"];
                    if (PropertiesAddDocSession.Count > 0)
                    {
                        FileName = PropertiesAddDocSession[0];
                        FileSize = PropertiesAddDocSession[1];
                    }
                }
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    FileName.Append(Path.GetFileName(file.FileName));
                    FileName.Append(",");
                    FileSize.Append(file.ContentLength);
                    FileSize.Append(",");

                    var fileName = Path.GetFileName(file.FileName);
                    string input = fileName;
                    string[] values = input.Split('.');

                    var checkext = values[1];

                    if (checkext == "png" || checkext == "jpg")
                    {
                        string unixTimestamp = Convert.ToString((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                        var a = "P";
                        var path = Path.Combine(Server.MapPath("~/Document/Properties/propertiesImages/"), ResourceKey + fileName);
                        file.SaveAs(path);
                    }

                    else
                    {
                        var path = Path.Combine(Server.MapPath("~/Document/Properties/propertiesImages/"), ResourceKey + fileName);
                        file.SaveAs(path);
                    }
                }

                if (PropertiesAddDocSession.Count > 0)
                {
                    PropertiesAddDocSession[0] = FileName;
                    PropertiesAddDocSession[1] = FileSize;
                }
                else
                {
                    PropertiesAddDocSession.Add(FileName);
                    PropertiesAddDocSession.Add(FileSize);
                }

                Session["PropertiesAddDocSession"] = PropertiesAddDocSession;
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Exception: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: ViewProperty
        public ActionResult APropertiesView(int PropertyKey)
        {
            try
            {

                IList<PropertyModel> lstManagerlist = null;
                PropertyModel resources = null;
                resources = __pMPropertiesService.GetDataViewEdit(PropertyKey);
                resources.BidRequestAmount = Math.Round(resources.BidRequestAmount, 2);
                resources.MinimumInsuranceAmount = Math.Round(resources.MinimumInsuranceAmount, 2);
                if (resources.Status == 1)
                {
                    resources.StatusValue = "Pending";
                }
                else if (resources.Status == 2)
                {
                    resources.StatusValue = "Approve";
                }
                else if (resources.Status == 3)
                {
                    resources.StatusValue = "Unapprove";
                }
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                //list of Manager
                List<System.Web.Mvc.SelectListItem> lstmanager = new List<System.Web.Mvc.SelectListItem>();
                int ResourceId = Convert.ToInt32(Session["resourceid"]);
                int CompnyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
                lstManagerlist = __pMPropertiesService.GetAllManager(ResourceId, 0, CompnyKey);
                for (int i = 0; i < lstManagerlist.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Value = Convert.ToString(lstManagerlist[i].ResourceKey);
                    sli.Text = Convert.ToString(lstManagerlist[i].Name);
                    lstmanager.Add(sli);
                }
                ViewBag.lstmanager = lstmanager;

                var a = resources.PropertyKey.ToString();
                IList<PropertyModel> Documentlist = null;
                Documentlist = __pMPropertiesService.GetbindDocument(PropertyKey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();

                string[] doc = new string[Documentlist.Count];
                for (int i = 0; i < Documentlist.Count; i++)
                {

                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "~/Document/Properties/" + a +" "+ Documentlist[i].FileName;
                    var path = Server.MapPath(imagelist);
                    string input = Documentlist[i].FileName;
                    string[] values = input.Split('.');
                    var checkext = values[1];
                    if (checkext == "png" || checkext == "jpg")
                    {
                        resources.checkimg = true;
                        doc[i] = imagelist;
                    }

                    else
                    {
                        resources.checkimg = false;
                    }
                }
                resources.img = doc;
                resources.ResourceKey = ResourceKey.ToString();
                resources.Description = resources.Description.Replace("\r\n", "<br>");
                return View(resources);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public JsonResult DocUpload()
        {
            try
            {
                if (Request.Form.Count > 0)
                {
                    int PropertyKey = 0;
                    PropertyKey = Convert.ToInt32(Request.Form["PropertyKey"].ToString());
                    StringBuilder strinbuilder = new StringBuilder();
                    StringBuilder strinbuilder1 = new StringBuilder();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        strinbuilder.Append(Path.GetFileName(file.FileName));
                        strinbuilder.Append(",");
                        strinbuilder1.Append(file.ContentLength);
                        strinbuilder1.Append(",");
                    }
                    strinbuilder.Remove(strinbuilder.Length - 1, 1);
                    strinbuilder1.Remove(strinbuilder1.Length - 1, 1);
                    bool value = false;
                    value = __companyservice.DocInsert(PropertyKey, strinbuilder.ToString(), strinbuilder1.ToString());

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Document/Properties/"), PropertyKey + " " + fileName);
                        file.SaveAs(path);
                    }
                }
                return Json(null);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult GetAllGroup(int PropertyKey)
        {
            try
            {
                IList<PropertyModel> lstManagerlist = null;
                lstManagerlist = __companyservice.GetAllManager(PropertyKey);
                return Json(lstManagerlist, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult bindDocument(int PropertyKey)
        {
            try
            {
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                IList<PropertyModel> Documentlist = null;
                Documentlist = __companyservice.GetbindDocument(PropertyKey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                string[] doc = new string[Documentlist.Count];
                
                for (int i = 0; i < Documentlist.Count; i++)
                {
                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "../Document/Properties/" + PropertyKey + ResourceKey + Documentlist[i].FileName;
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

        public ActionResult APMPropertyEdit(int PropertyKey)
        {
            try
            {
                IList<PropertyModel> lststate = null;

                //list of state
                var lststatus = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="Pending"},
              new SelectListItem{ Value="2",Text="Approve"},
              new SelectListItem{ Value="3",Text="Unapprove"},
            };
                ViewBag.lststatus = lststatus;
                //list of state
                lststate = __companyservice.GetAllState();
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
                PropertyModel resources = null;
                resources = __companyservice.GetDataViewEdit(PropertyKey);
                return View(resources);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PMPropertiesList");
            }
        }

        [HttpPost]
        public ActionResult APMPropertyEdit(PropertyModel collection)
        {
            try
            {
                bool value = false;
                value = __companyservice.PropertyEdit(collection);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been updated successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("APropertiesView", "ACompanyManagement", new { @PropertyKey = collection.PropertyKey });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                IList<PropertyModel> lststate = null;

                //list of state
                var lststatus = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="Pending"},
              new SelectListItem{ Value="2",Text="Approve"},
              new SelectListItem{ Value="3",Text="Unapprove"},
            };
                ViewBag.lststatus = lststatus;
                //list of state
                lststate = __companyservice.GetAllState();
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
                return View(collection);
            }
        }

        public ActionResult Updatemanager(int PropertyKey, string managername)
        {
            try
            {
                bool value = false;
                value = __companyservice.Updatemanager(PropertyKey, managername);
                return RedirectToAction("APropertiesView", new { PropertyKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("APropertiesView", new { PropertyKey });
            }
        }
        public JsonResult JsonUpdatemanager(int PropertyKey, string managername)
        {
            try
            {
                bool value = false;
                value = __companyservice.Updatemanager(PropertyKey, managername);
                return Json(new { APropertiesView = PropertyKey, data = value }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { APropertiesView = PropertyKey, data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ManagerDelete(int PropertyKey, string managername)
        {
            try
            {
                Boolean valuea = false;
                valuea = __companyservice.ManagerDelete(PropertyKey,Convert.ToInt32(managername));
                if (valuea)
                {
                    TempData["Errormessage"] = "Required One Manager";
                }
                
                return RedirectToAction("APropertiesView", new { PropertyKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("APropertiesView", new { PropertyKey });
            }
        }

        public ActionResult DocumentDelete(int PropertyKey, string Docname)
        {
            try
            {
                string Directory = Server.MapPath("~/Document/Properties/");
                string fileName = Docname;
                string path = Directory + PropertyKey + " " + Docname;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                bool value = false;
                value = __companyservice.DocumentDelete(PropertyKey, Docname);
                return RedirectToAction("APropertiesView", new { PropertyKey });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction("APropertiesView", new { PropertyKey });
            }
        }
        public ActionResult APMAcessEdit(int PropertyKey)
        {
            try
            {
                PropertyModel resources = null;
                IList<PropertyModel> lstManagerlist = null;

                //list of state
                List<System.Web.Mvc.SelectListItem> lstmanager = new List<System.Web.Mvc.SelectListItem>();
                lstManagerlist = __companyservice.GetAllManager(0);
                for (int i = 0; i < lstManagerlist.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Value = Convert.ToString(lstManagerlist[i].Groupkey);
                    sli.Text = Convert.ToString(lstManagerlist[i].GroupName);
                    lstmanager.Add(sli);
                }
                ViewBag.lstmanager = lstmanager;
                //list of Manager

                resources = __companyservice.GetDataViewEdit(PropertyKey);
                return View(resources);
            }
            catch
            {
                return RedirectToAction("PropertiesView", new { PropertyKey });
            }
        }

        public ActionResult Delete(Int32 PropertyKey)
        {
            try
            {
                bool value = false;
                value = __companyservice.Remove(PropertyKey);
                if (value == true)
                {
                    TempData["SuccessMessage"] = "Record has been deleted successfully.";

                    return RedirectToAction("companydefaultView", "ACompanyManagement", new { @CompanyKey = Convert.ToString(Session["ViewCompanyFromList"]) });

                    //TempData["Sucessmessage"] = "Record has been deleted successfully.";
                    //return RedirectToAction("companydefaultView", "ACompanyManagement", new { @CompanyKey = Convert.ToString(Session["ViewCompanyFromList"]) });
                }
                else
                {
                    TempData["Errormessage"] = "There is a pending bids for this property";
                    return RedirectToAction("companydefaultView", "ACompanyManagement", new { CompanyKey = Convert.ToString(Session["ViewCompanyFromList"]) });
                }
       

               

                //return RedirectToAction("companydefaultView");
            }
            catch
            {
                ViewData["Errormessage"] = "Error";
                return RedirectToAction("PMPropertiesList");
            }
        }

        //User MODULE:
        public JsonResult IndexreCUserPaging(int CompanyKey)
        {
            IList<ResourceModel> itemList = null;
            itemList = __companyservice.Searchcompany(CompanyKey);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdminStaffDirectoryAdd()
        {
            IList<StaffDirectoryModel> lststate = null;
            StaffDirectoryModel lstGroup = new StaffDirectoryModel();
            //list of state
            lststate = __companyservice.GetAllStateee();
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
            slii2.Text = "Property Manager";
            slii2.Value = "1105";
            list.Add(slii2);
            System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
            sli3.Text = "Company Admin";
            sli3.Value = "1104";
            list.Add(sli3);

            ViewBag.lst = list;

            lstGroup.GroupList = __companyservice.GetAllGroup().ToList();

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
        public ActionResult AdminStaffDirectoryAdd(StaffDirectoryModel staffDirectoryModel, HttpPostedFileBase UploadImage)
        {
            try
            {
                Int64 value = 0;
                string filename = "";
                staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
                staffDirectoryModel.Password = "12345";

                if (staffDirectoryModel.Statuss == true)
                {
                    staffDirectoryModel.Status = 101;
                }
                else
                {
                    staffDirectoryModel.Status = 1;
                }
                bool ErrorMsg = __companyservice.CheckDuplicatedEmail(staffDirectoryModel.Email);
                if (ErrorMsg != true)
                {
                    if (UploadImage != null)
                    {
                        filename = Path.GetFileName(UploadImage.FileName);
                        staffDirectoryModel.FileName = filename;
                        staffDirectoryModel.FileSize = UploadImage.ContentLength;
                    }
                    if (!string.IsNullOrEmpty(staffDirectoryModel.GroupId)) {
                        staffDirectoryModel.GroupId = staffDirectoryModel.GroupId.Trim().Trim(',').Trim();
                    }
                    value = __companyservice.ACompanyStaffInsert(staffDirectoryModel);
                    if (UploadImage != null)
                    {
                        //string path = Path.Combine(Server.MapPath("~/Images/StaffDirectory/"), value + " " + filename);
                        string path = Path.Combine(Server.MapPath("~/Documents/Users/"), value + " " + filename);
                        UploadImage.SaveAs(path);
                    }
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Record has been inserted successfully.";
                        return RedirectToAction("CompanyDefaultView","ACompanyManagement",new { CompanyKey = staffDirectoryModel.CompanyKey });
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
                    lststate = __companyservice.GetAllStateee();
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

                    staffDirectoryModel.GroupList = __companyservice.GetAllGroup().ToList();

                    List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();
                    System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                    sli1.Text = "--Please Select--";
                    sli1.Value = "0";
                    list.Add(sli1);
                    System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
                    slii2.Text = "Property Manager";
                    slii2.Value = "1105";
                    list.Add(slii2);
                    System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
                    sli3.Text = "Company Admin";
                    sli3.Value = "1104";
                    list.Add(sli3);

                    ViewBag.lst = list;

                    ViewBag.ErrorMessage = "Email already exits.";

                    return View(staffDirectoryModel);
                }
            }
            catch (Exception ex)
            {
                return View("StaffDirectoryAdd");
            }
        }

        public JsonResult GetAllGroup()
        {
            List<StaffDirectoryModel> lstuser = null;
            lstuser = __companyservice.GetAllGroup();
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGroupEditUser()
        {
            List<StaffDirectoryModel> lstuser = null;
            lstuser = __companyservice.GetAllGroup();
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGroupForAddStaff()
        {
            List<StaffDirectoryModel> lstuser = null;
            lstuser = __companyservice.GetAllGroup();
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGroupManagers(int PropertyKey)
        {
            try
            {
                IList<PropertyModel> lstManagerlist = null;
                int ResourceId = Convert.ToInt32(Session["resourceid"]);
                int CompnyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
                lstManagerlist = __pMPropertiesService.GetAllManager(ResourceId, PropertyKey, CompnyKey);
                return Json(lstManagerlist, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(null);
            }
        }

        public ActionResult AStaffDirectoryView(int ResourceKey)
        {
            List<StaffDirectoryModel> staffDirectoryList = null;
            StaffDirectoryModel staffDirectory = new StaffDirectoryModel();
            staffDirectoryList = __companyservice.GetDataViewEdittt(ResourceKey);
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

            staffDirectory.UserName = staffDirectoryList[0].Email;
            staffDirectory.GroupKey = staffDirectoryList[0].GroupKey;
            staffDirectory.Password = staffDirectoryList[0].Password;
            for (int i = 0; i < staffDirectoryList.Count; i++)
            {
                staffDirectory.GroupResponseId += staffDirectoryList[i].GroupId + ",";
            }
          
            return View(staffDirectory);
        }

        public ActionResult AStaffDirectoryEditStaff(int ResourceKey)
        {
            IList<StaffDirectoryModel> lststate = null;
            StaffDirectoryModel lstGroup = new StaffDirectoryModel();
            //list of state
            lststate = __companyservice.GetAllStateee();
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
            staffDirectoryList = __companyservice.GetDataViewEdittt(ResourceKey);

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
            staffDirectory.PrimaryContact = staffDirectoryList[0].PrimaryContact;
            staffDirectory.Description = staffDirectoryList[0].Description;
            if (staffDirectoryList[0].Status == 2 || staffDirectoryList[0].Status == 101) {
                staffDirectory.Status = 101;
            }
            return View(staffDirectory);
        }

        public ActionResult AStaffDirectoryEditGroup(int ResourceKey)
        {
            List<StaffDirectoryModel> staffDirectoryList = null;
            StaffDirectoryModel staffDirectory = new StaffDirectoryModel();
            staffDirectoryList = __companyservice.GetDataViewEdittt(ResourceKey);

            for (int i = 0; i < staffDirectoryList.Count; i++)
            {
                staffDirectory.GroupResponseId += staffDirectoryList[i].GroupId + ",";
            }
            return View(staffDirectory);
        }

        public ActionResult AStaffDirectoryEditUser(int ResourceKey)
        {
            List<StaffDirectoryModel> staffDirectoryList = null;
            StaffDirectoryModel staffDirectory = new StaffDirectoryModel();
            staffDirectoryList = __companyservice.GetDataViewEdittt(ResourceKey);

            staffDirectory.ResourceKey = staffDirectoryList[0].ResourceKey;
            staffDirectory.UserName = staffDirectoryList[0].UserName;
            staffDirectory.Password = staffDirectoryList[0].Password;
            staffDirectory.FileName = staffDirectoryList[0].FileName;

            return View(staffDirectory);
        }

        [HttpPost]
        public ActionResult AStaffDirectoryEditStaff(int ResourceKey, StaffDirectoryModel staffDirectoryModel)
        {
            try
            {
                Int64 value = 0;
                bool ErrorMsg = false;
                staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
                if (staffDirectoryModel.OldEmail != staffDirectoryModel.Email) {
                    ErrorMsg = __companyservice.CheckDuplicatedEmail(staffDirectoryModel.Email);
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
                    value = __companyservice.StaffDirectoryEditStaff(staffDirectoryModel);
                    if (value == 0)
                    {
                        TempData["SuccessMessage"] = "Record has been updated successfully.";
                        return RedirectToAction("AStaffDirectoryView", "ACompanyManagement", new { @ResourceKey = staffDirectoryModel.ResourceKey });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error";
                        return View(staffDirectoryModel);
                    }
                }
                else
                {
                    IList<StaffDirectoryModel> lststate = null;
                    //list of state
                    lststate = __companyservice.GetAllStateee();
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

                    staffDirectoryModel.GroupList = __companyservice.GetAllGroup().ToList();

                    List<System.Web.Mvc.SelectListItem> list = new List<System.Web.Mvc.SelectListItem>();
                    System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                    sli1.Text = "--Please Select--";
                    sli1.Value = "0";
                    list.Add(sli1);
                    System.Web.Mvc.SelectListItem slii2 = new System.Web.Mvc.SelectListItem();
                    slii2.Text = "Property Manager";
                    slii2.Value = "1105";
                    list.Add(slii2);
                    System.Web.Mvc.SelectListItem sli3 = new System.Web.Mvc.SelectListItem();
                    sli3.Text = "Company Admin";
                    sli3.Value = "1104";
                    list.Add(sli3);

                    ViewBag.lst = list;

                    ViewBag.ErrorMessage = "Email already exits.";

                    return View(staffDirectoryModel);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                IList<StaffDirectoryModel> lststate = null;
                StaffDirectoryModel lstGroup = new StaffDirectoryModel();
                //list of state
                lststate = __companyservice.GetAllStateee();
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

                return View(staffDirectoryModel);
            }
        }

        [HttpPost]
        public ActionResult AStaffDirectoryEditGroup(int ResourceKey, StaffDirectoryModel staffDirectoryModel)
        {
            try
            {
                Int64 value = 0;
                if (!string.IsNullOrEmpty(staffDirectoryModel.GroupId)) {
                    staffDirectoryModel.GroupId = staffDirectoryModel.GroupId.Trim().Trim(',').Trim();
                }
                staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
                value = __companyservice.StaffDirectoryEditGroup(staffDirectoryModel);
                if (value == 0)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("AStaffDirectoryView", "ACompanyManagement", new { @ResourceKey = staffDirectoryModel.ResourceKey });
                }
                else
                {
                    TempData["ErrorMessage"] = "Error";
                    return View(staffDirectoryModel);
                }
            }
            catch (Exception ex)
            {
                return View(staffDirectoryModel);
            }
        }

        [HttpPost]
        public ActionResult AStaffDirectoryEditUser(int ResourceKey, StaffDirectoryModel staffDirectoryModel, HttpPostedFileBase UploadImage)
        {
            try
            {
                Int64 value = 0;
                staffDirectoryModel.CompanyKey = Convert.ToInt32(Session["ViewCompanyFromList"]);
                string filename = "";

                if (UploadImage != null)
                {
                    filename = Path.GetFileName(UploadImage.FileName);
                    staffDirectoryModel.FileName = filename;
                    staffDirectoryModel.FileSize = UploadImage.ContentLength;
                }
                value = __companyservice.StaffDirectoryEditUser(staffDirectoryModel);
                if (UploadImage != null)
                {
                    //string path = Path.Combine(Server.MapPath("~/Images/StaffDirectory/"), value + " " + filename);
                    string path = Path.Combine(Server.MapPath("~/Documents/Users/"), value + " " + filename);

                    UploadImage.SaveAs(path);
                }
                if (value == 0)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("AStaffDirectoryView", "ACompanyManagement", new { @ResourceKey = staffDirectoryModel.ResourceKey });
                }
                else
                {
                    TempData["ErrorMessage"] = "Error";
                    return View(staffDirectoryModel);
                }                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(staffDirectoryModel);
            }
        }

        public JsonResult ResetPassword(int ResourceKey)
        {
            bool Status = false;

            try
            {
                Status = __companyservice.ResetPassword(ResourceKey);
            }
            catch (Exception ex)
            {
                return Json("Exception: "+ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ACompanyStaffDelete(Int32 ResourceKey)
        {
            bool Status = false;
            Status = __companyservice.RemoveStaffDirecroty(ResourceKey);
            if (Status == true)
            {
                TempData["SuccessMessage"] = "Record has been deleted successfully.";
                return RedirectToAction("companydefaultView", "ACompanyManagement", new { CompanyKey = Convert.ToString(Session["ViewCompanyFromList"]) });
            }
            else
            {
                ViewData["Errormessage"] = "Error";
                return RedirectToAction("AStaffDirectoryEditStaff", "ACompanyManagement", new { ResourceKey = Convert.ToString(ResourceKey) });
            }
        }

        public JsonResult Deleteee(int id)
        {
            bool Status = false;
            Status = __companyservice.Removeee(id);
            if (Status == true)
            {
                TempData["Sucessmessage"] = "Record has been deleted successfully.";
            }
            else
            {
                ViewData["Errormessage"] = "Error";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
    }
}