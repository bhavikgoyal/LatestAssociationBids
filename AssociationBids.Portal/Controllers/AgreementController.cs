using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EmailFilterModel = AssociationBids.Portal.Model.EmailFilterModel;

namespace AssociationBids.Portal.Controllers
{
    public class AgreementController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.Interface.IAgreementService __aGreementService;
        public AgreementController(AssociationBids.Portal.Service.Base.Interface.IAgreementService AgreementService)
       
        {
            this.__aGreementService = AgreementService;
        }
        public ActionResult AgreementList()
        {
            try
            {
                IList<AgreementModel> lAgreement = null;
                lAgreement = __aGreementService.GetAll();
                return View(lAgreement);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View();
            }
            
        }
        public JsonResult IndexreAgreementPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            
                List<AgreementModel> lstEmailTemplate = null;
                lstEmailTemplate = __aGreementService.SearchUser(PageSize, PageIndex, Search.Trim(), Sort);
                return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexreAgreementPagingForAdvancedSearch(Int64 PageSize, Int64 PageIndex, string Search, string Status,String Sort)
        {

            List<AgreementModel> lstEmailTemplate = null;
            lstEmailTemplate = __aGreementService.AdvancedSearchAgreement(PageSize, PageIndex, Search.Trim(), Status.Trim(), Sort);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        // GET: Agreement
        public ActionResult AgreementAdd()
        {
            //list of Status
            FillStatus();
            return View();
        }
        [HttpPost]
        public ActionResult AgreementAdd(AgreementModel collection, HttpPostedFileBase[] files)
        {
            try
            {
                    int value = 0;
                    value = __aGreementService.Insert(collection);
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Record has been inserted successfully.";
                    AgreementModel Agreementlist = new AgreementModel();
                    int AgreementKey = value;
                    Agreementlist = __aGreementService.GetDataViewEdit(AgreementKey);
                    return View("AgreementView", Agreementlist);
                }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                    }
                
                return RedirectToAction("AgreementList");

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("AgreementList");
            }
        }
        public ActionResult AgreementView(int AgreementKey )
        {
            List<AgreementModel> AgreementList = null;
            AgreementModel Agreementlist = new AgreementModel();
            Agreementlist = __aGreementService.GetDataViewEdit(AgreementKey);
            Agreementlist.Description = Agreementlist.Description.Replace("\r\n", "<br>");
             
            return View(Agreementlist);
        }
        [HttpPost]
        public ActionResult AgreementView(AgreementModel collection)
        {
            try
            {
                Int64 value = 0;


                value = __aGreementService.AgreementEdit(collection);
                if (value != 0)
                {
                    TempData["SuccessMessage"] = "Record has been Updated successfully.";
                }
                else
                {
                    ViewData["ErrorMessage"] = "Error";
                }
                return RedirectToAction("AgreementList");
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("AgreementList");
            }

        }
        //// GET: EditAgreement
        public ActionResult AgreementEdit(int AgreementKey)
        {
            try
            {
                FillStatus();
                List<AgreementModel> AgreementList = null;
                AgreementModel Agreementlist = new AgreementModel();
                Agreementlist = __aGreementService.GetDataViewEdit(AgreementKey);
                return View(Agreementlist);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return View();

        }
        [HttpPost]
        public ActionResult AgreementEdit(int AgreementKey, AgreementModel collection)
        {
            try
            {
                    Int64 value = 0;
                collection.AgreementDate = Convert.ToDateTime(collection.AgreementDates);

                    value = __aGreementService.AgreementEdit(collection);
                    if (value == 0)
                    {
                        TempData["SuccessMessage"] = "Record has been  updated successfully.";
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Error";
                    }
                AgreementModel Agreementlist = new AgreementModel();
                Agreementlist = __aGreementService.GetDataViewEdit(AgreementKey);
              
                return RedirectToAction("AgreementView", new { AgreementKey });
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return RedirectToAction("AgreementView", new { AgreementKey });
            }
        }
        public ActionResult Delete(Int32 AgreementKey)
        {
            try
            {

                bool value = false;
                value = __aGreementService.Delete(AgreementKey);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been deleted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("AgreementList");
            }

            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return RedirectToAction("AgreementList");
            }

        }
        public void FillStatus()
        {
            try
            {
                var lststatus = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Choose one",Selected=true},
              new SelectListItem{ Value="1",Text="Active"},
              new SelectListItem{ Value="2",Text="Inactive"},
            };
                ViewBag.lststatus = lststatus;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
           
        }
    }
}