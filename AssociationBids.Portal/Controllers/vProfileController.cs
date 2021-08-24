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
    public class vProfileController : Controller
    {
        private readonly IResourceService _resourceservice;

        public vProfileController(IResourceService resourceService)
        {
            this._resourceservice = resourceService;
        }



        public ActionResult VProfileView()
        {
            ResourceModel resourcemodel = new ResourceModel();

            
          

            int userid = Convert.ToInt32(Session["resourceid"]);
            if (userid == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var lstRadiuslist = new List<SelectListItem>()
                    {
                    //new SelectListItem{ Value="0",Text="Please Select",Selected=true},
                    new SelectListItem{ Value="1",Text="10 miles"},
                    new SelectListItem{ Value="2",Text="15 miles"},
                    new SelectListItem{ Value="3",Text="20 miles"},
                    new SelectListItem{ Value="4",Text="25 miles"},
                    };

                ViewBag.lstRadius = lstRadiuslist;
                resourcemodel = _resourceservice.GetDataViewEdit(userid);
                int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);

                IList<ResourceModel> lstservice = null;


                //Service
                lstservice = _resourceservice.AppoGetAllService("--Select Service--");
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();
                SelectListItem sServ = new SelectListItem();
                sServ.Text = "--- Select Service ---";
                sServ.Value = "0";
                lstservicelist.Add(sServ);

                for (int i = 0; i < lstservice.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;
                ViewBag.lstservice2 = lstservicelist;
                ViewBag.lstservice3 = lstservicelist;

                IList<ResourceModel> Services = null;
                Services = _resourceservice.GetServiceByCompany(CompanyKey);

                for (int i = 0; i < Services.Count; i++)
                {
                    if (i == 0)
                    {
                        resourcemodel.ServiceKey = Services[i].ServiceKey;
                    }
                    if (i == 1)
                    {
                        resourcemodel.ServiceKey1 = Services[i].ServiceKey;
                    }
                    if (i == 2)
                    {
                        resourcemodel.ServiceKey2 = Services[i].ServiceKey;
                    }

                }
                ViewBag.Address = resourcemodel.Address;
                ViewBag.Address2 = resourcemodel.Address2;
                ViewBag.City = resourcemodel.City;
                ViewBag.Zip = resourcemodel.Zip;
                ViewBag.State = resourcemodel.State;
                return View(resourcemodel);

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(resourcemodel);

            }
        }

        public ActionResult VProfile()
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
                var lstRadiuslist = new List<SelectListItem>()
                    {
                    //new SelectListItem{ Value="0",Text="Please Select",Selected=true},
                    new SelectListItem{ Value="1",Text="10 miles"},
                    new SelectListItem{ Value="2",Text="15 miles"},
                    new SelectListItem{ Value="3",Text="20 miles"},
                    new SelectListItem{ Value="4",Text="25 miles"},
                    };

                ViewBag.lstRadius = lstRadiuslist;
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
                int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);

                IList<ResourceModel> lstservice = null;


                //Service
                lstservice = _resourceservice.AppoGetAllService("--Select Service--");
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();
                SelectListItem sServ = new SelectListItem();
                sServ.Text = "--- Select Service ---";
                sServ.Value = "0";
                lstservicelist.Add(sServ);

                for (int i = 0; i < lstservice.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;
                ViewBag.lstservice2 = lstservicelist;
                ViewBag.lstservice3 = lstservicelist;

                IList<ResourceModel> Services = null;
                Services = _resourceservice.GetServiceByCompany(CompanyKey);

                for (int i = 0; i < Services.Count; i++)
                {
                    if (i == 0)
                    {
                        resourcemodel.ServiceKey = Services[i].ServiceKey;
                    }
                    if (i == 1)
                    {
                        resourcemodel.ServiceKey1 = Services[i].ServiceKey;
                    }
                    if (i == 2)
                    {
                        resourcemodel.ServiceKey2 = Services[i].ServiceKey;

                    }
                    resourcemodel.VendorKey = Services[i].VendorKey;
                    resourcemodel.Radius = Services[i].Radius;
                }

                return View(resourcemodel);

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(resourcemodel);

            }
        }

        [HttpPost]
        public ActionResult VProfile(AssociationBids.Portal.Model.ResourceModel modal, HttpPostedFileBase file)
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
                            if (Request.Files.Keys[i] == "ProfilePicture") {
                                string ImageName = Request.Files[i].FileName;
                                Int64 ImageLength = Request.Files[i].ContentLength;
                                string Title = "Profile Image";
                                string Controller = "vProfile";
                                string Action = "VProfile";

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
                return RedirectToAction("VProfileView");
            }
            catch (Exception ex)
            {
                lststate = _resourceservice.GetAllState();
                IList<ResourceModel> lstservice = _resourceservice.AppoGetAllService("--Select Service--");
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();
                SelectListItem sServ = new SelectListItem();
                sServ.Text = "--- Select Service ---";
                sServ.Value = "0";
                lstservicelist.Add(sServ);

                for (int i = 0; i < lstservice.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;
                ViewBag.lstservice2 = lstservicelist;
                ViewBag.lstservice3 = lstservicelist;
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
                var lstRadiuslist = new List<SelectListItem>()
                    {
                    //new SelectListItem{ Value="0",Text="Please Select",Selected=true},
                    new SelectListItem{ Value="1",Text="10 miles"},
                    new SelectListItem{ Value="2",Text="15 miles"},
                    new SelectListItem{ Value="3",Text="20 miles"},
                    new SelectListItem{ Value="4",Text="25 miles"},
                    };

                ViewBag.lstRadius = lstRadiuslist;
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