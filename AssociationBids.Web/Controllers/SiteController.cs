using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Site;

namespace AssociationBids.Portal.Web
{
    public class SiteController : BaseController
    {
        #region Properties
        private IList<StateModel> __stateList;
        private SiteSettingsModel __siteSettings;
        private IDictionary<string, int> __lookUpSettings;
        protected RouteHelper Route { get; set; }
        protected string HostName { get; set; }
        protected SessionModel CurrentSession;
        protected string PortalID;
        protected string Controller;
        protected string Action;
        protected string SessionID;

        public SiteSettingsModel SiteSettings
        {
            get
            {
                if (__siteSettings == null)
                {
                    __siteSettings = new SiteSettingsModel();
                }

                return __siteSettings;
            }
            set { __siteSettings = value; }
        }

        /// <summary>
        /// Gets the LookUpSettings from Cache and saves it to Cache if it doesn't exist
        /// </summary>
        public IDictionary<string, int> LookUpSettings
        {
            get
            {
                if (__lookUpSettings == null && GetCacheValue("AssociationBids.Portal.LookUpSettings") != null)
                {
                    __lookUpSettings = (IDictionary<string, int>)GetCacheValue("AssociationBids.Portal.LookUpSettings");
                }

                if (__lookUpSettings == null)
                {
                    ILookUpSettingsService service = new LookUpSettingsService();

                    __lookUpSettings = service.GetAll();

                    SetCacheValue("AssociationBids.Portal.LookUpSettings", __lookUpSettings);
                }

                return __lookUpSettings;
            }
        }
        #endregion

        protected T CreateService<T>() where T : new()
        {
            T service = new T();

            ((AssociationBids.Portal.Service.Base.IBaseService)service).SiteSettings = SiteSettings;

            return service;
        }

