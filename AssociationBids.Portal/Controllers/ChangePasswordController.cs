using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword

        private readonly IChangePasswordService _ChangePasswordService;

        // GET: ChangePassword
        public ActionResult Edit()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
             {

                throw ex ;
            }
        }
        public ChangePasswordController(IChangePasswordService changePasswordService)
        {
            this._ChangePasswordService = changePasswordService;
        }

        [HttpPost]
        public ActionResult Edit(ChangePasswordModel changePasswordModel)
        {
            //string UserId = "";
            if(Session["userid"] == null)
                return RedirectToAction("Logout", "Login");
            changePasswordModel.UserId= Session["userid"].ToString ();
            
            int  response = _ChangePasswordService.ChangePassword(changePasswordModel);
            if (response == 1)
            {
                if (Session["PortalKey"] != null && Convert.ToInt32(Session["PortalKey"]) != 0)
                {
                    int portalKey = Convert.ToInt32(Session["PortalKey"]);

                    if (portalKey == 1)
                    {
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                    else if (portalKey == 2)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else if (portalKey == 3)
                    {
                            return RedirectToAction("Index", "VDashboard");
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                }
                return RedirectToAction("Logout", "Login");
            }
            else
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('Curret PassWord Is Invalid!');window.location = '/ChangePassword/Edit';</script>");
                TempData["wrongpass"] = "Current password is wrong.";
                return View(changePasswordModel);
            }
        }

        public ActionResult ErrorMessage()
        {
            return View();
        }


        public ActionResult UpdateTerm()
        {

            int UserKey = Convert.ToInt32(Session["userid"]);

            int Ak = Convert.ToInt32(Session["Akey"]);
            bool Res =  _ChangePasswordService.UpdateTermsConditions("", UserKey, Ak);
            if (Res == true)
            {
                return RedirectToAction("Dashboard", "PMDashboard");
            }
            else
            {
                return RedirectToAction("UserAggrement", "ChangePassword");
            }
        }


        public ActionResult UserAggrement()
        {
            int UserKey = Convert.ToInt32(Session["userid"]);
            bool termact = Convert.ToBoolean(Session["Termaccpected"]);
            ViewBag.UserKey = UserKey;
            ViewBag.Isterm = termact;

            ChangePasswordModel cc = null;

                cc = _ChangePasswordService.GeAgreementDetails();
               Session["Akey"] = cc.AggrementKey;
            if (termact == false)
            {
                return View(cc);
            }
            else
            {
                 return RedirectToAction("Dashboard", "PMDashboard");
            }
           
        }
        public ActionResult ResetPassword()
        {
            try
            {
                string CurrentURL = System.Web.HttpContext.Current.Request.Url.ToString();
                int UserKey = Convert.ToInt32(CurrentURL.Split('=')[1]);
                ViewBag.UserKey = UserKey;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(ChangePasswordModel changePasswordModel)
        {
            //string UserId = "";
            string CurrentURL = System.Web.HttpContext.Current.Request.Url.ToString();
            //int UserKey = Convert.ToInt32(CurrentURL.Split('=')[1]);

            //changePasswordModel.UserId = UserKey.ToString();
            int response = _ChangePasswordService.ResetPasswordByUser(changePasswordModel);
            if (response == 1)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>window.location = '/ChangePassword/Edit';</script>");
            }
        }
    }
}