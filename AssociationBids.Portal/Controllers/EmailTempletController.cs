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
    public class EmailTemplateController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.Interface.IEmailTemplateService __eMailTemlateService;
        public EmailTemplateController(AssociationBids.Portal.Service.Base.Interface.IEmailTemplateService  emailTempletService )
        {
            this.__eMailTemlateService  = emailTempletService ;
        }
        public ActionResult EmailTemplateList()
        {
            try
            {
                IList<EmailTemplateModel> lEmailTemplate = null;
                lEmailTemplate = __eMailTemlateService.GetAll();
                return View(lEmailTemplate);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View();
            }
            
        }
        
        public JsonResult IndexreEmailTmpPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort,string EmailTite,string EmailType)
        {
            List<EmailTemplateModel> lstEmailTemplate = null;
            lstEmailTemplate = __eMailTemlateService.SearchUser(PageSize, PageIndex, Search.Trim(), Sort, EmailTite, EmailType);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }
        public void FillEmailType()
        {
            try
            {
                IList<LookUpModel> lLookUp = null;
                lLookUp = __eMailTemlateService.GetAllTitle();
                List<System.Web.Mvc.SelectListItem> lLookUpList = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lLookUpList.Add(sli2);
                for (int i = 0; i < lLookUp.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lLookUp[i].Title);
                    sli.Value = Convert.ToString(lLookUp[i].LookUpKey);
                    lLookUpList.Add(sli);
                }
                ViewBag.lststate = lLookUpList;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

        }
        // GET: EmailTemplet
        public ActionResult EmailTemplateAdd()
        {
            //EmailTemplateModel EmailTmpDirectory = new EmailTemplateModel();
            //EmailTmpDirectory.Body = "a";
            FillEmailType();
            return View();
        }
        
        [HttpPost]
        public ActionResult EmailTemplateAdd(EmailTemplateModel  collection, HttpPostedFileBase[] files)
        {
            try
            {
                    Int64 value = 0;
                    value = __eMailTemlateService.Insert(collection);
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Record has been inserted successfully.";
                    }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                    }
                    return RedirectToAction("EmailTemplateList");
                
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("EmailTemplateList");
            }
        }

        public ActionResult EmailTemplateView(int EmailTemplateKey)
        {
            List<EmailTemplateModel> EmailTmpList = null;
            EmailTemplateModel EmailTmpDirectory = new EmailTemplateModel();
            EmailTmpDirectory = __eMailTemlateService.GetDataViewEdit(EmailTemplateKey);

            EmailTmpDirectory.Body = EmailTmpDirectory.Body.Replace("&nbsp;", " ");
            return View(EmailTmpDirectory);
        }
       

        // GET: EditEmailTemplate
        public ActionResult EmailTemplateEdit(int EmailTemplateKey)
        {
            try
            {
                IList<LookUpModel> lLookUp = null;

                lLookUp = __eMailTemlateService.GetAllTitle();
                List<System.Web.Mvc.SelectListItem> lLookUpList = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lLookUpList.Add(sli2);
                for (int i = 0; i < lLookUp.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lLookUp[i].Title);
                    sli.Value = Convert.ToString(lLookUp[i].LookUpKey);
                    lLookUpList.Add(sli);
                }
                ViewBag.lstlookup = lLookUpList;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
                EmailTemplateModel resources = null;
                resources = __eMailTemlateService.GetDataViewEdit(EmailTemplateKey);
                return View(resources);
            }

        [HttpPost]
        public ActionResult EmailTemplateEdit(int EmailTemplateKey, EmailTemplateModel  collection)
        {
            try
            {
             
                Int64 value = 0;
                EmailTemplateModel resources = null;
                    resources = __eMailTemlateService.GetDataViewEdit(EmailTemplateKey);              
                  
                    
                value = __eMailTemlateService.EmailTempletEdit(collection,resources.lookUpType);
                    if (value == 0)
                    {
                        TempData["SuccessMessage"] = "Record has been  updated successfully.";
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Error";
                    }

                return RedirectToAction("EmailTemplateView", new { EmailTemplateKey });
        
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("EmailTemplateList");
            }
        }

        public ActionResult EmailTempletupdates(int EmailTemplateKey)
        {
            try
            {
                IList<LookUpModel> lLookUp = null;
                lLookUp = __eMailTemlateService.GetAllTitle();
                List<System.Web.Mvc.SelectListItem> lLookUpList = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lLookUpList.Add(sli2);
                for (int i = 0; i < lLookUp.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lLookUp[i].Title);
                    sli.Value = Convert.ToString(lLookUp[i].LookUpKey);
                    lLookUpList.Add(sli);
                }
                ViewBag.lststate = lLookUpList;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
          

            return View();
        }
        [HttpPost]
        public ActionResult EmailTempletupdates(int EmailTemplateKey, EmailTemplateModel  collection)
        {
            try
            {
                Int64 value = 0;
                value = __eMailTemlateService.EmailTempletupdates(collection);
                if (value != 0)
                {
                    TempData["Sucessmessage"] = "Record has been inserted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("EmailTemplateList");

            }
            catch (Exception ex)
            {
                return View("EmailTemplateList");
            }
        }

        public ActionResult Delete(Int32 EmailTemplateKey)
        {
            try
            {
                bool Status = false;
                Status = __eMailTemlateService.Remove(EmailTemplateKey);
                if (Status == true)
                {
                    TempData["Sucessmessage"] = "Record has been deleted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
           
            return RedirectToAction("EmailTemplateList");

        }
       
    }
}