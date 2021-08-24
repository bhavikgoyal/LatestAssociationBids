using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AssociationBids.Portal.Controllers
{
    public class PMVendorController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.Interface.IPMVendorService __vendorservice;

        public PMVendorController(AssociationBids.Portal.Service.Base.Interface.IPMVendorService vendorService)
        {
            this.__vendorservice = vendorService;
        }

        // GET: Vendor
        public ActionResult PMVendorlist()
        {
            IList<VendorModel> lstservice = null;
            IList<VendorModel> lstproperty = null;

            //Service

            lstservice = __vendorservice.GetAllService();
            List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
            sli1.Text = "--Please Select--";
            sli1.Value = "0";
            lstservicelist.Add(sli1);
            for (int i = 0; i < lstservice.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                lstservicelist.Add(sli);
            }
            ViewBag.lstservice = lstservicelist;

            ////Property
            ///
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            lstproperty = __vendorservice.GetAllProperty(resourcekey);
            List<System.Web.Mvc.SelectListItem> lstpropertylist = new List<System.Web.Mvc.SelectListItem>();

            for (int i = 0; i < lstproperty.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstproperty[i].Title);
                sli.Value = Convert.ToString(lstproperty[i].PropertyKey);
                lstpropertylist.Add(sli);
            }
            ViewBag.lstproperty = lstpropertylist;
            return View();
        }
        public JsonResult IndexvendorPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort, string service, string checkstar,string Invited,string Duplicate)
        {
            int resourcekey = Convert.ToInt32(Session["resourceid"]);
            IList<VendorModel> itemList = null;
            itemList = __vendorservice.SearchVendor(PageSize, PageIndex, Search.Trim(), Sort, resourcekey, service, checkstar, Invited, Duplicate);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexvendorBidREquestPaging(Int64 PageSize, Int64 PageIndex, String Sort, string CompanyKey)
        {
            //string CompanyKey = "";
            //CompanyKey = Convert.ToString(Session["CompanyKey"]);
            IList<VendorModel> itemList = null;
            itemList = __vendorservice.SearchBidrequest(PageSize, PageIndex, Sort, CompanyKey);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexvendorWorkOrderPaging(Int64 PageSize, Int64 PageIndex, String Sort, string CompanyKey)
        {
            //string CompanyKey = "";
            //CompanyKey = Convert.ToString(Session["CompanyKey"]);
            IList<VendorModel> itemList = null;
            itemList = __vendorservice.SearchWorkOrder(PageSize, PageIndex, Sort, CompanyKey);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexvendorFeedBackPaging(Int64 PageSize, Int64 PageIndex, String Sort, string CompanyKey)
            {
          
            IList<VendorModel> itemList = null;
            itemList = __vendorservice.SearchFeedbackvendor(PageSize, PageIndex, Sort,  CompanyKey);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PMVendorBidRequestDetails(int BidRequestKey, int ComapanyKey)
        {
            try
            {
                VendorModel resources = null;
                
                resources = __vendorservice.GetBidDataForProperties(BidRequestKey, ComapanyKey);
                resources.CompanyKey = ComapanyKey;
                if (resources.sstartddate == "01/01/00")
                {

                    resources.sstartddate = "";

                }
                Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
                int Modulekey = 100;
                IList<VendorModel> Documentlist = null;
                IList<VendorModel> Documentlist1 = null;

                Documentlist = __vendorservice.GetbindDocument(BidRequestKey, Modulekey);
                Documentlist1 = __vendorservice.GetbindDocument1(BidRequestKey, 107);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                string[] doc = new string[Documentlist.Count];
                string[] doc1 = new string[Documentlist1.Count];
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

                resources.Description = resources.Description.Replace("\r\n", "<br>");
                return View(resources);
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }
        public JsonResult SearchVendorByBidRequest()
        {
            List<VendorModel> lstVendor = null;
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());
            lstVendor = __vendorservice.SearchVendorByBidRequest(BidRequestKey, modulekey, Convert.ToInt32(Session["resourceid"]));
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult bindDocument()
        {
            try
            {
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                IList<VendorModel> Documentlist = null;
                int Modulekey = 100;
                int BidRequestKey = 0;
                if (Request.Form["BidRequestKey"] != null)
                    Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
                Documentlist = __vendorservice.GetbindDocument(BidRequestKey, Modulekey);
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
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        // GET: AddVendor
        public ActionResult PMVendorAdd()
        {
            try
            {
        
                 IList<VendorModel> lststate = null;
                //list of state
                lststate = __vendorservice.GetAllState();
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
              
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View();
            }

            return View();
        }

        [HttpPost]
        public ActionResult PMVendorAdd(VendorModel collection)
        {
            try
            {
                Int64 value = 0;
                int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                bool ErrorMsg = __vendorservice.CheckDuplicatedEmail(collection.Email);
                if (ErrorMsg != true)
                {
                    value = __vendorservice.Insert(collection, ResourceKey);
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Invitation sent.";
                    }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                    }


                    return RedirectToAction("PMVendorlist");
                }

                else
                {
                    IList<VendorModel> lststate = null;
                    //list of state
                    lststate = __vendorservice.GetAllState();
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
                    ViewBag.ErrorMessage = "Email are Already Exists";

                    return View(collection);
                }

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                //return View("PMVendorlist");
                return null;
            }
        }

        // GET: EditVendor
        public ActionResult PMVendorEdit(int CompanyKey)
        {
            try
            {
                IList<VendorModel> lststate = null;
                IList<VendorModel> lstservice = null;

                //Service

                lstservice = __vendorservice.GetAllService();
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                sli1.Text = "--Please Select--";
                sli1.Value = "0";
                lstservicelist.Add(sli1);
                for (int i = 0; i < lstservice.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;

                //list of state
                lststate = __vendorservice.GetAllState();
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
                VendorModel resources = null;
                resources = __vendorservice.GetDataViewEdit(CompanyKey);
                return View(resources);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PMVendorlist");
            }
        }

        [HttpPost]
        public ActionResult PMVendorEdit(int CompanyKey, VendorModel collection)
        {
            VendorModel vendorv = new VendorModel();
            try
            {
                
                Int64 value = 0;
                value = __vendorservice.VendorEdit(collection);
                if (value == 0)
                {
                    TempData["Sucessmessage"] = "Invitation resent.";
                    return RedirectToAction("PMVendorlist");
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                    return RedirectToAction("PMVendorEdit");

                }  

            }
            catch (Exception ex)
            {
                return View(vendorv);

            }

        }

        // GET: Viewvendor
        public ActionResult VendorView(int CompanyKey)
        {
            VendorModel vendorv = new VendorModel();

            try
            {
                IList<VendorModel> lstservice = null;
                VendorModel vendor = new VendorModel();
               

                //Service

                lstservice = __vendorservice.GetAllService();
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();                
                System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                sli1.Text = "--Please Select--";
                sli1.Value = "0";
                lstservicelist.Add(sli1);
                for (int i = 0; i < lstservice.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;
                vendor = __vendorservice.GetDataViewEdit(CompanyKey);

                IList<VendorModel> docs = new List<VendorModel>();
                docs = __vendorservice.GetbindDocument12(CompanyKey);

                ViewBag.InsuranceDocs = docs;
                if (vendor.invited != "0" )
                {
                    vendor.invitevendor = true;
                }

                else
                {
                    vendor.invitevendor = false;
                }



                if (vendor != null)
                {
                    if (vendor.VendorKey != 0)
                    {
                        vendor.starvenor = true;

                    }
                    else
                    {
                        vendor.starvenor = false;
                    }

                    return View(vendor);
                }
                else
                {

                    if (vendorv.VendorKey != 0)
                    {
                        vendorv.starvenor = true;

                    }
                    else
                    {
                        vendorv.starvenor = false;
                    }
                    return View(vendorv);

                }
                //IList<VendorManagerModel> docs = new List<VendorManagerModel>();
                //docs = __vendorManagerservice.GetbindDocument(CompanyKey);

                //ViewBag.InsuranceDocs = docs;
              
            }
            catch(Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(vendorv);
            }

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

        public JsonResult checkInvitedOrNot(int CompanyKey)
        {



            try
            {
                VendorModel vendor = new VendorModel();

                vendor = __vendorservice.GetDataViewEdit(CompanyKey);

                if (vendor.invited != "100")
                {
                    
                        return Json(false, JsonRequestBehavior.AllowGet);

                    }
                else{

                    return Json(true, JsonRequestBehavior.AllowGet);

                }


            }
            catch
            {
                return Json(null);
            }



        }


        public JsonResult IndexinsurancePaging(int CompanyKey)
        {
            try
            {
                IList<VendorModel> itemList = null;
                itemList = __vendorservice.Searchinsurance(CompanyKey);
                return Json(itemList, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult bindservice(int CompanyKey)
        {
            try
            {
                IList<VendorModel> servicelist = null;
                servicelist = __vendorservice.Getbindservice(CompanyKey);
                return Json(servicelist, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }

        //public JsonResult bindDocument(int CompanyKey)
        //{
        //    try
        //    {
        //        IList<VendorModel> Documentlist = null;
        
        //        //Documentlist = __vendorservice.GetbindDocument(CompanyKey);
        //        Documentlist = __vendorservice.GetbindDocumentByCompanyKey(CompanyKey);
        //        return Json(Documentlist, JsonRequestBehavior.AllowGet);

        //    }
        //    catch
        //    {
        //        return Json(null);
        //    }
        //}
        public ActionResult Delete(int CompanyKey)
        {

            bool Status = false;
            Status = __vendorservice.Remove(CompanyKey);
            if(Status == true)
            {
                TempData["Sucessmessage"] = "Record has been deleted successfully.";
                return RedirectToAction("PMVendorlist", "PMVendor");
            }
            else
            {
                ViewData["Errormessage"] = "Error";
                return RedirectToAction("PMVendorlist", "PMVendor");
            }
            
        }

        public ActionResult DeleteService(int CompanyKey, string servicename)
        {
            try
            {
                bool Status = false;
                Status = __vendorservice.RemoveService(CompanyKey,servicename);
                return RedirectToAction("VendorView", new { CompanyKey = CompanyKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("VendorView", new { CompanyKey = CompanyKey });
            }

        }
        public ActionResult Deletedocument(int CompanyKey, int documentid)
        {
            try
            {
                bool Status = false;
                Status = __vendorservice.RemoveDocument(CompanyKey, documentid);
                return RedirectToAction("VendorView", new { CompanyKey = CompanyKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("VendorView", new { CompanyKey = CompanyKey });
            }

        }

        public ActionResult MarkstarOrNot(int CompanyKey )
        {
            try
            {
               

                bool Status = false;
                int Resourcekey = Convert.ToInt32(Session["resourceid"]);
                Status = __vendorservice.MarkstarOrNot(CompanyKey, Resourcekey);
                return RedirectToAction("VendorView", new { CompanyKey = CompanyKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("VendorView", new { CompanyKey = CompanyKey });
            }

        }

        public JsonResult MarkstarOrNotforList(int CompanyKey)
        {



            try
            {
                bool Status = false;
                int Resourcekey = Convert.ToInt32(Session["resourceid"]);
                Status = __vendorservice.MarkstarOrNot(CompanyKey, Resourcekey);
                if (Status == true)
                {
                  
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    return Json(false, JsonRequestBehavior.AllowGet);
                }



            }
            catch(Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }



        }

        public JsonResult bindDocument12(int CompanyKey)
        {
            try
            {
                IList<VendorModel> Documentlist = null;

                Documentlist = __vendorservice.GetbindDocument12(CompanyKey);
                return Json(Documentlist, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult GetInsuranceDetails(int CompanyKey, int InsuranceKey)
        {
            try
            {
                IList<VendorModel> Documentlist = null;

                Documentlist = __vendorservice.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                return Json(Documentlist, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }

        public ActionResult DownloadFile(int DocumentKey, int CompanyKey, int InsuranceKey)
        {
            try
            {
                var doc = __vendorservice.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                string filename = doc.Where(w => w.Document.DocumentKey == DocumentKey).FirstOrDefault().Document.FileName;
                
                string path = Server.MapPath("~/Document/Insurance/" + CompanyKey + "/" + InsuranceKey + "/" + filename);
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                if (fileBytes.Length > 0)
                {
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                }

                TempData["Error"] = "File not found";
                System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
                rvd.Add("CompanyKey", CompanyKey);
                return RedirectToAction("VendorView", rvd);
            }
            catch
            {
                TempData["Error"] = "Opps... Something went wrong.";
                System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
                rvd.Add("CompanyKey", CompanyKey);
                return RedirectToAction("VendorView", rvd);
            }


        }
    }
}