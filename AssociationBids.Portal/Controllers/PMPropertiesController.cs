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

namespace AssociationBids.Portal.Controllers
{
    public class PMPropertiesController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IResourceService _resourceservice;

        private readonly AssociationBids.Portal.Service.Base.Interface.IPMPropertiesService __pMPropertiesService;

        public PMPropertiesController(AssociationBids.Portal.Service.Base.Interface.IPMPropertiesService PMPropertiesService, AssociationBids.Portal.Service.Base.IResourceService resourceService)
        {
            this.__pMPropertiesService = PMPropertiesService;
            this._resourceservice = resourceService;
        }
        //public PMPropertiesController(IResourceService resourceService)
        //{
        //    this._resourceservice = resourceService;
        //}

        // GET: Property
        public ActionResult PMPropertiesList()
        {
            return View();
        }
        public JsonResult IndexreUserPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            if (Search == null)
                Search = "";

            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            IList<PropertyModel> itemList = null;
            itemList = __pMPropertiesService.SearchUser(PageSize, PageIndex, Search.Trim(), Sort, ResourceKey);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexvendorBidREquestPaging(Int64 PageSize, Int64 PageIndex, String Sort, string PropertyKey)
        {
            //string CompanyKey = "";
            //CompanyKey = Convert.ToString(Session["CompanyKey"]);
            IList<PropertyModel> itemList = null;
            itemList = __pMPropertiesService.SearchBidrequest(PageSize, PageIndex, Sort, PropertyKey);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexvendorWorkOrderPaging(Int64 PageSize, Int64 PageIndex, String Sort, string PropertyKey)
        {
            //string CompanyKey = "";
            //CompanyKey = Convert.ToString(Session["CompanyKey"]);
            IList<PropertyModel> itemList = null;
            itemList = __pMPropertiesService.SearchWorkOrder(PageSize, PageIndex, Sort, PropertyKey);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PMPropertiesBidRequestView(int BidRequestKey)
        {
            try
            {
                PropertyModel resources = null;
                resources = __pMPropertiesService.GetBidDataForProperties(BidRequestKey);
                
                if (resources.sstartddate == "01/01/00")
                {
                    resources.sstartddate = "";
                }
                Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
                int Modulekey = 100;
                IList<BidRequestModel> Documentlist = null;
                IList<BidRequestModel> Documentlist1 = null;

                Documentlist = __pMPropertiesService.GetbindDocumentBid(BidRequestKey, Modulekey);
                //Documentlist1 = __vendorservice.GetbindDocument1(BidRequestKey, 107);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                string[] doc = new string[Documentlist.Count];
                //string[] doc1 = new string[Documentlist1.Count];
                var a = BidRequestKey;
                for (int i = 0; i < Documentlist.Count; i++)
                {
                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "~/Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
                    var path = Server.MapPath(imagelist);
                    string input = Documentlist[i].FileName;
                    string[] values = input.Split('.');
                    var checkext = values[1];
                    if (checkext == "png" || checkext == "jpg")
                    {
                        resources.checkimg = true;
                        string imagelist1 = "../Document/Properties/" + BidRequestKey + " " + Documentlist[i].FileName;
                        doc[i] = imagelist1;
                    }

                    else
                    {
                        resources.checkimg = false;
                    }
                }

                resources.img = doc;

                return View(resources);

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        // GET: AddPMProperty
        public ActionResult PMPropertyAdd(PropertyModel collection)
        {
            try
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

            List<System.Web.Mvc.SelectListItem> lstcompany = new List<System.Web.Mvc.SelectListItem>();
            lstcompanylist = __pMPropertiesService.GetAllCompany();
            System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
            sli1.Text = "--Please Select--";
            sli1.Value = "0";
            lstcompany.Add(sli1);
            for (int i = 0; i < lstcompanylist.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Value = Convert.ToString(lstcompanylist[i].CompanyKey);
                sli.Text = Convert.ToString(lstcompanylist[i].Company);
                lstcompany.Add(sli);
            }
            ViewBag.lstcompany = lstcompany;


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
            int CompnyKey = Convert.ToInt32(Session["CompanyKey"]);
            lstManagerlist = __pMPropertiesService.GetAllManager(ResourceId, 0, CompnyKey);
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
            }
            catch (Exception)
            {
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult PMPropertyAdd(PropertyModel collection, HttpPostedFileBase[] files)
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
                //for (Int32 ind = 0; ind < FileName.ToString().Trim(',').Split(',').Length; ind++) {
                //    if (!string.IsNullOrEmpty(FileName.ToString().Trim(',').Split(',')[ind])) {
                //        collection.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                //        //collection.ResourceKey = Convert.ToString(Session["resourceid"]);
                //        collection.ResourceKey = Convert.ToString((Request.Form["ManagerChoosen"] == null ? "" : Request.Form["ManagerChoosen"])).Trim().Trim(',').Trim();
                //        var value = __pMPropertiesService.Insert(collection, FileName.ToString().Trim(',').Split(',')[ind], FileSize.ToString().Trim(',').Split(',')[ind]);
                //    }
                //}
                collection.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                if (collection.ResourceKey==null)
                {
                    collection.ResourceKey = ResourceKey.ToString();
                }
                else
                {
                    collection.ResourceKey = Convert.ToString((Request.Form["ManagerChoosen"] == null ? "" : Request.Form["ManagerChoosen"])).Trim().Trim(',').Trim();

                }
                var value = __pMPropertiesService.Insert(collection, FileName.ToString().Trim(','), FileSize.ToString().Trim(','));
            
                string rootFolderPath = Server.MapPath("~/Document/Properties/propertiesImages/" ) ;
                string destinationPath = Server.MapPath("~/Document/Properties/");
             
                if (Directory.Exists(rootFolderPath))
                {
                    //var v = rootFolderPath.GetFiles();
                    //var firstCharOfString = StringInfo.GetNextTextElement(v, 0);
                   
                    foreach (var file in new DirectoryInfo(rootFolderPath).GetFiles())
                    {
                        string firstCharOfString = file.Name;
                        string strModified = firstCharOfString.Substring(0, 5);
                        if (strModified == Convert.ToString(ResourceKey))
                        {
                            file.MoveTo($@"{destinationPath}\{+value + firstCharOfString}");
                        }
                    }
                }       
                

                    TempData["Errormessage"] = "Record has been inserted successfully.";


                return RedirectToAction("PMPropertiesList");

            }
            catch (Exception ex)
            {
               return View("PMPropertiesList");
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
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Document/Properties/"), "0" + FileName.ToString().Trim().Trim(',').Trim().Split(',')[ind]));
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
                    string [] values = input.Split('.');

                    var checkext = values[1];

                    if (checkext =="png" || checkext == "jpg")
                    {
                        string unixTimestamp = Convert.ToString((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                        var a ="P";
                        var path = Path.Combine(Server.MapPath("~/Document/Properties/propertiesImages/"), ResourceKey  + fileName );
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
        public ActionResult PropertiesView(int PropertyKey, PropertyModel collection)

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
                int CompnyKey = Convert.ToInt32(Session["CompanyKey"]);
                lstManagerlist = __pMPropertiesService.GetAllManager(ResourceId, 0, CompnyKey);
                for (int i = 0; i < lstManagerlist.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Value = Convert.ToString(lstManagerlist[i].ResourceKey);
                    sli.Text = Convert.ToString(lstManagerlist[i].Name);
                    lstmanager.Add(sli);
                }
                ViewBag.lstmanager = lstmanager;
                ViewBag.PropertyKey = PropertyKey;
                var a = resources.PropertyKey.ToString();


                IList<PropertyModel> Documentlist = null;
                Documentlist = __pMPropertiesService.GetbindDocument(PropertyKey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();

                string[] doc = new string[Documentlist.Count];
                for (int i = 0; i < Documentlist.Count; i++)
                {                   
                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "~/Document/Properties/"+a+ ResourceKey +Documentlist[i].FileName;
                    if(Text.Contains(PropertyKey+" "))
                        imagelist = "~/Document/Properties/" + Documentlist[i].FileName;
                    var path= Server.MapPath(imagelist);                  
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
                resources.RKey = ResourceKey;
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
                    var a  = Convert.ToString(Session["resourceid"]);
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
                    value = __pMPropertiesService.DocInsert(PropertyKey, strinbuilder.ToString(), strinbuilder1.ToString());

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Document/Properties/"), +PropertyKey+ a+fileName);
                        file.SaveAs(path);

                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }


                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetAllGroup(int PropertyKey)
        {
            try
            {
                IList<PropertyModel> lstManagerlist = null;
                int ResourceId = Convert.ToInt32(Session["resourceid"]);
                int CompnyKey = Convert.ToInt32(Session["CompanyKey"]);
                lstManagerlist = __pMPropertiesService.GetAllManager(ResourceId, PropertyKey, CompnyKey);
                return Json(lstManagerlist, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(null);
            }
        }

       

        public JsonResult bindBidRequestDocument(int BidRequestKey, int Modulekey)
        {
            try
            {
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                IList<BidRequestModel> Documentlist = null;
                Documentlist = __pMPropertiesService.GetbindDocumentBid(BidRequestKey, Modulekey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                string[] doc = new string[Documentlist.Count];
                for (int i = 0; i < Documentlist.Count; i++)
                {
                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "../Document/Properties/" + "BidRequestKeyTemp " + Documentlist[i].FileName;
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

        public JsonResult GetMangerName()
        {
            try
            {
                IList<PropertyModel> lstManagerlist = null;
                int PropertyKey = 0;
                int ResourceId = Convert.ToInt32(Session["resourceid"]);
                int CompnyKey = Convert.ToInt32(Session["CompanyKey"]);
                lstManagerlist = __pMPropertiesService.GetManagerForAdd(ResourceId, PropertyKey, CompnyKey);
                return Json(lstManagerlist, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(null);
            }
        }

        public JsonResult checkPortal()
        {

            try
            {

                int Portalkey = Convert.ToInt32(Session["Portalkey"]);
                if (Portalkey == 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else if (Portalkey == 1)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else  if (Portalkey == 3)
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

        public JsonResult SearchVendorByBidRequest()
        {
            List<PropertyModel> lstVendor = null;
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());
            lstVendor = __pMPropertiesService.SearchVendorByBidRequest(BidRequestKey, modulekey, Convert.ToInt32(Session["resourceid"]));
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult bindDocument(int PropertyKey)
        {
            try
            {

                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                IList<PropertyModel> Documentlist = null;
                Documentlist = __pMPropertiesService.GetbindDocument(PropertyKey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();

                string[] doc = new string[Documentlist.Count];
                for (int i = 0; i < Documentlist.Count; i++)
                {

                    var Text = Convert.ToString(Documentlist[i].FileName);
                    string imagelist = "../Document/Properties/" + PropertyKey + ResourceKey + Documentlist[i].FileName;
                    if (Text.Contains(PropertyKey + " "))
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

        public ActionResult PMPropertyEdit(int PropertyKey)
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
                PropertyModel resources = null;
                resources = __pMPropertiesService.GetDataViewEdit(PropertyKey);
                return View(resources);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PMPropertiesList");
            }
        }

        [HttpPost]
        public ActionResult PMPropertyEdit(PropertyModel collection)
        {

            try
            {
                bool value = false;
                value = __pMPropertiesService.PropertyEdit(collection);
                if (value == true)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("PropertiesView", new { collection.PropertyKey });
                }
                else
                {
                    ViewData["ErrorMessage"] = "Error";
                    return RedirectToAction("PropertiesView", new { collection.PropertyKey });
                }
               

              
            }
            catch (Exception ex)
            {
                return RedirectToAction("PMPropertiesList");

            }

        }

        public ActionResult Updatemanager(PropertyModel collection, int PropertyKey, string managername)
        {
            try
            {

                bool value = false;
                collection.ResourceKey = Convert.ToString(Session["resourceid"]);
                value = __pMPropertiesService.Updatemanager(collection, PropertyKey, managername);
                return RedirectToAction("PropertiesView", new { PropertyKey });
            }
            catch (Exception ex)
            {

                return RedirectToAction("PropertiesView", new { PropertyKey });


            }


        }
        public JsonResult JsonUpdatemanager(int PropertyKey, string managername)
        {
            try
            {
                PropertyModel collection = new PropertyModel();
                bool value = false;
                collection.ResourceKey = Convert.ToString(Session["resourceid"]);
                value = __pMPropertiesService.Updatemanager(collection, PropertyKey, managername);
                return Json(new{PropertiesView=PropertyKey,data=value },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { PropertiesView = PropertyKey, value = "0" }, JsonRequestBehavior.AllowGet);


            }


        }

        public ActionResult ManagerDelete(PropertyModel collection, int PropertyKey, string managername,string ResourceKey)
        {
            try
            {
                PropertyModel resources = null;
                //int valuea = 0;
                resources = __pMPropertiesService.checkmanager(managername);
                string resourceKey = resources.ResourceKey;
                //string ResourceKey = __pMPropertiesService.checkmanager(managername);
                bool value = false;

                value = __pMPropertiesService.ManagerDelete(PropertyKey, ResourceKey);

            }
            catch (Exception ex)
            {

                return RedirectToAction("PropertiesView", new { PropertyKey });


            }
            return RedirectToAction("PropertiesView", new { PropertyKey });

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
                value = __pMPropertiesService.DocumentDelete(PropertyKey, Docname);
                return RedirectToAction("PropertiesView", new { PropertyKey });



            }
            catch (Exception ex)
            {

                return RedirectToAction("PropertiesView", new { PropertyKey });


            }


        }
        public ActionResult PMAcessEdit(int PropertyKey)

        {

            try
            {
                PropertyModel resources = null;
                IList<PropertyModel> lstManagerlist = null;


                //list of state
                //list of Manager
                List<System.Web.Mvc.SelectListItem> lstmanager = new List<System.Web.Mvc.SelectListItem>();
                int ResourceId = Convert.ToInt32(Session["resourceid"]);
                int CompnyKey = Convert.ToInt32(Session["CompanyKey"]);
                lstManagerlist = __pMPropertiesService.GetAllManager(ResourceId, PropertyKey, CompnyKey);
                for (int i = 0; i < lstManagerlist.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Value = Convert.ToString(lstManagerlist[i].ResourceKey);
                    sli.Text = Convert.ToString(lstManagerlist[i].Name);
                    lstmanager.Add(sli);
                }
                ViewBag.lstmanager = lstmanager;
                //list of Manager


                resources = __pMPropertiesService.GetDataViewEdit(PropertyKey);
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
                value = __pMPropertiesService.Remove(PropertyKey);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been deleted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("PMPropertiesList");
            }

            catch
            {
                ViewData["Errormessage"] = "Error";
                return RedirectToAction("PMPropertiesList");
            }

        }

        public ActionResult ProfileView()
        {
            ResourceModel resourcemodel = new ResourceModel();
            IList<ResourceModel> lststate = null;

            int userid = Convert.ToInt32(Session["resourceid"]);
            if (userid == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {

                resourcemodel = _resourceservice.GetDataViewEdit(userid);
                return View(resourcemodel);

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(resourcemodel);

            }
        }

        public ActionResult Profile()
        {
            ResourceModel resourcemodel = new ResourceModel();
            IList<ResourceModel> lststate = null;

            int userid = Convert.ToInt32(Session["resourceid"]);
            if (userid == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                lststate = _resourceservice.GetAllState();
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

                resourcemodel = _resourceservice.GetDataViewEdit(userid);
                return View(resourcemodel);

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(resourcemodel);

            }
        }

        [HttpPost]
        public ActionResult Profile(AssociationBids.Portal.Model.ResourceModel modal, HttpPostedFileBase file)
        {
            IList<ResourceModel> lststate = null;
            try
            {
                bool value = false;
                //value = _resourceservice.Edit(modal);

                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(Request.Files[i].FileName))
                        {
                            if (Request.Files.Keys[i] == "ProfilePicture")
                            {
                                string ImageName = Request.Files[i].FileName;
                                Int64 ImageLength = Request.Files[i].ContentLength;
                                string Title = "Profile Image";
                                string Controller = "PMProperties";
                                string Action = "Profile";

                                if (!Directory.Exists(Server.MapPath("~/Document/Resources/" + modal.ResourceKey.ToString())))
                                {
                                    Directory.CreateDirectory(Server.MapPath("~/Document/Resources/" + modal.ResourceKey.ToString()));
                                }

                                _resourceservice.PropertyMangerProfileImage(modal.ResourceKey, Title, Controller, Action, ImageName, ImageLength);
                                Request.Files[i].SaveAs(Server.MapPath("~/Document/Resources/" + modal.ResourceKey.ToString() + "/" + Request.Files[i].FileName));
                                try
                                {
                                    string ProfileImagePath = Server.MapPath("~/Document/Resources/" + modal.ResourceKey.ToString() + "/" + Request.Files[i].FileName);
                                    if (System.IO.File.Exists(ProfileImagePath))
                                    {
                                        byte[] imgdata = System.IO.File.ReadAllBytes(ProfileImagePath);
                                        Session["ProfileImageBase64"] = "data:image/png;base64," + Convert.ToBase64String(imgdata, 0, imgdata.Length);
                                    }
                                    else
                                    {
                                        ProfileImagePath = Server.MapPath("~/Content/themes/assets/images/users/avatars/19.png");
                                        if (System.IO.File.Exists(ProfileImagePath))
                                        {
                                            byte[] imgdata = System.IO.File.ReadAllBytes(ProfileImagePath);
                                            Session["ProfileImageBase64"] = "data:image/png;base64," + Convert.ToBase64String(imgdata, 0, imgdata.Length);
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            }
                        }
                    }
                }

                value = _resourceservice.PropertyMangerProfileEdit(modal);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been updated successfully.";
                }
                else
                {
                    TempData["Errormessage"] = "Error";
                }
                return RedirectToAction("ProfileView");
            }
            catch (Exception ex)
            {
                lststate = _resourceservice.GetAllState();
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
                TempData["Errormessage"] = "Exception: " + ex.Message;
                return View(modal);
            }
        }

        public JsonResult SavePassword(string OldPassword, string NewPassword)
        {

            Int32 userid = Convert.ToInt32(Session["resourceid"]);

            try
            {
                string retVal = _resourceservice.SaveProfilePassword(userid, OldPassword, NewPassword);

                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return Json("Exception: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}