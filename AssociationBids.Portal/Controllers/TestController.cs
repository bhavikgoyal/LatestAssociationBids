using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssociationBids.Portal.Model;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PropertyModel = AssociationBids.Portal.Model.PropertyModel;
using AssociationBids.Portal.Service.Base.Interface;
using AssociationBids.Portal.Service.Base.Code;
using Stripe;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Controllers
{
    public class TestController : Controller
    {
        // GET: API
        private readonly AssociationBids.Portal.Service.Base.Interface.IAPIService __IAPIservice;

        public TestController(IAPIService APIservicsse)
        {
            this.__IAPIservice = APIservicsse;
        }

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public string AddAutomativallyBidVendor()
        {
            try
            {
                IList<APIModel> lstuser = null;

                lstuser = __IAPIservice.GetBidRequestListPassedResponsedate();


                for (int i = 0; i < lstuser.Count; i++)
                {
                    __IAPIservice.insertBidVendorForBid(lstuser[i].BidRequestKey);
                }



                if (lstuser.Count == 0)
                {
                    return ("There is Zero BidVendor For Automatically Add.");
                }
                else
                {
                    return ("Total" + lstuser.Count + "For AddAutomativallyBidVendor.");
                }
            }

            catch (Exception ex)
            {
                string Error = "Error into Add Automativally BidVendor is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into Add Automativally BidVendor is " + Convert.ToString(ex));
            }
        }

        public string BidReminderDueDate()
        {
            try
            {


                IList<APIModel> lstuser = null;
                string lookUpTitle = "Bid invitation reminde";
                int userid = Convert.ToInt32(Session["resourceid"]);
                lstuser = __IAPIservice.SendMailToBidDueRemiders(lookUpTitle);

                if (lstuser.Count == 0)
                {
                    Common.Error.WriteErrorsToFile("There is Zero Bid For Vendor BidReminder.");
                    return ("There is Zero Bid For Vendor BidReminder.");
                }
                else
                {
                    Common.Error.WriteErrorsToFile("Total" + lstuser.Count + "BidReminderSent.");
                    return ("Total" + lstuser.Count + "BidReminderSent.");
                }
            }

            catch (Exception ex)
            {
                string Error = "Error into BidReminderDueDate is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into BidReminderDueDate is " + Convert.ToString(ex));
            }
        }
    }
}