using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class LayOutMasterController : Controller
    {



        // GET: ComonFunction
        public ActionResult _SendMessage()
        {
            return PartialView();
        }
    }
}