        #region Controller Events
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            HostName = requestContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority); ;
            base.Initialize(requestContext);
        }
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            PortalID = GetRouteDataValue(filterContext.RouteData, "portal");
            Controller = GetRouteDataValue(filterContext.RouteData, "controller");
            Action = GetRouteDataValue(filterContext.RouteData, "action");
            SessionID = GetRouteDataValue(filterContext.RouteData, "session");

            Route = new RouteHelper(Url, PortalID, Controller);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Load Site Settings - this must be loaded before LoadUser()
            LoadSiteSettings();

            // Setup Session
            CurrentSession = new SessionModel();

            // Setup ViewBag data
            LoadViewBag();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (CurrentSession.IsDirty)
            {
                ISessionService service = new SessionService();

                service.Update(CurrentSession);
            }
        }

        protected void LoadViewBag()
        {
            ViewBag.RouteHelper = Route;

            // Set default access (Read)
            ViewBag.Access = 1;
        }

        protected void LoadSiteSettings()
        {
            SiteSettings = new SiteSettingsModel();

            SiteSettings.LookUpSettings = LookUpSettings;
        }
        #endregion

        #region RenderView & RenderPartialView methods
        protected ActionResult RenderView()
        {
            return RenderView(null);
        }

        protected ActionResult RenderView(string viewName)
        {
            return RenderView(viewName, null);
        }

        protected ActionResult RenderView(object model)
        {
            string action = GetRouteDataValue(Request.RequestContext.RouteData, "action");

            return RenderView(action, model);
        }

        protected ActionResult RenderView(string viewName, object model)
        {
            ViewEngineResult result = null;

            if (!String.IsNullOrEmpty(CurrentSession.ViewExtension))
            {
                result = ViewEngines.Engines.FindView(ControllerContext, viewName + CurrentSession.ViewExtension, null);
            }

            if (result != null && result.View != null)
            {
                return View(result.View, model);
            }
            else
            {
                return View(viewName, model);
            }
        }

        protected ActionResult RenderPartialView(object model)
        {
            string action = GetRouteDataValue(Request.RequestContext.RouteData, "action");

            return RenderPartialView(action, model);
        }

        protected ActionResult RenderPartialView(string viewName, object model)
        {
            ViewEngineResult result = null;

            if (!String.IsNullOrEmpty(CurrentSession.ViewExtension))
            {
                result = ViewEngines.Engines.FindPartialView(ControllerContext, viewName + CurrentSession.ViewExtension);
            }

            if (result != null && result.View != null)
            {
                return PartialView(viewName + CurrentSession.ViewExtension, model);
            }
            else
            {
                return PartialView(viewName, model);
            }
        }
        #endregion

        #region Filter & Paging methods

        protected T GetFilter<T>(T filter) where T : new()
        {
            object sessionFilter = GetSessionValue("AssociationBids.Portal.Filter." + SessionID + "." + Controller);

            if (sessionFilter != null)
            {
                return (T)sessionFilter;
            }
            else
            {
                return filter;
            }
        }

        protected T UpdateFilter<T>(T filter)
        {
            PropertyInfo property = filter.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
            if (null != property && property.CanWrite)
            {
                property.SetValue(filter, "PortalKey", null);
            }
            return filter;
        }

        protected T GetPaging<T>() where T : new()
        {
            object paging = GetSessionValue("AssociationBids.Portal.Paging" + SessionID + "." + Controller);

            if (paging != null)
            {
                return (T)paging;
            }
            else
            {
                return new T();
            }
        }

        protected void ResetPaging()
        {
            PagingModel paging = new PagingModel();

            // Set the updated paging object
            ViewBag.Paging = paging;

            // Set session values
            SetSessionValue("AssociationBids.Portal.Paging" + SessionID + "." + Controller, paging);
        }

        /// <summary>
        /// Saves the filter object to ViewBag and Session.
        /// </summary>
        /// <param name="filter"></param>
        protected void ResetFilterAndPaging(object filter)
        {
            ResetPaging();

            // Set the updated filter objects
            ViewBag.Filter = filter;

            // Set session values
            SetSessionValue("AssociationBids.Portal.Filter." + SessionID + "." + Controller, filter);
        }

        /// <summary>
        /// Saves the paging and filter objects to ViewBag and Session.
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filter"></param>
        /// <param name="enabled"></param>
        protected void SavePagingAndFilter(object paging, object filter, bool enabled)
        {
            // Set the updated paging & filter objects
            ViewBag.Paging = paging;
            ViewBag.Filter = filter;
            ViewBag.FilterEnabled = enabled;

            // Set session values
            SetSessionValue("AssociationBids.Portal.Paging" + SessionID + "." + Controller, paging);
            SetSessionValue("AssociationBids.Portal.Filter." + SessionID + "." + Controller, filter);
        }
        #endregion

        #region Access methods
        public bool HasAccess(LookUpType.ModuleType module, LookUpType.AccessType access)
        {
            // TODO: verify access
            return true;
        }

        public int GetModuleAccess(LookUpType.ModuleType module)
        {
            // TODO: Get access from db
            return 0;
        }
        #endregion

        protected ActionResult RedirectToList()
        {
            return RedirectToAction("List");
        }
        protected ActionResult RedirectToDetails(int id)
        {
            return RedirectToAction("Details", new { ID = id });
        }
        protected ActionResult RedirectToDetails(string controllerName, int id)
        {
            return RedirectToAction("Details", controllerName, new { ID = id });
        }
        public void LogError(Exception e)
        {
            // TODO: Log error in db
        }

        /// <summary>
        /// Gets the States from Cache and saves it to Cache if it doesn't exist
        /// </summary>
        public IList<StateModel> GetStateList()
        {
            if (__stateList == null && GetCacheValue("AssociationBids.Portal.StateList") != null)
            {
                __stateList = (IList<StateModel>)GetCacheValue("AssociationBids.Portal.StateList");
            }

            if (__stateList == null)
            {
                StateService stateService = new StateService();
                __stateList = stateService.GetAll();

                SetCacheValue("AssociationBids.Portal.StateList", __stateList);
            }

            return __stateList;
        }

        protected SelectList GetLookUpList(string lookUpType, int lookUpKey)
        {
            ILookUpService lookUpService = new LookUpService();
            LookUpFilterModel lookUpFilter = new LookUpFilterModel();

            lookUpFilter.LookUpTypeKey = GetLookUpSettings(lookUpType);

            return GetSelectList(lookUpService.GetAll(lookUpFilter), "LookUpKey", "Title", lookUpKey);
        }

        protected SelectList GetLookUpValueList(string lookUpType, int lookUpKey)
        {
            ILookUpService lookUpService = new LookUpService();
            LookUpFilterModel lookUpFilter = new LookUpFilterModel();

            lookUpFilter.LookUpTypeKey = GetLookUpSettings(lookUpType);

            return GetSelectList(lookUpService.GetAll(lookUpFilter), "Value", "Title", lookUpKey);
        }

        protected int GetLookUpSettings(string key)
        {
            return LookUpSettings[key];
        }

        protected SelectList GetSelectList<T>(IList<T> items, string dataValueField, string dataTextField)
        {
            return GetSelectList(items, dataValueField, dataTextField, null);
        }

        protected SelectList GetSelectList<T>(IList<T> items, string dataValueField, string dataTextField, object selectedValue)
        {
            if (items == null)
            {
                return null;
            }
            else
            {
                SelectList list = new SelectList(items, dataValueField, dataTextField, selectedValue);
                return list;
            }
        }


    }
}
