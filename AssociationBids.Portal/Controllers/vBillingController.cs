using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using AssociationBids.Portal.Service.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class vBillingController : Controller
    {
        private readonly IvBillingService _vBillingService;

        public vBillingController(IvBillingService vBillingService)
        {
            this._vBillingService = vBillingService;
        }

        // GET: vBilling
        public ActionResult vBillingList()
        {
            return View();
        }      
               
        public JsonResult LoadBillingList()
        {
            List<vBillingModel> lstbilling = null;
            Int64 Comp = Convert.ToInt64(Session["CompanyKey"]);
            Int64 Reso = Convert.ToInt64(Session["resourceid"]);
            lstbilling = _vBillingService.LoadBillingList(Comp, Reso);
            return Json(lstbilling, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Int32 PaymentMethodKey)
        {
            try
            {               
                bool value = false;
                value = _vBillingService.Delete(PaymentMethodKey);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been deleted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("vBillingList");
            }

            catch (Exception ex)
            {

                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return RedirectToAction("vBillingList");
            }

        }

        public ActionResult vBillingAdd()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<JsonResult> PaymentVerificationAsync(string CardNumber, int Month, int Year, string CVV, string value, string name,string lname, bool PrimaryMethod, string addressline1, string addressline2, string zip, string city, string state)
        {
            // name = Convert.ToInt64(Session["CompanyKey"]);
            addressline1 = "addressline1";
             vBillingModel Billmodel = new vBillingModel();
            string value1 = "";

            bool value2 = false;
            try
            {
                int cid = Convert.ToInt32(Session["CompanyKey"]);
                int rid = Convert.ToInt32(Session["resourceid"]);
                var a = _vBillingService.LoadBillingList(cid, rid);
                var pmMethodCard = a.Where(w => w.StripeTokenID.StartsWith("cus")).FirstOrDefault();
                string tokenID = "";
                if (pmMethodCard != null)
                    tokenID = pmMethodCard.StripeTokenID;
                //value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, 1, name, addressline1, addressline2, zip, city, state);
                string PmId = "";
                if(tokenID == "")
                {
                    IResourceService rService = new ResourceService();
                    var res = rService.Get(rid);
                    value1 = await Common.Payment.GenerateTokenForCC(CardNumber, Month, Year, CVV, res.FirstName+' '+res.LastName, res.Address, res.Address2, res.Zip, res.City, res.State);
                    tokenID = value1.Split('=')[1].Split('&')[0];
                    PmId = value1.Split('=')[2];
                }
                else
                {
                    value1 = await Common.Payment.AddPaymentMethod(tokenID, CardNumber, Month, Year, CVV, PrimaryMethod);
                    
                }
                
                if (value1.Contains("Success"))
                {
                    if (value1.Split('?')[1] != "")
                    {
                        if(PmId == "")
                            PmId = value1.Split('=')[1];
                        string[] splitCardNumber =CardNumber.Trim().Split(' ');
                        string maskedcard = "";
                        for (int i = 0; i < splitCardNumber.Length - 1; i++)
                            maskedcard += "XXXX";
                        maskedcard += splitCardNumber[splitCardNumber.Length - 1];

                        Billmodel.StripeTokenID = tokenID;
                        Billmodel.PaymentMethodId = PmId;
                        Billmodel.CompanyKey = Convert.ToInt16(Session["CompanyKey"]);
                        Billmodel.AddedByResourceKey = Convert.ToInt16(Session["resourceid"]);
                        //Billmodel.MaskedCCNumber = Convert.ToString(CardNumber.Trim().Substring(CardNumber.Trim().Length - 4));
                        Billmodel.MaskedCCNumber = maskedcard;
                        Billmodel.CVV = CVV;
                        Billmodel.ValidTillYY = Year.ToString();
                        Billmodel.ValidTillMM = Month.ToString();
                        Billmodel.CardHolderFirstName = name;
                        Billmodel.CardHolderLastName = lname;
                        Billmodel.PrimaryMethod = PrimaryMethod;
                        Billmodel.Status = 1;

                        if (value1.Split('?')[0].Trim() == "Success")
                        { 
                            value2 = _vBillingService.vBillingInsert(Billmodel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            if (value1.Contains("Success"))
            {
                //1 Insert Fail
                //2 Insert Success
                //3 Invalid Card detail
                return value1.Split('?')[0].Contains("Failed") ? Json(1, JsonRequestBehavior.AllowGet) : Json(2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewData["Errormessage"] =  "Your card number is incorrect.";                               
                return Json(3, JsonRequestBehavior.AllowGet);                
            }
        }
        
        public JsonResult UpdatePM(int PaymentMethodKey)
        {
            int cid = Convert.ToInt32(Session["CompanyKey"]);
            int rid = Convert.ToInt32(Session["resourceid"]);
            var a = _vBillingService.LoadBillingList(cid, rid);
            var pmMethodCard = a.Where(w => w.PaymentMethodKey == PaymentMethodKey).FirstOrDefault();
            
            if(pmMethodCard != null)
            {
                //string st = await Common.Payment.ChangePrimaryMethod(pmMethodCard.StripeTokenID, pmMethodCard.PaymentMethodId);
                //if (st.Contains("Success"))
                {
                    bool status = _vBillingService.ChangePrimaryMethod(PaymentMethodKey, cid, rid);
                    if(status)
                        return Json(true, JsonRequestBehavior.AllowGet);
                }
                
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}