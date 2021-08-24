using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
//using INewPricingService = AssociationBids.Portal.Service.Base.Interface.

namespace AssociationBids.Portal.Controllers
{
    public class NewPricingController : Controller
    {
        private readonly INewPricingService _NewPricingservice;

        public NewPricingController(INewPricingService NewPricingService)
        {
            this._NewPricingservice = NewPricingService;
        }

        //Add Pricing
        public ActionResult NewPricingAdd()
        {
            FillCompny();
            FillPricingType();
            return View();
        }

        public void FillCompny()
        {
            try
            {
                IList<LookUpModel> lLookUp = null;
                lLookUp = _NewPricingservice.GetAllTitle();
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
                ViewBag.lstcompany = lLookUpList;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

        }

        public void FillCompnySearch()
        {
            try
            {
                IList<LookUpModel> lLookUp = null;
                lLookUp = _NewPricingservice.GetAllTitle();
                List<System.Web.Mvc.SelectListItem> lLookUpList = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select Company--";
                sli2.Value = "0";
                lLookUpList.Add(sli2);
                for (int i = 0; i < lLookUp.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lLookUp[i].Title);
                    sli.Value = Convert.ToString(lLookUp[i].LookUpKey);
                    lLookUpList.Add(sli);
                }
                ViewBag.lstcompany = lLookUpList;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

        }

        public void FillPricingType()
        {
            try
            {
                IList<LookUpModel> lLookUp = null;
                lLookUp = _NewPricingservice.GetAllLookUpTitle("Pricing Type");
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
                ViewBag.lstPricingType = lLookUpList;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

        }

        [HttpPost]
        public ActionResult NewPricingAdd(PricingModel collection)
        {
            try
            {
                Int64 value = 0;
                if (collection.PricingTypeKey != 1202)
                {
                    collection.StartAmount = 0;
                    collection.EndAmount = 0;
                }
                collection.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                value = _NewPricingservice.Insert(collection);
                if (value >= 0)
                {
                    TempData["Sucessmessage"] = "Record has been inserted successfully.";
                                        
                    string url = string.Format("/NewPricing/NewPricingView?PricingKey={0}", value);
                    //return Redirect(url);
                    System.Web.Routing.RouteValueDictionary rd = new System.Web.Routing.RouteValueDictionary();
                    rd.Add("PricingKey", value);
                    return RedirectToAction("NewPricingView", "NewPricing", rd);
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                    return RedirectToAction("NewPricingList");
                }


            }
            catch (Exception ex)
            {
                return RedirectToAction("NewPricingList");
            }

        }


        //Pricing List
        public ActionResult NewPricingList()
        {
            FillPricingType();
            FillCompnySearch();
            return View();
        }

        public JsonResult IndexPricingPaging(Int64 PageSize, Int64 PageIndex, string Search, String  Sort)
        {
            List<PricingModel> lstPricing = null;

            try
            {         
               
                lstPricing = _NewPricingservice.SearchPricing(PageSize, PageIndex, Search, Sort);
                return Json(lstPricing, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(lstPricing, JsonRequestBehavior.AllowGet);
            }
            
        }


        //Pricing View ()
        public ActionResult NewPricingView(int PricingKey)
        {
            List<PricingModel> PrincingList = null;
            PricingModel PricingDirectory = new PricingModel();
            PricingDirectory = _NewPricingservice.GetDataViewEdit(PricingKey);
            return View(PricingDirectory);
        }

        [HttpPost]
        public ActionResult NewPricingView(PricingModel collection)
        {
            try
            {
                collection.CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
                Int64 value = 0;


                value = _NewPricingservice.PricingEdit(collection);
                if (value != 0)
                {
                    TempData["Sucessmessage"] = "Record has been  successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("NewPricingList");
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("NewPricingList");
            }

        }

        //Pricing Edit Page
        public ActionResult NewPricingEdit(int PricingKey)
        {

            FillCompny();
            FillPricingType();


            PricingModel resources = null;
            resources = _NewPricingservice.GetDataViewEdit(PricingKey);
            return View(resources);
        }

        [HttpPost]
        public ActionResult NewPricingEdit(int PricingKey, PricingModel collection)
        {
            PricingModel pricing = new PricingModel();
            try
            {
                Int64 value = 0;
                if(collection.PricingTypeKey != 1202)
                {
                    collection.StartAmount = 0;
                    collection.EndAmount = 0;
                }
                value = _NewPricingservice.PricingEdit(collection);
                if (value == 0)
                {
                    TempData["SuccessMessage"] = "Record has been updated successfully.";
                    return RedirectToAction("NewPricingView", new { PricingKey });
                   

                }
            
                else
                {
                    ViewData["ErrorMessage"] = "Error";


                    return RedirectToAction("NewPricingView", new { PricingKey });

                    //return Content("<script language='javascript' type='text/javascript'>alert('Service Title Already Exit!');window.location = '/ServiceTmp/ServiceList';</script>");
                }
               
            }
            catch (Exception ex)
            {

                //Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return RedirectToAction("NewPricingView", new { PricingKey });
            }
        }

        public JsonResult GetFeeTypeFromPricingType(int PricingTypeKey,string PricingType)
        {
            try
            {
                ILookUpService s = new LookUpService();
                var t = s.Get(PricingTypeKey);
                var title = "";
                if (t != null)
                    title = t.Title;
                List<SelectListItem> sList = new List<SelectListItem>();
                SelectListItem item = new SelectListItem();
                item.Value = "Fixed";
                item.Text = "Fixed Fee";
                sList.Add(item);
                if (title == "Bid Fee")
                {
                    item = new SelectListItem();
                    item.Value = "Percentage";
                    item.Text = "Percentage Fee";
                    sList.Add(item);
                }
                return Json(sList,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
            
        } 

        public ActionResult Delete(Int32 PricingKey)
        {
            try
            {

                bool value = false;
                value = _NewPricingservice.Delete(PricingKey);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been deleted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("NewPricingList");
            }

            catch (Exception ex)
            {

                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return RedirectToAction("NewPricingList");
            }

        }
    }
}