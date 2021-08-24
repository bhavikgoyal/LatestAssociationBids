using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace AssociationBids.Portal.Controllers
{
    public class UserController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IResourceService _resourceservice;

        public UserController(AssociationBids.Portal.Service.Base.IResourceService resourceService)
        {
            this._resourceservice = resourceService;
        }
        
        // GET: User
        public ActionResult UserList()
        {
         
                return View();
            
            
        }
        public ActionResult Delete(int ResourceKey)
        {
            bool value = false;
            value = _resourceservice.Remove(ResourceKey);
            if (value == true)
            {
                TempData["Sucessmessage"] = "Record has been deleted successfully.";
            }
            else
            {
                ViewData["Errormessage"] = "Error";
            }
            return RedirectToAction("UserList");
        }
        public JsonResult IndexreUserPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            List<ResourceModel> lstuser = null;
            lstuser = _resourceservice.SearchUser(PageSize, PageIndex, Search.Trim(), Sort);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }

        // GET: EditUser
        public ActionResult UserEdit(int ResourceKey)
        {
            IList<ResourceModel> lstcompanylist = null;
            IList<ResourceModel> lststate = null;
            //list of state
            var lststatus = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="Pending"},
              new SelectListItem{ Value="2",Text="Approve"},
              new SelectListItem{ Value="3",Text="Unapprove"},
            };
            ViewBag.lststatus = lststatus;

            //list of company
            List<System.Web.Mvc.SelectListItem> lstcompany = new List<System.Web.Mvc.SelectListItem>();
            lstcompanylist = _resourceservice.GetAllCompany();
            System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
            sli1.Text = "--Please Select--";
            sli1.Value = "0";
            lstcompany.Add(sli1);
            for (int i = 0; i < lstcompanylist.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstcompanylist[i].Company);
                sli.Value = Convert.ToString(lstcompanylist[i].CompanyKey);
                lstcompany.Add(sli);
            }
            ViewBag.lstcompany = lstcompany;

            //list of state
            lststate = _resourceservice.GetAllState();
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }
            ViewBag.lststate = lststatelist;
            ResourceModel resources = null;
            resources = _resourceservice.GetDataViewEdit(ResourceKey);
            return View(resources);
        }

        [HttpPost]
        public ActionResult UserEdit(ResourceModel collection)
        {
            try
            {
                bool value = false;
                value = _resourceservice.Edit(collection);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been updated successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("UserList");

            
            }
            catch (Exception ex)
            {
                return RedirectToAction("UserList");

            }
           
        }
        // GET: AddUser
        public ActionResult UserAdd()
        {
            IList<ResourceModel> lstcompanylist = null;
            IList<ResourceModel> lststate = null;
            //list of state
            var lststatus = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="Pending"},
              new SelectListItem{ Value="2",Text="Approve"},
              new SelectListItem{ Value="3",Text="Unapprove"},
            };
            ViewBag.lststatus = lststatus;

            //list of company
            List<System.Web.Mvc.SelectListItem> lstcompany = new List<System.Web.Mvc.SelectListItem>();
            lstcompanylist = _resourceservice.GetAllCompany();
            System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
            sli1.Text = "--Please Select--";
            sli1.Value = "0";
            lstcompany.Add(sli1);
            for (int i = 0; i < lstcompanylist.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstcompanylist[i].Company);
                sli.Value = Convert.ToString(lstcompanylist[i].CompanyKey);
                lstcompany.Add(sli);
            }
            ViewBag.lstcompany = lstcompany;

            //list of state
            lststate = _resourceservice.GetAllState();
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }
            ViewBag.lststate = lststatelist;

            return View();
        }

        [HttpPost]
        public ActionResult UserAdd(ResourceModel collection)
        {
            try
            {

                bool value = false;                
                value = _resourceservice.Insert(collection);
                if (value == true)
                {
                    TempData["Sucessmessage"] = "Record has been inserted successfully.";
                }
                else
                {
                    ViewData["Errormessage"] = "Error";
                }
                return RedirectToAction("UserList");

            }
            catch (Exception ex)
            {
                return View("UserList");
            }

        }
        // GET: ViewUser
        public ActionResult UserView(int ResourceKey)
        {
            ResourceModel resources = null;
            resources = _resourceservice.GetDataViewEdit(ResourceKey);
            if(resources.Status == 1)
            {
                resources.StatusValue = "Pending";
            }
            else if(resources.Status == 2)
            {
                resources.StatusValue = "Approve";
            }
            else if (resources.Status == 3)
            {
                resources.StatusValue = "Unapprove";
            }
            return View(resources);
        
        }
    }
}