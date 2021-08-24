using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IForgotPasswordServices _forgotPasswordservice;

        public ForgotPasswordController(AssociationBids.Portal.Service.Base.IForgotPasswordServices forgotPasswordService)
        {
            this._forgotPasswordservice = forgotPasswordService;
        }
        // GET: ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            bool ErrorMsg = _forgotPasswordservice.CheckEmail(forgotPasswordModel.email);

            if (ErrorMsg == true)
            {
                ViewBag.msg = "Email has been Send";
                return View();
            }
            else
            {
                ViewBag.msg = "Email not found";
                return View();
            }
        }
    }
}


