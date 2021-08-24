using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ServiceModel = AssociationBids.Portal.Model.ServiceModel;

namespace AssociationBids.Portal.Controllers
{
    public class ServiceTmpController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.Interface.IServiceService _sErviceService;
        public ServiceTmpController(AssociationBids.Portal.Service.Base.Interface.IServiceService ServiceService)
        {
            this._sErviceService = ServiceService;
        }
        public ActionResult ServiceList()
        {
            IList<ServiceModel> Service = null;
            Service = _sErviceService.GetAll();
            return View(Service);
        }

        public JsonResult IndexreServicePaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            List<ServiceModel> lstService = null;
            lstService = _sErviceService.SearchUser(PageSize, PageIndex, Search.Trim(), Sort);
            return Json(lstService, JsonRequestBehavior.AllowGet);
        }

        // GET: Service
        public ActionResult ServiceAdd()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ServiceAdd(ServiceModel collection, HttpPostedFileBase[] files)
        {
            try
            {
                    Int64 value = 0;
                value = _sErviceService.Insert(collection);
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Record has been inserted successfully.";
                    }
                    else
                    {
                        //ViewData["Errormessage"] = "Error";
                        //TempData["Sucessmessage"] = " Service Title Already Exit" ;
                        return Content("<script language='javascript' type='text/javascript'>alert('Service Title Already Exit!');window.location = '/ServiceTmp/ServiceList';</script>");
                    }

                
                return RedirectToAction("ServiceList");
            }
             
            catch (Exception ex)
            {

                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("ServiceList");
            }
          

        }

        public ActionResult ServiceView(int ServiceKey)
        {
            List<ServiceModel> ServiceList = null;
            ServiceModel Service = new ServiceModel();
            Service = _sErviceService.GetDataViewEdit(ServiceKey);
            return View(Service);

        }
        [HttpPost]
        public ActionResult ServiceView(ServiceModel collection)
        {
            try
            {
                Int64 value = 0;


                value = _sErviceService.ServiceEdit(collection);
                if (value != 0)
                {
                    TempData["Sucessmessage"] = "Record has been  successfully.";
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

        // GET: EditService
        public ActionResult ServiceEdit(int ServiceKey)
        {

            List<ServiceModel> ServiceList = null;
            ServiceModel Service = new ServiceModel();
            Service = _sErviceService.GetDataViewEdit(ServiceKey);
            return View(Service);

        }

        [HttpPost]
        public ActionResult ServiceEdit(int ServiceKey, ServiceModel collection)
        {
            try
            {
                    Int64 value = 0;


                    value = _sErviceService.ServiceEdit(collection);
                    if (value != 0)
                    {
                        TempData["SuccessMessage"] = "Record has been  updated successfully.";
                    }
                    else
                    {
                        ViewData["Errormessage"] = "Error";
                        return Content("<script language='javascript' type='text/javascript'>alert('Service Title Already Exit!');window.location = '/ServiceTmp/ServiceList';</script>");
                    }
                return RedirectToAction("ServiceView", new { ServiceKey });
            }
            catch (Exception ex)
            {

                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("ServiceList");
            }
        }

        public ActionResult Delete(Int32 ServiceKey)
        {
            try
            {

                bool value = false;
                value = _sErviceService.Delete(ServiceKey);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been deleted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("ServiceList");
            }

            catch(Exception ex)
            {

                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return RedirectToAction("ServiceList");
            }

        }
    }
}