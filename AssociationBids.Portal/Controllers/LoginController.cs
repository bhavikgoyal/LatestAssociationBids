using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model.Login;
using AssociationBids.Portal.Service.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        // GET: Login
        public ActionResult Index(int isinsurance = 0)
        {
            LoginModel loginModel = new LoginModel();
            loginModel.isinsurance = isinsurance;
            return View(loginModel);
        }

        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpPost]
         public ActionResult Index(LoginModel loginModel)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    //string password = Common.Security.Decrypt("Oe58gljdfcwjVTIy6GQJc2S+QEiI262P3WkYxtJK4ls=");
                    LoginResponseModel loginResponseModel = null;
                    loginResponseModel = _loginService.GetUsersDetails(loginModel);
                    if (loginResponseModel != null && loginResponseModel.UserId > 0)
                    {
                        Session["userid"] = loginResponseModel.UserId;
                        Session["resourceid"] = loginResponseModel.ResourceId;
                        Session["username"] = loginResponseModel.UserName;
                        Session["Companyname"] = loginResponseModel.Companyname;
                        Session["GroupKey"] = loginResponseModel.GroupKey;
                        Session["PortalKey"] = loginResponseModel.PortalKey;
                        Session["CompanyKey"] = loginResponseModel.companyKey;
                        Session["Password"] = loginResponseModel.password;
                        Session["ProfileImage"] =  loginResponseModel.ImageName;
                        Session["Name"] = loginResponseModel.Title;
                        Session["ProfileImageBase64"] = "";
                        Session["FirstTimeAccess"] = loginResponseModel.FirstTimeAccess;
                        Session["Termaccpected"] = loginResponseModel.Termaccpected;
                        Session["InsauranceKey"] = loginResponseModel.InsauranceKey;
                        try
                        {
                            string ProfileImagePath = Server.MapPath("~/Document/Resources/" + loginResponseModel.ResourceId.ToString() + "/" + loginResponseModel.ImageName);
                            if (System.IO.File.Exists(ProfileImagePath))
                            {
                                byte[] imgdata = System.IO.File.ReadAllBytes(ProfileImagePath);
                                Session["ProfileImageBase64"] = "data:image/png;base64," + Convert.ToBase64String(imgdata, 0, imgdata.Length);
                            }
                            else {
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

                     
                        if (loginResponseModel.FirstTimeAccess != true)
                        {
                            //if (loginResponseModel.companyTypeKey == 0)
                            //{
                            //    return RedirectToAction("Dashboard", "Dashboard");
                            //}
                            //else
                            //{
                            //    return RedirectToAction("Dashboard", "PMDashboard");
                            //}
                            if (loginModel.isinsurance == 1)
                            {
                                return RedirectToAction("PolicyList", "VendorPolicy");
                            }
                            else if (loginResponseModel.Title == "Association Bids Portal")
                            {
                                return RedirectToAction("Dashboard", "Dashboard");
                            }
                            else if (loginResponseModel.Title == "Company Portal" && loginResponseModel.Termaccpected == false)
                            {
                                return RedirectToAction("UserAggrement", "ChangePassword");
                            }
                            else if (loginResponseModel.Title == "Company Portal" && loginResponseModel.Termaccpected == true)
                            {
                                return RedirectToAction("Dashboard", "PMDashboard");
                            }
                            else if (loginResponseModel.Title == "Vendor Portal")
                            {
                                return RedirectToAction("Index", "VDashboard");

                                //if (loginResponseModel.InsauranceKey == null || loginResponseModel.InsauranceKey == 0 )
                                //{
                                //    return RedirectToAction("Index", "VDashboard");
                                //}
                                //else
                                //{
                                //    return RedirectToAction("Index", "VDashboard");
                                //}

                            }
                            else
                            {
                                return RedirectToAction("Dashboard", "Dashboard");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Edit", "ChangePassword");
                        }
                    }
                    else
                    {
                        ViewBag.msg = "NoData";
                        return View(loginModel);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
               
                return this.Json("Fail");
            }
        }

        public ActionResult LogOut() {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}