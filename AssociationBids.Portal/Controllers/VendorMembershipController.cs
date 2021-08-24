using System;
using System.Collections.Generic;
using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MembershipFilterModel = AssociationBids.Portal.Model.MembershipFilterModel;

namespace AssociationBids.Portal.Controllers
{

    public class VendorMembershipController : Controller
    {

        private readonly AssociationBids.Portal.Service.Base.Interface.IMembershipService _mEmberShipService;
        public VendorMembershipController(AssociationBids.Portal.Service.Base.Interface.IMembershipService mEmaberShipSeervice)
        {
            this._mEmberShipService = mEmaberShipSeervice;
        }
        // GET: VendorMembership
        public ActionResult MemberShipFind()
        {
            int VendorKey = Convert.ToInt32(Session["CompanyKey"]);
            List<MembershipModel> MemberShipRecord = null;
            MembershipModel membership = new MembershipModel();
            membership = _mEmberShipService.GetDataViewEdit(VendorKey);
            if (membership != null)
            {
                if (membership.CurrentDate > membership.RenewalDate)
                {
                    ViewBag.Active = "InActive";
                }
                else
                {
                    ViewBag.Active = "Active";
                }
            }
            return View(membership);
        }
      
    }
}