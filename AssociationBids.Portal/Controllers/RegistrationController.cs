using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Service.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class RegistrationController : Controller
    {

        private IVendorPolicyService _vendorPolicy;
        private readonly IRegistrationService _registrationservice;
        private readonly AssociationBids.Portal.Service.Base.Interface.IVendorManagerService __vendorManagerservice;
        
        public RegistrationController(IVendorManagerService vendorManagerService, IRegistrationService registrationService, IVendorPolicyService policyService)
        {
            this._registrationservice = registrationService;
            this.__vendorManagerservice = vendorManagerService;
            _vendorPolicy = policyService;
        }

        // GET: Registration
        public ActionResult Registration(int CompanyKey = 0)
        {

            RegistrationModel registrationmodel = new RegistrationModel();
            bool CheckLinkExpired = false;
            try
            {
                CheckLinkExpired = _registrationservice.GetLinkExpiredCheck(CompanyKey);
                if (CheckLinkExpired == true && CompanyKey != 0)
                {
                    return RedirectToAction("LinkExpired");
                }
                else
                {
                    registrationmodel.CompanyKey = CompanyKey;
                    //state
                    IList<RegistrationModel> lstregistration = null;
                    lstregistration = _registrationservice.GetAllState();
                    List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                    System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                    sli2.Text = "--Please Select--";
                    sli2.Value = "0";
                    lststatelist.Add(sli2);
                    for (int i = 0; i < lstregistration.Count; i++)
                    {
                        System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                        sli.Text = Convert.ToString(lstregistration[i].State);
                        sli.Value = Convert.ToString(lstregistration[i].StateKey);
                        lststatelist.Add(sli);
                    }
                    ViewBag.lststate = lststatelist;
                    //Service
                    lstregistration = _registrationservice.GetAllService();
                    List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();
                    SelectListItem sServ = new SelectListItem();
                    sServ.Text = "--- Select Service ---";
                    sServ.Value = "0";
                    lstservicelist.Add(sServ);
                    for (int i = 0; i < lstregistration.Count; i++)
                    {
                        System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                        sli.Text = Convert.ToString(lstregistration[i].ServiceTitle1);
                        sli.Value = Convert.ToString(lstregistration[i].ServiceKey);
                        lstservicelist.Add(sli);
                    }
                    ViewBag.lstservice = lstservicelist;
                    //Radius
                    var lstRadiuslist = new List<SelectListItem>()
                    {

                      new SelectListItem{ Value="1",Text="10 miles",Selected=true},
                      new SelectListItem{ Value="2",Text="15 miles"},
                      new SelectListItem{ Value="3",Text="20 miles"},
                      new SelectListItem{ Value="4",Text="25 miles"},
                    };
                    ViewBag.lstRadius = lstRadiuslist;
                    return View(registrationmodel);
                }
            }

            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(registrationmodel);

            }
        
        }

        [HttpGet]
        public ActionResult RegistrationVendor(int CompanyKey)
        {

            RegistrationModel registrationmodel = new RegistrationModel();
            try
            {
                //state

                IList<RegistrationModel> lstregistration = null;
                lstregistration = _registrationservice.GetAllState();
                List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lststatelist.Add(sli2);
                for (int i = 0; i < lstregistration.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstregistration[i].State);
                    sli.Value = Convert.ToString(lstregistration[i].StateKey);
                    lststatelist.Add(sli);
                }
                ViewBag.lststate = lststatelist;
                //Service
                lstregistration = _registrationservice.GetAllService();
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();

                for (int i = 0; i < lstregistration.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstregistration[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstregistration[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;

                //Radius
                var lstRadiuslist = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="10 miles"},
              new SelectListItem{ Value="2",Text="15 miles"},
              new SelectListItem{ Value="3",Text="20 miles"},
              new SelectListItem{ Value="4",Text="25 miles"},
            };
                ViewBag.lstRadius = lstRadiuslist;

                registrationmodel = _registrationservice.Getvendordetails(CompanyKey);

                return View(registrationmodel);

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(registrationmodel);

            }

        }

        
        public JsonResult GetVendorDetails(int CompanyKey = 0)
        {
            int Length = 0;
            var details = _registrationservice.Getvendordetails(CompanyKey);
            if (details != null)
            {
                Length = details.ServiceTitle1.Split(',').Length; 
            }
            
            if (Length == 3)
            {
                details.ServiceTitle2 = details.ServiceTitle1.Split(',')[1];
                details.ServiceTitle3 = details.ServiceTitle1.Split(',')[2];
                details.ServiceTitle1 = details.ServiceTitle1.Split(',')[0];
            }
            else if (Length == 2)
            {
                details.ServiceTitle2 = details.ServiceTitle1.Split(',')[1];
                details.ServiceTitle1 = details.ServiceTitle1.Split(',')[0];
            }
            else if(Length == 1)
            {
                details.ServiceTitle1 = details.ServiceTitle1.Split(',')[0];
            }
            
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GeAgreementDetails(int CompanyKey = 0)
        {
            //if (CompanyKey == 0)
                //return Json(null, JsonRequestBehavior.AllowGet);
            var details = _registrationservice.GeAgreementDetails(CompanyKey);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult>  Registration(int CompanyKey,RegistrationModel registrationModel, HttpPostedFileBase[] Insurancefiles)
        {
            RegistrationModel registrationmodel = new RegistrationModel();
            try
            {
                int ser1 = Convert.ToInt32(Request["Service1"]);
                int ser2 = Convert.ToInt32(Request["Service2"]);
                int ser3 = Convert.ToInt32(Request["Service3"]);
                if(ser1 == 0 && ser2 == 0 && ser3 == 0)
                {
                    return RedirectToAction("Registration");
                }
                bool isFirst = true;
                if (ser1 != 0)
                {
                    registrationModel.ServiceTitle1 = ser1.ToString();
                    isFirst = false;
                }

                if (ser2 != 0)
                {
                    if (isFirst)
                        registrationModel.ServiceTitle1 = ser2.ToString();
                    else
                        registrationModel.ServiceTitle1 += "," + ser2;
                    isFirst = false;
                }
                if (ser3 != 0)
                {
                    if (isFirst)
                        registrationModel.ServiceTitle1 = ser3.ToString();
                    else
                        registrationModel.ServiceTitle1 += "," + ser3;
                    isFirst = false;
                }
                Int64 value = 0;
                registrationmodel.CompanyKey = CompanyKey;
                try
                {
                    registrationmodel.Resourcekey = Convert.ToInt32(Request["ResourceKey"]);
                }
                catch { }
                //if (Insurancefiles != null && licensefiles != null)
                //{
                //    if (Insurancefiles.FileName != "" && licensefiles.FileName != "")
                //    {
                //        registrationModel.FileName = Insurancefiles.FileName + '?' + licensefiles.FileName;
                //        if (Insurancefiles.ContentLength != 0 && licensefiles.ContentLength != 0)
                //        {
                //            registrationModel.FileSize = Convert.ToString(Insurancefiles.ContentLength) + ',' + Convert.ToString(licensefiles.ContentLength);
                //        }
                //    }
                //}
                //registrationModel.CardNumber = Convert.ToString(registrationModel.CardNumber.Trim().Substring(registrationModel.CardNumber.Trim().Length - 4));


                if (registrationModel.CardNumber != null && registrationModel.CardNumber != "")
                {
                    string[] splitCardNumber = registrationModel.CardNumber.Trim().Split(' ');
                    string maskedcard = "";
                    for (int i = 0; i < splitCardNumber.Length - 1; i++)
                        maskedcard += "XXXX";
                    maskedcard += splitCardNumber[splitCardNumber.Length - 1];
                    registrationModel.CardNumber = maskedcard;

                }
              
               

                if (registrationModel.CardNumber != null && registrationModel.CardNumber != "")
                {
                    registrationModel.StripeTokenID = Session["StripeTokenId"].ToString();
                    registrationModel.PMId = Session["PMId"].ToString();
                    value = _registrationservice.Insert(registrationModel);

                    if (value != 0)
                    {
                        try
                        {
                            //if (Insurancefiles.Length > 0 && Insurancefiles[0] != null)
                            //{
                            if (registrationModel.PolicyNumber != null && registrationModel.PolicyNumber != "")
                            {
                                long ikey = 0;
                                IVendorManagerService vendorManagerService = new VendorManagerService();
                                InsuranceModel insurance = new InsuranceModel();
                                insurance.VendorKey = Convert.ToInt32(value);
                                insurance.CompanyName = registrationModel.CompanyName;
                                insurance.PolicyNumber = registrationModel.PolicyNumber;
                                insurance.InsuranceKey = Convert.ToInt32(registrationModel.InsuranceKey);
                                insurance.InsuranceAmount = registrationModel.InsuranceAmount;
                                insurance.StartDate = registrationModel.StartDate;
                                insurance.EndDate = registrationModel.EndDate;
                                insurance.RenewalDate = registrationModel.RenewalDate;
                                insurance.Address = registrationModel.Address;
                                insurance.Address2 = registrationModel.Address2;
                                insurance.Work = registrationModel.Work;
                                insurance.City = registrationModel.City;
                                insurance.Email = registrationModel.EmailId;
                                insurance.Fax = registrationModel.Fax;
                                insurance.State = registrationModel.State;
                                insurance.Zip = registrationModel.Zip;

                                if (insurance.StartDate.Year < 2000)
                                    insurance.StartDate = new DateTime(2001, 01, 01);
                                if (insurance.EndDate.Year < 2000)
                                    insurance.EndDate = new DateTime(2001, 01, 01);

                                if (insurance.InsuranceKey == 0)
                                {
                                    ikey = vendorManagerService.VendorManagerAddInsurance(insurance);
                                }
                                else
                                {
                                    ikey = _vendorPolicy.VendorManagerEditInsurance(insurance);
                                    ikey = insurance.InsuranceKey;
                                }

                                if (Insurancefiles != null && ikey != 0)
                                {
                                    IDocumentService document = new DocumentService();
                                    foreach (var file in Insurancefiles)
                                    {
                                        var module = new ModuleService().GetAll(new ModuleFilterModel());
                                        var key = module.Where(w => w.Title == "Insurance").FirstOrDefault().ModuleKey;
                                        DocumentModel dm = new DocumentModel();
                                        dm.ObjectKey = Convert.ToInt32(ikey);
                                        dm.ModuleKey = key;
                                        if (file != null)
                                        {
                                            dm.FileName = file.FileName;
                                            dm.FileSize = file.ContentLength;
                                            dm.LastModificationTime = DateTime.Now;
                                            var st = document.Create(dm);
                                            if (st)
                                            {
                                                //Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + value + "/" + ikey));
                                                //file.SaveAs(Server.MapPath("~/Document/Insurance/" + value + "/" + ikey + "/") + file.FileName);
                                                Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + ikey));
                                                file.SaveAs(Server.MapPath("~/Document/Insurance/" + ikey + file.FileName));
                                            }
                                        }
                                    }
                                }

                            }

                            //}
                        }
                        catch
                        {

                        }
                        //if (Insurancefiles != null)
                        //{
                        //    string path = Path.Combine(Server.MapPath("~/Document/Insurance/"), value + " " + Insurancefiles.FileName);
                        //    Insurancefiles.SaveAs(path);
                        //}
                        //else if (licensefiles != null)
                        //{
                        //    string path = Path.Combine(Server.MapPath("~/Document/license/"), value + " " + licensefiles.FileName);
                        //    licensefiles.SaveAs(path);
                        //}
                        try
                        {
                            Session.Remove("StripeTokenId");
                            Session.Remove("PMId");
                        }
                        catch { }
                        return RedirectToAction("Thankyou");

                    }

                   
                 

                }
                



                if (registrationModel.CardNumber == null || registrationModel.CardNumber == "")
                {
                    Int64 value1 = 0;
                    int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                    VendorManagerVendorModel collection = new VendorManagerVendorModel();
                    collection.LegalName = registrationModel.LegalName;
                    collection.CompanyName = registrationModel.CompanyName;
                    collection.TaxID = registrationModel.TaxID;
                    collection.ServiceTitle1 = registrationModel.ServiceTitle1;
                    collection.Address = registrationModel.Address;
                    collection.Address2 = registrationModel.Address2;
                    collection.City = registrationModel.City;
                    collection.Zip = registrationModel.Zip;
                    collection.ContactPerson = registrationModel.FirstName.Trim() + " " + registrationModel.LastName.Trim();
                    collection.FirstName = registrationModel.FirstName.Trim();
                    collection.LastName = registrationModel.LastName.Trim();
                    collection.Work = registrationModel.Work;
                    collection.Work2 = registrationModel.Work2;
                    collection.Email = registrationModel.EmailId;
                    collection.Fax = registrationModel.Fax;
                    collection.Website = registrationModel.Website;
                    collection.State = registrationModel.State;
                    collection.Title = "";
                    collection.CompanyKey = registrationModel.CompanyKey;
                    collection.Description = "";
                    collection.Radius =Convert.ToInt32(registrationModel.Radius);
                    collection.Latitude = registrationModel.Latitude;
                    collection.Longitude = registrationModel.Longitude;

                    value1 = __vendorManagerservice.RegistrationManagerInviteVendor(collection, ResourceKey);
                    //value1 = __vendorManagerservice.VendorManagerInviteVendor(collection, ResourceKey);
                    if (value1 != 0)
                    {
                        try
                        {
                            //if (Insurancefiles.Length > 0 && Insurancefiles[0] != null)
                            //{
                            if (registrationModel.PolicyNumber != null && registrationModel.PolicyNumber != "")
                            {
                                long ikey = 0;
                                IVendorManagerService vendorManagerService = new VendorManagerService();
                                InsuranceModel insurance = new InsuranceModel();
                                insurance.VendorKey = Convert.ToInt32(value1);
                                insurance.CompanyName = registrationModel.CompanyName;
                                insurance.InsuranceKey = Convert.ToInt32(registrationModel.InsuranceKey);
                                insurance.PolicyNumber = registrationModel.PolicyNumber;
                                insurance.InsuranceAmount = registrationModel.InsuranceAmount;
                                insurance.StartDate = registrationModel.StartDate;
                                insurance.EndDate = registrationModel.EndDate;
                                insurance.RenewalDate = registrationModel.RenewalDate;
                                insurance.Address = registrationModel.Address;
                                insurance.Address2 = registrationModel.Address2;
                                insurance.Work = registrationModel.Work;
                                insurance.City = registrationModel.City;
                                insurance.Email = registrationModel.EmailId;
                                insurance.Fax = registrationModel.Fax;
                                insurance.State = registrationModel.State;
                                insurance.Zip = registrationModel.Zip;

                                if (insurance.StartDate.Year < 2000)
                                    insurance.StartDate = new DateTime(2001, 01, 01);
                                if (insurance.EndDate.Year < 2000)
                                    insurance.EndDate = new DateTime(2001, 01, 01);
                                if (insurance.InsuranceKey == 0)
                                {
                                    ikey = vendorManagerService.VendorManagerAddInsurance(insurance);
                                }
                                else
                                {
                                    ikey = _vendorPolicy.VendorManagerEditInsurance(insurance);
                                    ikey = insurance.InsuranceKey;
                                }

                                if (Insurancefiles != null && ikey != 0)
                                {
                                    IDocumentService document = new DocumentService();
                                    foreach (var file in Insurancefiles)
                                    {
                                        var module = new ModuleService().GetAll(new ModuleFilterModel());
                                        var key = module.Where(w => w.Title == "Insurance").FirstOrDefault().ModuleKey;
                                        DocumentModel dm = new DocumentModel();
                                        dm.ObjectKey = Convert.ToInt32(ikey);
                                        dm.ModuleKey = key;
                                        if (file != null)
                                        {
                                            dm.FileName = file.FileName;
                                            dm.FileSize = file.ContentLength;
                                            dm.LastModificationTime = DateTime.Now;
                                            var st = document.Create(dm);
                                            if (st)
                                            {
                                                //Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + value + "/" + ikey));
                                                //file.SaveAs(Server.MapPath("~/Document/Insurance/" + value + "/" + ikey + "/") + file.FileName);
                                                Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + ikey));
                                                file.SaveAs(Server.MapPath("~/Document/Insurance/" + ikey + file.FileName));
                                            }
                                        }
                                    }
                                }

                            }
                           
                            //}
                        }
                        catch
                        {

                        }
                        //if (Insurancefiles != null)
                        //{
                        //    string path = Path.Combine(Server.MapPath("~/Document/Insurance/"), value + " " + Insurancefiles.FileName);
                        //    Insurancefiles.SaveAs(path);
                        //}
                        //else if (licensefiles != null)
                        //{
                        //    string path = Path.Combine(Server.MapPath("~/Document/license/"), value + " " + licensefiles.FileName);
                        //    licensefiles.SaveAs(path);
                        //}
                        try
                        {
                            Session.Remove("StripeTokenId");
                            Session.Remove("PMId");
                        }
                        catch { }
                        return RedirectToAction("RegiestrationInProcess");

                    }

                }




                   
                 



            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());

            }

            registrationmodel.CompanyKey = CompanyKey;
            IList<RegistrationModel> lstregistration = null;
            lstregistration = _registrationservice.GetAllState();
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lstregistration.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstregistration[i].State);
                sli.Value = Convert.ToString(lstregistration[i].StateKey);
                lststatelist.Add(sli);
            }
            ViewBag.lststate = lststatelist;

            //Service

            lstregistration = _registrationservice.GetAllService();
            List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();

            for (int i = 0; i < lstregistration.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstregistration[i].ServiceTitle1);
                sli.Value = Convert.ToString(lstregistration[i].ServiceKey);
                lstservicelist.Add(sli);
            }
            ViewBag.lstservice = lstservicelist;

            //Radius
            var lstRadiuslist = new List<SelectListItem>()
                    {
                    new SelectListItem{ Value="0",Text="Please Select",Selected=true},
                    new SelectListItem{ Value="1",Text="10 miles"},
                    new SelectListItem{ Value="2",Text="15 miles"},
                    new SelectListItem{ Value="3",Text="20 miles"},
                    new SelectListItem{ Value="4",Text="25 miles"},
                    };
            ViewBag.lstRadius = lstRadiuslist;
            ViewData["Errormessage"] = "Error";


            return View(registrationmodel);

        }
        
        public ActionResult Thankyou()
        {
            return View();
        }


        public ActionResult RegiestrationInProcess()
        {
            return View();
        }
        public ActionResult LinkExpired()
        {
            return View();
        }

        public JsonResult IsCompanyNameExits(string Name, int Id)
        {
            int value = this._registrationservice.IsCompanyName(Id, Name);
            return value == 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult IsEmailExits(string Name,int ResourceKey)
        {
            int value = this._registrationservice.IsEmailExist(Name,ResourceKey);
            return value == 1 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);

        }

        public async System.Threading.Tasks.Task<JsonResult> PaymentVerificationAsync(string Fname, string Lname ,string CardNumber, int Month, int Year, string CVV, string value, string name, string addressline1, string addressline2, string zip, string city, string state)
        {
            RegistrationModel registrationmodel = new RegistrationModel();
            string value1 = "";

            bool value2 = false;

            string Value3 = "";
            try
            {
                
                //value1 = await Common.Payment.PayAsync(CardNumber, Month, Year, CVV, 1, name, addressline1, addressline2, zip, city, state);
                string st = await Common.Payment.GenerateTokenForCC(CardNumber, Month, Year, CVV, name, addressline1, addressline2, zip, city, state);
                if (st != "failed")
                {
                    string tokenID = st.Split('=')[1].Split('&')[0];
                    string PmId = st.Split('=')[2];
                    registrationmodel.StripeTokenID = tokenID;
                    value1 = await Common.Payment.PayAsyncNew(50, registrationmodel.StripeTokenID,PmId,"");
                


                    if (value1.Contains("Success"))
                    {
                        if (value1.Split('?')[1] != "")
                        {
                            if (value1.Split('?')[0].Trim() == "Success")
                            {


                                string VID = value1.Split('?')[1];
                               
                                Session.Add("StripeTokenId", registrationmodel.StripeTokenID);
                                Session.Add("PMId", PmId);

                                Value3 = await Common.Payment.RefundPayAsync(VID);
                                //value2 = _registrationservice.PaymentInsert(registrationmodel, CardNumber, Month, Year, CVV, Fname, Lname);
                                value2 = _registrationservice.PaymentInsert_New(registrationmodel.StripeTokenID, CVV);
                                
                            }
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
                return value1.Split('?')[0].Contains("Failed") ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }



        }


        /*  Reset Password  */

        public ActionResult ResetPassword()
        {
            try
            {
                string CurrentURL = System.Web.HttpContext.Current.Request.Url.ToString();
                int UserKey = Convert.ToInt32(CurrentURL.Split('=')[1]);
                ViewBag.ResourceKey = UserKey;
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
            IChangePasswordService _ChangePasswordService = new ChangePasswordService();
            //int UserKey = Convert.ToInt32(CurrentURL.Split('=')[1]);

            //changePasswordModel.UserId = UserKey.ToString();
            int response = _ChangePasswordService.ResetPassword(changePasswordModel);
            if (response == 1)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return Content(String.Format("<script type='text/javascript'>alert('Something went wrong...\\r\\nPlease Try agin later!');window.location = '/Registration/ResetPassword?ResourceKey="+changePasswordModel.UserKey+"';</script>"));
            }
        }

        public JsonResult GetInsuranceDetails(int CompanyKey)
        {
            try
            {
                IList<VendorManagerModel> Documentlist = null;
                //int CompanyKey = Convert.ToInt32(Session["companykey"]);
                Documentlist = _vendorPolicy.GetbindDocumentByInsuranceKey(CompanyKey, 0);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                string[] doc = new string[Documentlist.Count];
                for (int i = 0; i < Documentlist.Count; i++)
                {
                    if (Documentlist[i].Document.FileName != "")
                    {
                        var Text = Convert.ToString(Documentlist[i].Document.FileName);
                        string imagelist = "../Document/Insurance/" + "BidRequestKeyTemp " + Documentlist[i].Document.FileName;
                        if (Documentlist[i].Document.FileName.Contains(Documentlist[i].Insurance.InsuranceKey + " "))
                            imagelist = "../Document/Insurance/" + Documentlist[i].Document.FileName;
                        var path = Server.MapPath(imagelist);

                        string input = Documentlist[i].Document.FileName;
                        string[] values = input.Split('.');
                        var checkext = values[1];

                        doc[i] = imagelist;
                    }

                }
                return Json(Documentlist, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }
        public JsonResult DocumentDelete(int InsuranceKey, string Docname,string CompanyKey)
        {
            try
            {
                string Directory = Server.MapPath("~/Document/Insurance/");
                string fileName = Docname;
                string path = Directory + InsuranceKey + " " + Docname;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                bool value = false;
                value = _vendorPolicy.DocumentDelete(InsuranceKey, Docname);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }


    }
}