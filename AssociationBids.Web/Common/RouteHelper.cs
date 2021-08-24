using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections;

namespace AssociationBids.Portal.Web
{
    public class RouteHelper
    {
        #region Properties
        private string PortalID { get; set; }
        public string Controller { get; set; }
        private string SessionID { get; set; }
        private UrlHelper Url { get; set; }
        #endregion

        public RouteHelper(UrlHelper url, string portal, string controller)
        {
            Url = url;
            PortalID = portal;
            Controller = controller;
            SessionID = Guid.Empty.ToString();
        }

        public RouteHelper(UrlHelper url, string portal, string controller, string sessionID)
            : this(url, portal, controller)
        {
            SessionID = sessionID;
        }

        #region Base Methods
        public string GetUrl(string action)
        {
            return Url.Action(action, Controller, GetRoute(action, Controller));
        }
        public string GetUrl(string action, int id)
        {
            return Url.Action(action, Controller, GetRoute(action, Controller, id));
        }
        public string GetUrl(string action, string controller)
        {
            return Url.Action(action, controller, GetRoute(action, controller));
        }
        public string GetResetUrl(string action, string controller)
        {
            return Url.Action(action, controller, GetResetRoute(action, controller));
        }
        public string GetUrl(string action, string controller, int id)
        {
            return Url.Action(action, controller, GetRoute(action, controller, id));
        }
        public string GetUrl(string action, string controller, string id)
        {
            return Url.Action(action, controller, GetRoute(action, controller, id));
        }
        public string GetUrl(string action, string controller, int id, int id2)
        {
            return Url.Action(action, controller, GetRoute(action, controller, id, id2));
        }
        public string GetUrl(string action, string controller, int id, string id2)
        {
            return Url.Action(action, controller, GetRoute(action, controller, id, id2));
        }
        public string GetUrl(string action, string controller, int id, int id2, int id3)
        {
            return Url.Action(action, controller, GetRoute(action, controller, id, id2, id3));
        }
        public string GetEmailUrl(int id)
        {
            return Url.Action("Email", Controller, GetRoute("Email", Controller, id));
        }
        public string GetEmailUrl()
        {
            return Url.Action("Create", "EmailNotification", GetRoute("Create", "EmailNotification"));
        }
        public string GetEmailSendUrl()
        {
            return Url.Action("Send", "EmailNotification", GetRoute("Send", "EmailNotification"));
        }
        public object GetRoute(string action)
        {
            return GetRoute(action, Controller);
        }
        public object GetRoute(string action, int id)
        {
            return GetRoute(action, Controller, id);
        }
        public object GetRoute(string action, string controller)
        {
            return new { portal = PortalID, action = action, controller = controller, session = SessionID, token = GenerateUrlToken(action, controller) };
        }
        public object GetResetRoute(string action, string controller)
        {
            return new { portal = PortalID, action = action, controller = controller, session = SessionID, token = GenerateUrlToken(action, controller), reset = true };
        }
        public object GetRoute(string action, string controller, int id)
        {
            return new { portal = PortalID, action = action, controller = controller, session = SessionID, token = GenerateUrlToken(action, controller, id.ToString()), id = id };
        }
        public object GetRoute(string action, string controller, string id)
        {
            return new { portal = PortalID, action = action, controller = controller, session = SessionID, token = GenerateUrlToken(action, controller, id), id = id };
        }
        public object GetRoute(string action, string controller, int id, int id2)
        {
            return new { portal = PortalID, action = action, controller = controller, session = SessionID, token = GenerateUrlToken(action, controller, id.ToString()), id = id, id2 = id2 };
        }
        public object GetRoute(string action, string controller, int id, string id2)
        {
            return new { portal = PortalID, action = action, controller = controller, session = SessionID, token = GenerateUrlToken(action, controller, id.ToString()), id = id, id2 = id2 };
        }
        public object GetRoute(string action, string controller, int id, int id2, int id3)
        {
            return new { portal = PortalID, action = action, controller = controller, session = SessionID, token = GenerateUrlToken(action, controller, id.ToString()), id = id, id2 = id2, id3 = id3 };
        }
        #endregion

        #region Token Methods
        public bool IsValidUrlToken(string token, string actionName, string controllerName)
        {
            return IsValidUrlToken(token, actionName, controllerName, String.Empty);
        }
        public bool IsValidUrlToken(string token, string actionName, string controllerName, string id)
        {
            return token.Equals(GenerateUrlToken(actionName, controllerName, id));
        }
        public string GenerateUrlToken(string actionName, string controllerName)
        {
            return GenerateUrlToken(actionName, controllerName, String.Empty);
        }
        public string GenerateUrlToken(string actionName, string controllerName, string id)
        {
            ArrayList args = new ArrayList();
            if (String.IsNullOrEmpty(id))
            {
                return GenerateUrlToken(actionName, controllerName, args);
            }
            else
            {
                args.Add(id);
                return GenerateUrlToken(actionName, controllerName, args);
            }
        }
        public string GenerateUrlToken(string actionName, string controllerName, ArrayList args)
        {
            // TODO: Generate token
            return "";
        }
        #endregion

        #region Url Methods
        public string GetCreateUrl()
        {
            return GetUrl("Create");
        }
        public string GetFilterUrl()
        {
            return GetUrl("Filter");
        }
        public string GetListUrl()
        {
            return GetUrl("List");
        }
        public string GetPagingUrl()
        {
            return GetUrl("Paging");
        }

        public string GetFilterResetUrl()
        {
            return GetUrl("FilterReset");
        }
        public string GetEditUrl(int id)
        {
            return GetUrl("Edit", id);
        }
        public string GetDeleteUrl(int id)
        {
            return GetUrl("Delete", id);
        }
        public string GetDetailsUrl(int id)
        {
            return GetUrl("Details", id);
        }

        #endregion

        #region Route Methods
        public object GetCreateRoute()
        {
            return GetRoute("Create");
        }
        public object GetListRoute()
        {
            return GetRoute("List");
        }
        public object GetFilterRoute()
        {
            return GetRoute("Filter");
        }
        public object GetFilterResetRoute()
        {
            return GetRoute("FilterReset");
        }
        public object GetEditRoute(int id)
        {
            return GetRoute("Edit", id);
        }
        public object GetDeleteRoute(int id)
        {
            return GetRoute("Delete", id);
        }
        public object GetDetailsRoute(int id)
        {
            return GetRoute("Details", id);
        }

        #endregion
    }
}
