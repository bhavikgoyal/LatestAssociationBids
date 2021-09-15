using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AssociationBids.Portal.Controllers
{
    public class PMDashboardController : Controller
    {
        private readonly IResourceService _resourceservice;
        private readonly AssociationBids.Portal.Service.Base.IBidRequestService _bidRequestservice;
        public IABNotificationService __notification;

        public PMDashboardController(AssociationBids.Portal.Service.Base.IBidRequestService bidRequestService, IResourceService resourceService, IABNotificationService notification)
        {
            this._resourceservice = resourceService;
            this._bidRequestservice = bidRequestService;
            this.__notification = notification;
        }

        // GET: PMDashboard
        public ActionResult Dashboard(string Message = "")
        {
            if (Message != "")
            {
                TempData["SuccessMessage"] = Message;

            }
            return View();
        }


        public ActionResult ProfilView()
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
                                string Controller = "PMDashboard";
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
                return RedirectToAction("ProfilView");
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

        public ActionResult BidRequestEdit(int BidRequestKey, int NotificationKey, int ResponseBy,int BidVendorKey)
        {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            //int Portalkey = Convert.ToInt32(Session["Portalkey"]);
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
            staffDirectory.ResourceKey = ResponseBy;
            staffDirectory.Bkey = BidVendorKey;
            __notification.UpdateStatus(NotificationKey, "Read");


        
            staffDirectory.BidVendorKey = ResponseBy;
            return View(staffDirectory);

        }

        public JsonResult BindVendorDetail(int BidRequestKey, int ByResourceKey)
        {
            try
            {
                VendorModel bidVendorModel = new VendorModel();
                bidVendorModel = _bidRequestservice.BindVendorDetail(BidRequestKey, ByResourceKey, "No");

                return Json(bidVendorModel, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public ActionResult BidRequestEdit(BidRequestModel collection)
        {
            VendorModel vendorv = new VendorModel();
            try
            {
                BidRequestModel staffDirectory = new BidRequestModel();
                staffDirectory = _bidRequestservice.GetDataBidRequestViewEdit(collection.BidRequestKey);
                staffDirectory.BidRequestKey = Convert.ToInt32(collection.BidRequestKey);
                staffDirectory.BidDueDate = Convert.ToDateTime(collection.BidDueDates);
                staffDirectory.ResponseDueDate = Convert.ToDateTime(collection.ResponseDueDates);
                bool value = false;
                value = true;



                if (collection.ModuleKey == 106)
                {
                    if (value == true)
                    {
                        TempData["SuccessMessage"] = "Record has been updated successfully.";

                        return RedirectToAction("PMBidRequestView", "PMBidRequests", new { collection.BidRequestKey });
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Error";

                        return RedirectToAction("BidRequestEdit", new { collection.BidRequestKey });

                    }
                }

                if (value == true)
                {
                    VendorModel bidVendorModel = new VendorModel();
                    bidVendorModel = _bidRequestservice.BindVendorDetail(collection.BidRequestKey, collection.ResourceKey, "Yes");
                    Service.Base.Interface.IABNotificationService notificationService = new Service.Base.Code.ABNotificationService();
                    int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                    notificationService.InsertNotificationdashborad("BidReqDate", 100, collection.BidRequestKey, collection.ResourceKey, "Bid date extension request has been accepted for bid " + staffDirectory.Title, ResourceKey);
                    string Message = "Record has been updated successfully.";

                    return RedirectToAction("PMBidRequestView", "PMBidRequests", new { collection.BidRequestKey });
                }
                else
                {
                    ViewData["ErrorMessage"] = "Error";

                    return RedirectToAction("BidRequestEdit", new { collection.BidRequestKey });

                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("BidRequestEdit", new { collection.BidRequestKey });

            }

        }

    }
}