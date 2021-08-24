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
    public class APISController : Controller
    {
        // GET: API
        private readonly AssociationBids.Portal.Service.Base.Interface.IAPIService __IAPIservice;

        public APISController(IAPIService APIservicsse)
        {
            this.__IAPIservice = APIservicsse;
        }

        public ActionResult Index()
        {
            return View();
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

        public string BidSubmissionReminderDue()
        {

            try
            {
                IList<APIModel> lstuser = null;
                string lookUpTitle = "Proposal Reminder";
                lstuser = __IAPIservice.SendMailToBidSubmissionReminderDue(lookUpTitle);

                if (lstuser.Count == 0)
                {
                    return ("There is Zero Reminder For BidVendor.");
                }
                else
                {
                    return ("Total" + lstuser.Count + "BidSubmissionReminderDue.");
                }
            }

            catch (Exception ex)
            {

                string Error = "Error into BidSubmissionReminderDue is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into BidSubmissionReminderDue is " + Convert.ToString(ex));
            }
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

        public string CardExpirayMail()
        {
            try
            {

                IList<APIModel> lstuser = null;
                string lookUpTitle = "Credit card expired";
                lstuser = __IAPIservice.CardExpiremail(lookUpTitle);

                if (lstuser.Count == 0)
                {
                    return ("There is Zero count of expired card.");
                }
                else
                {
                    return ("Total Count of expired card is " + lstuser.Count + "For CardExpirayMail.");
                }
            }

            catch (Exception ex)
            {
                string Error = "Error into CardExpirayMail is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into CardExpirayMail is " + Convert.ToString(ex));
            }
        }


        public string CardExpirayMails()
        {
            try
            {


                IList<APIModel> lstuser = null;
                string lookUpTitle = "Credit card about to expire";
                lstuser = __IAPIservice.CardExpiremail(lookUpTitle);

                if (lstuser.Count == 0)
                {
                    return ("There is Zero count of card that are near to expiry date.");
                }
                else
                {
                    return ("Total Count of expired card is " + lstuser.Count + "For Card about to Expire.");
                }
            }

            catch (Exception ex)
            {
                string Error = "Error into Expirey Card reminder mail is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into Expirey Card reminder mail is " + Convert.ToString(ex));
            }
        }

        public string InsaurenceRenawalmail()
        {
            try
            {
                IList<APIModel> lstuser = null;
                string lookUpTitle = "Insurance renewal";
                lstuser = __IAPIservice.InsaurenceRenawalmail(lookUpTitle);

                if (lstuser.Count == 0)
                {
                    return ("There is Zero count of Insurance that are near to expiry date.");
                }
                else
                {
                    return ("Total Count of expired Insurance is " + lstuser.Count);
                }
            }

            catch (Exception ex)
            {
                string Error = "Error into Insurance renewal reminder mail is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into Insurance renewal reminder mail is " + Convert.ToString(ex));

            }
        }

        public HttpResponseMessage InviteMailToVendor()
        {

            APIModel registrationmodel = new APIModel();
            string value1 = "";
            try
            {
                IList<APIModel> lstuser = null;
                string lookUpTitle = "Bid Invitation";
                lstuser = __IAPIservice.InvitemailToVendor(lookUpTitle);
                List<System.Web.Mvc.SelectListItem> istusersss = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < lstuser.Count; i++)
                {
                    string Title = lstuser[i].Title;
                    string Email = lstuser[i].Email;
                    string propertyTitle = lstuser[i].propertyTitle;
                    string Vendorname = lstuser[i].VendorName;
                    string Companyname = lstuser[i].CompanyName;
                    string BidDueDate = lstuser[i].bdate;

                    __IAPIservice.Invitemail(Email, Title, propertyTitle, Vendorname, Companyname, BidDueDate, lookUpTitle);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.Found);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }
        }

        public string MembershipRenewalMail()
        {
            try
            {
                IList<APIModel> lstuser = null;
                string lookUpTitle = "Membership Renewal";
                lstuser = __IAPIservice.MembershipRenewalMail(lookUpTitle);

                if (lstuser.Count == 0)
                {
                    return ("There is Zero count of Membership Renewal  that are near to expiry date.");
                }
                else
                {
                    return ("Total Count of Renewal Membership is " + lstuser.Count);
                }
            }

            catch (Exception ex)
            {
                string Error = "Error into Renewal Membership  is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into Renewal Membership is" + Convert.ToString(ex));

            }
        }

        public string MembershipExpired()
        {
            try
            {
                IList<APIModel> lstuser = null;
                string lookUpTitle = "Membership Expired";
                lstuser = __IAPIservice.MembershipExpired(lookUpTitle);

                if (lstuser.Count == 0)
                {
                    return ("There is Zero count of Membership Expired");
                }
                else
                {
                    return ("Total Count of Expired Membership is " + lstuser.Count);
                }
            }

            catch (Exception ex)
            {
                string Error = "Error into Expired Membership  is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into Expired Membership is" + Convert.ToString(ex));

            }
        }


        public string InsaurenceExpiredmail()
        {
            try
            {
                IList<APIModel> lstuser = null;
                string lookUpTitle = "Insurance expired";
                lstuser = __IAPIservice.InsaurenceExpiredmail(lookUpTitle);

                if (lstuser.Count == 0)
                {
                    return ("There is Zero count of Insurance Expired");
                }
                else
                {
                    return ("Total Count of expired Insurance is " + lstuser.Count);
                }
            }

            catch (Exception ex)
            {
                string Error = "Error into Insurance expired reminder is " + Convert.ToString(ex);
                Common.Error.WriteErrorsToFile(Error);
                return ("Error into Insurance expired reminder is " + Convert.ToString(ex));

            }
        }
        public async Task<IHttpActionResult> WinfeechargePayment()
        {
            APIModel registrationmodel = new APIModel();
            string value1 = "";
            try
            {
                string destinationPath = Server.MapPath("~/Document/Invoices/");
                string imagepath = Server.MapPath("~/");
                IList<APIModel> lstuser = null;
                string lookUpTitle = "Win Fee";
                lstuser = __IAPIservice.WinFeechargeMailAsync(lookUpTitle);
                List<System.Web.Mvc.SelectListItem> istusersss = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < lstuser.Count; i++)
                {
                    string CardNumber = lstuser[i].CardNumber;
                    int Month = lstuser[i].CardExpiryMonth;
                    int Year = lstuser[i].CardExpiryYear;
                    string CVV = lstuser[i].CVV;
                    int value = lstuser[i].Amount*100;
                    int MainValue = lstuser[i].Amount;
                    string name = lstuser[i].CardHoldername;
                    string addressline1 = lstuser[i].Address1;
                    string addressline2 = lstuser[i].Address2;
                    string zip = lstuser[i].PostalCode;
                    string city = lstuser[i].City;
                    string state = lstuser[i].State;
                    string stripeToken = lstuser[i].stripeToken;
                    string pmId = lstuser[i].PaymentMethodId;
                    int VendorKey = lstuser[i].VendorKey;
                    int PaymentTypeKey = lstuser[i].PaymentTypeKey;
                    string CardHoldername = lstuser[i].CardHoldername;
                    string Title = lstuser[i].Title;
                    string Email = lstuser[i].Email;
                    string RefrenceNumber = lstuser[i].BidvendorId;
                    //value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, value, name, addressline1, addressline2, zip, city, state);
                    value1 = await Common.Payment.PayAsyncNew(value, stripeToken, pmId, Title);
                    if (value1.Contains("Success"))
                    {
                        if (value1.Split('?')[1] != "")
                        {
                            registrationmodel.stripeToken = value1.Split('?')[1];
                            if (value1.Split('?')[0].Trim() == "Success")

                            {
                                __IAPIservice.PaymentmodalInsert(CVV, registrationmodel.stripeToken);
                                __IAPIservice.Insertpaymet(RefrenceNumber, MainValue, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                                __IAPIservice.DowmloadInvoice(imagepath, destinationPath, VendorKey, Email, CardHoldername, Title, MainValue, addressline1, addressline2, zip, city, state);
                                __IAPIservice.WinFeeMain(Email, CardHoldername, MainValue, Title, lookUpTitle);

                            }
                        }
                    }
                    else
                    {
                        __IAPIservice.Insertpaymetfail(RefrenceNumber, MainValue, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                        string Remarks = "Paymentfail";
                        __IAPIservice.InsertIntoErrorLog(Remarks);
                    }
                }
                return null;    
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return null;
            }
        }

        public async System.Threading.Tasks.Task<JsonResult> WinfeechargePaymentAccpectBid(int BidRequestKey, string BidVendorkey)
        {
            string destinationPath = Server.MapPath("~/Document/Invoices/");
            string imagepath = Server.MapPath("~/");
            APIModel registrationmodel = new APIModel();
            string value1 = "";
            APIModel resources = null;
            resources = __IAPIservice.GetdataWinfeechargePaymentAccpectBid(BidRequestKey, BidVendorkey);
            if (resources.CardNumber == null || resources.CardNumber == "")
            {
                __IAPIservice.Insertpaymetfail(resources.BidvendorId, resources.Amount, resources.VendorKey, resources.PaymentTypeKey, registrationmodel.stripeToken);
                string Remarks = "Paymentfail";
                __IAPIservice.InsertIntoErrorLog(Remarks);
            }

            try
            {
                string lookUpTitle = "Win Fee";
                string CardNumber = resources.CardNumber;
                int Month = resources.CardExpiryMonth;
                int Year = resources.CardExpiryYear;
                string CVV = resources.CVV;
                int value = resources.Amount*100;
                string name = resources.CardHoldername;
                string addressline1 = resources.Address1;
                string addressline2 = resources.Address2;
                string zip = resources.PostalCode;
                string city = resources.City;
                string state = resources.State;
                string stripeToken = resources.stripeToken;
                string pmId = resources.PaymentMethodId;
                int VendorKey = resources.VendorKey;
                int PaymentTypeKey = resources.PaymentTypeKey;
                string CardHoldername = resources.CardHoldername;
                string Title = resources.Title;
                string Email = resources.Email;
                string RefrenceNumber = resources.BidvendorId;

                //value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, value, name, addressline1, addressline2, zip, city, state);
                value1 = await Common.Payment.PayAsyncNew(value, stripeToken, pmId, Title);
                if (value1.Contains("Success"))
                {
                    registrationmodel.stripeToken = value1.Split('?')[1];
                    if (value1.Split('?')[1] != "")
                    {
                        __IAPIservice.PaymentmodalInsert(CVV, stripeToken);
                        __IAPIservice.Insertpaymet(RefrenceNumber, resources.Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                        __IAPIservice.DowmloadInvoice(imagepath, destinationPath, VendorKey, Email, CardHoldername, Title, resources.Amount, addressline1, addressline2, zip, city, state);
                        __IAPIservice.WinFeeMain(Email, CardHoldername, resources.Amount, Title, lookUpTitle);
                    }
                }
                else
                {
                    __IAPIservice.Insertpaymetfail(RefrenceNumber, resources.Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                    string Remarks = "Paymentfail";
                    __IAPIservice.InsertIntoErrorLog(Remarks);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public async System.Threading.Tasks.Task<JsonResult> WinfeechargePaymentAccpectBids(int BidRequestKey, string BidVendorkey)
        {
            string destinationPath = Server.MapPath("~/Document/Invoices/");
            string imagepath = Server.MapPath("~/");
            APIModel registrationmodel = new APIModel();
            string value1 = "";
            APIModel resources = null;
            resources = __IAPIservice.GetdataWinfeechargePaymentAccpectBid(BidRequestKey, BidVendorkey);
            //if (resources.CardNumber  == null || resources.CardNumber == "")
            //{
            //    __IAPIservice.Insertpaymetfail(resources.BidvendorId, resources.Amount, resources.VendorKey, resources.PaymentTypeKey, registrationmodel.stripeToken);
            //       string Remarks = "Paymentfail";
            //    __IAPIservice.InsertIntoErrorLog(Remarks);
            //}

            try
            {
                string lookUpTitle = "Win Fee";
                string CardNumber = resources.CardNumber;
                int Month = resources.CardExpiryMonth;
                int Year = resources.CardExpiryYear;
                string CVV = resources.CVV;
                int value = resources.Amount*100;
                string name = resources.CardHoldername;
                string addressline1 = resources.Address1;
                string addressline2 = resources.Address2;
                string zip = resources.PostalCode;
                string city = resources.City;
                string state = resources.State;
                string stripeToken = resources.stripeToken;
                string pmId = resources.PaymentMethodId;
                int VendorKey = resources.VendorKey;
                int PaymentTypeKey = resources.PaymentTypeKey;
                string CardHoldername = resources.CardHoldername;
                string Title = resources.Title;
                string Email = resources.Email;
                string RefrenceNumber = resources.BidvendorId;

                //value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, value, name, addressline1, addressline2, zip, city, state);
                value1 = await Common.Payment.PayAsyncNew(value, stripeToken, pmId, Title);
                if (value1.Contains("Success"))
                {
                    registrationmodel.stripeToken = value1.Split('?')[1];
                    if (value1.Split('?')[1] != "")
                    {
                        __IAPIservice.PaymentmodalInsert(CVV, stripeToken);
                        __IAPIservice.Insertpaymet(RefrenceNumber, resources.Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                        __IAPIservice.DowmloadInvoice(imagepath, destinationPath, VendorKey, Email, CardHoldername, Title, resources.Amount, addressline1, addressline2, zip, city, state);
                        //__IAPIservice.WinFeeMain(Email, CardHoldername, value, Title, lookUpTitle);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    __IAPIservice.Insertpaymetfail(RefrenceNumber, resources.Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                    string Remarks = "Paymentfail";
                    __IAPIservice.InsertIntoErrorLog(Remarks);
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public async Task<IHttpActionResult> MembershipFee()
        {
            string destinationPath = Server.MapPath("~/Document/Invoices/");
            string imagepath = Server.MapPath("~/");
            APIModel registrationmodel = new APIModel();
            string value1 = "";
            try
            {
                IList<APIModel> lstuser = null;
                string lookUpTitle = "MembershipFees";
                lstuser = __IAPIservice.MembershipFees(lookUpTitle);
                List<System.Web.Mvc.SelectListItem> istusersss = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < lstuser.Count; i++)
                {
                    string CardNumber = lstuser[i].CardNumber;
                    int Month = lstuser[i].CardExpiryMonth;
                    int Year = lstuser[i].CardExpiryYear;
                    string CVV = lstuser[i].CVV;
                    int value = lstuser[i].Amount*100;
                    int MainValue = lstuser[i].Amount;
                    string name = lstuser[i].CardHoldername;
                    string addressline1 = lstuser[i].Address1;
                    string addressline2 = lstuser[i].Address2;
                    string zip = lstuser[i].PostalCode;
                    string city = lstuser[i].City;
                    string state = lstuser[i].State;
                    string stripeToken = lstuser[i].stripeToken;
                    string pmId = lstuser[i].PaymentMethodId;
                    int VendorKey = lstuser[i].VendorKey;
                    int PaymentTypeKey = lstuser[i].PaymentTypeKey;
                    string CardHoldername = lstuser[i].CardHoldername;
                    string Title = lstuser[i].Title;
                    string Email = lstuser[i].Email;
                    string RefrenceNumber = lstuser[i].RNumber;
                    //value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, 1, name, addressline1, addressline2, zip, city, state);
                    value1 = await Common.Payment.PayAsyncNew(value, stripeToken, pmId, Title);
                    if (value1.Contains("Success"))
                    {
                        if (value1.Split('=')[1] != "")
                        {
                            registrationmodel.stripeToken = value1.Split('?')[1];
                            if (value1.Split('?')[0].Trim() == "Success")
                            {
                                __IAPIservice.PaymentmodalInsert(CVV, stripeToken);
                                __IAPIservice.DowmloadInvoice(imagepath, destinationPath, VendorKey, Email, CardHoldername, Title, MainValue, addressline1, addressline2, zip, city, state);
                                __IAPIservice.Insertpaymet(RefrenceNumber, MainValue, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                                __IAPIservice.Updatemembership(VendorKey);
                                __IAPIservice.WinFeeMain(Email, CardHoldername, MainValue, Title, lookUpTitle);
                            }
                        }
                    }
                    else
                    {
                        __IAPIservice.Insertpaymetfail(RefrenceNumber, MainValue, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                        string Remarks = "Paymentfail";
                        __IAPIservice.InsertIntoErrorLog(Remarks);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return null;
            }
        }

        public async System.Threading.Tasks.Task<JsonResult> MemberShipFeeForApprove(int CompanyKey)
        {
            string destinationPath = Server.MapPath("~/Document/Invoices/");
            string imagepath = Server.MapPath("~/");
            APIModel registrationmodel = new APIModel();
            string value1 = "";
            APIModel resources = null;
            resources = __IAPIservice.GetDataforMembershipPayment(CompanyKey);
            if (resources.CardNumber == null || resources.CardNumber == "")
            {
                __IAPIservice.Insertpaymetfail(resources.BidvendorId, resources.Amount, resources.VendorKey, resources.PaymentTypeKey, registrationmodel.stripeToken);
                string Remarks = "Paymentfail";
                __IAPIservice.InsertIntoErrorLog(Remarks);
            }
            try
            {
                string lookUpTitle = "MembershipFees";
                string CardNumber = resources.CardNumber;
                int Month = resources.CardExpiryMonth;
                int Year = resources.CardExpiryYear;
                string CVV = resources.CVV;
                int value = resources.Amount*100;
                string name = resources.CardHoldername;
                string addressline1 = resources.Address1;
                string addressline2 = resources.Address2;
                string zip = resources.PostalCode;
                string city = resources.City;
                string state = resources.State;
                string stripeToken = resources.stripeToken;
                string pmId = resources.PaymentMethodId;
                int VendorKey = CompanyKey;
                int PaymentTypeKey = resources.PaymentTypeKey;
                string CardHoldername = resources.CardHoldername;
                string Title = "Membership Fee";
                string Email = resources.Email;
                string RefrenceNumber = resources.RNumber;

                //value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, 1, name, addressline1, addressline2, zip, city, state);
                value1 = await Common.Payment.PayAsyncNew(value, resources.stripeToken, pmId, Title);
                if (value1.Contains("Success"))
                {
                    registrationmodel.stripeToken = value1.Split('?')[1];
                    if (value1.Split('?')[1] != "")
                    {
                        __IAPIservice.PaymentmodalInsert(CVV, registrationmodel.stripeToken);
                        __IAPIservice.DowmloadInvoice(imagepath, destinationPath, VendorKey, Email, CardHoldername, Title, resources.Amount, addressline1, addressline2, zip, city, state);

                        __IAPIservice.Insertpaymet(RefrenceNumber, resources.Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                        __IAPIservice.MembershipInsert(VendorKey);
                        __IAPIservice.WinFeeMain(Email, CardHoldername, resources.Amount, Title, lookUpTitle);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    __IAPIservice.Insertpaymetfail(RefrenceNumber, resources.Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                    string Remarks = "Paymentfail";
                    __IAPIservice.InsertIntoErrorLog(Remarks);
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public async System.Threading.Tasks.Task<string> MemberShipFeeForApproveNew(int CompanyKey)
        {
            string destinationPath = Server.MapPath("~/Document/Invoices/");
            string imagepath = Server.MapPath("~/");
            APIModel registrationmodel = new APIModel();
            string value1 = "";
            APIModel resources = null;
            resources = __IAPIservice.GetDataforMembershipPayment(CompanyKey);

            if (resources != null)
            {
                if (resources.CardNumber == null || resources.CardNumber == "")
                {
                    __IAPIservice.Insertpaymetfail(resources.BidvendorId, resources.Amount, resources.VendorKey, resources.PaymentTypeKey, registrationmodel.stripeToken);
                    string Remarks = "Paymentfail";
                    __IAPIservice.InsertIntoErrorLog(Remarks);
                }
                try
                {
                    string lookUpTitle = "MembershipFees";
                    string CardNumber = resources.CardNumber;
                    int Month = resources.CardExpiryMonth;
                    int Year = resources.CardExpiryYear;
                    string CVV = resources.CVV;
                    int value = resources.Amount * 100;
                    string name = resources.CardHoldername;
                    string addressline1 = resources.Address1;
                    string addressline2 = resources.Address2;
                    string zip = resources.PostalCode;
                    string city = resources.City;
                    string state = resources.State;
                    string stripeToken = resources.stripeToken;
                    string pmId = resources.PaymentMethodId;
                    int VendorKey = CompanyKey;
                    int PaymentTypeKey = resources.PaymentTypeKey;
                    string CardHoldername = resources.CardHoldername;
                    string Title = "Membership Fee";
                    string Email = resources.Email;
                    string RefrenceNumber = resources.RNumber;

                    //value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, 1, name, addressline1, addressline2, zip, city, state);
                    value1 = await Common.Payment.PayAsyncNew(value, resources.stripeToken, pmId, Title);
                    if (value1.Contains("Success"))
                    {
                        registrationmodel.stripeToken = value1.Split('?')[1];
                        if (value1.Split('?')[1] != "")
                        {
                            Session.Add("TempCompanyKey", CompanyKey);
                            __IAPIservice.PaymentmodalInsert(CVV, registrationmodel.stripeToken);

                            __IAPIservice.Insertpaymet(RefrenceNumber, resources.Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                            __IAPIservice.MembershipInsert(VendorKey);
                            __IAPIservice.WinFeeMain(Email, CardHoldername, resources.Amount, Title, lookUpTitle);

                            try
                            {
                                __IAPIservice.DowmloadInvoice(imagepath, destinationPath, VendorKey, Email, CardHoldername, Title, resources.Amount, addressline1, addressline2, zip, city, state);
                            }
                            catch(Exception ex)
                            {

                            }
                           
                        }
                        return "done";
                    }
                    else
                    {
                        __IAPIservice.Insertpaymetfail(RefrenceNumber, resources.Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                          string Remarks = "Paymentfail";
                        __IAPIservice.InsertIntoErrorLog(Remarks);
                    }
                }
                catch (Exception ex)
                {
                    Common.Error.WriteErrorsToFile(ex.Message.ToString());
                }
            }
            else
            {
                return "NotFound";
            }
            return "NotDone";
        }

        public async Task<IHttpActionResult> BidFinePayment()
        {
            string destinationPath = Server.MapPath("~/Document/Invoices/");
            string imagepath = Server.MapPath("~/");

            APIModel registrationmodel = new APIModel();
            string value1 = "";
            try
            {
                IList<APIModel> lstuser = null;
                string lookUpTitle = "Bid Fine";
                lstuser = __IAPIservice.BidFinePayment(lookUpTitle);

                List<System.Web.Mvc.SelectListItem> istusersss = new List<System.Web.Mvc.SelectListItem>();
                for (int i = 0; i < lstuser.Count; i++)
                {
                    string CardNumber = lstuser[i].CardNumber;
                    int Month = lstuser[i].CardExpiryMonth;
                    int Year = lstuser[i].CardExpiryYear;
                    string CVV = lstuser[i].CVV;
                    int value = lstuser[i].Amount*100;
                    string name = lstuser[i].CardHoldername;
                    string addressline1 = lstuser[i].Address1;
                    string addressline2 = lstuser[i].Address2;
                    string zip = lstuser[i].PostalCode;
                    string city = lstuser[i].City;
                    string state = lstuser[i].State;
                    string stripeToken = lstuser[i].stripeToken;
                    string pmId = lstuser[i].PaymentMethodId;
                    int VendorKey = lstuser[i].VendorKey;
                    int PaymentTypeKey = lstuser[i].PaymentTypeKey;
                    string CardHoldername = lstuser[i].CardHoldername;
                    string Title = lstuser[i].Title;
                    string Email = lstuser[i].Email;
                    string RefrenceNumber = lstuser[i].BidvendorId;
                    int BidvendorKey = lstuser[i].BidVendorKey;
                    // value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, value, name, addressline1, addressline2, zip, city, state);
                    value1 = await Common.Payment.PayAsyncNew(value, stripeToken, pmId, Title);
                    if (value1.Contains("Success"))
                    {
                        if (value1.Split('?')[1] != "")
                        {
                            registrationmodel.stripeToken = value1.Split('?')[1];
                            if (value1.Split('?')[0].Trim() == "Success")
                            {
                                __IAPIservice.PaymentmodalInsert(CVV, registrationmodel.stripeToken);
                                __IAPIservice.DowmloadInvoice(imagepath, destinationPath, VendorKey, Email, CardHoldername, Title, lstuser[i].Amount, addressline1, addressline2, zip, city, state);
                                __IAPIservice.Insertpaymet(RefrenceNumber, lstuser[i].Amount, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                                __IAPIservice.UpdateVendorStatus(BidvendorKey);
                                __IAPIservice.WinFeeMain(Email, CardHoldername, lstuser[i].Amount, Title, lookUpTitle);

                            }
                        }
                    }
                    else
                    {
                        __IAPIservice.Insertpaymetfail(RefrenceNumber, value, VendorKey, PaymentTypeKey, registrationmodel.stripeToken);
                        string Remarks = "Paymentfail";
                        __IAPIservice.InsertIntoErrorLog(Remarks);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return null;
            }
        }

        public async Task<JsonResult> BidAbdWorkOrderEmailSent()
        {
            try
            {
                IList<ReportEmailModel> lstuser = null;
                IList<VendorModel> Documentlist = null;
                string lookUpTitle = "BidEmailSent";
                lstuser = __IAPIservice.GetReportList();
                bool status = false;
                for (int i = 0; i < lstuser.Count; i++)
                {
                    lstuser[i].ReportDocumentFilePath = Server.MapPath("~/Documents/Reports/") + lstuser[i].DocumentName + ".docx";
                    //lstuser[i].ReportDocumentFilePath = "../Documents/Reports/" + lstuser[i].DocumentName + ".docx";
                    if (lstuser[i].IncludeCOI == true) 
                    { 
                        string[] VendorList = lstuser[i].VendorList.Split(',');
                        for (int j = 0; j < VendorList.Length - 1; j++)
                        {
                                    

                            if (VendorList[i].Contains("Cbl_"))
                            {
                                VendorList[i] = VendorList[i].Replace("Cbl_", "");
                            }
                          
                            Documentlist = bindDocument12(Convert.ToInt32(VendorList[i]));
                            for (int k = 0; k < Documentlist.Count; k++)
                            {
                                string pathd = Server.MapPath("~/Document/Insurance/" + Convert.ToInt32(VendorList[i]) + "/" + Documentlist[k].Insurance.InsuranceKey + "/" + Documentlist[k].Document.FileName);
                                string fileNamen = pathd;
                                string path = fileNamen;
                                FileInfo file = new FileInfo(path);
                                if (file.Exists)//check file exsit or not  
                                {
                                    lstuser[i].InsuranceDocumentFilePath = path + ",";

                                }
                             
                            }
                        }
                    }
                    
                    status = __IAPIservice.BidAbdWorkOrderEmailSent(lstuser[i]);
                }

                return Json(true);
            }

            catch (Exception ex)
            {
                return Json(false);   
            }
        }

        public IList<VendorModel> bindDocument12(int CompanyKey)
        {
            try
            {
                IList<VendorModel> Documentlist = null;

                Documentlist = __IAPIservice.GetbindDocument12(CompanyKey);
                return Documentlist;
            }
            catch
            {
                return null;
            }
        }

    }
}