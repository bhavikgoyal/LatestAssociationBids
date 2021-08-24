using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AssociationBids.Portal.Web
{
    public class BaseController : Controller
    {
        #region Methods
        protected string GetRouteDataValue(RouteData routeData, string key)
        {
            return (routeData.Values[key] != null ? routeData.Values[key].ToString() : "");
        }

        protected int GetRouteDataValueInt(RouteData routeData, string key)
        {
            string value = GetRouteDataValue(routeData, key);
            return ((!String.IsNullOrEmpty(value)) ? int.Parse(value) : 0);
        }
        #endregion

        #region Session Functions
        protected void ClearSession()
        {
            System.Web.HttpContext.Current.Session.Abandon();
        }

        protected object GetSessionValue(string key)
        {
            return (System.Web.HttpContext.Current.Session[key]);
        }

        protected void SetSessionValue(string key, object data)
        {
            if (data == null)
            {
                RemoveFromSession(key);
            }
            else
            {
                System.Web.HttpContext.Current.Session[key] = data;
            }
        }

        protected void RemoveFromSession(string key)
        {
            System.Web.HttpContext.Current.Session.Remove(key);
        }
        #endregion

        #region Application Functions
        protected object GetApplicationValue(string key)
        {
            return (System.Web.HttpContext.Current.Application[key]);
        }

        protected void SetApplicationValue(string key, object data)
        {
            if (data == null)
            {
                RemoveFromApplication(key);
            }
            else
            {
                System.Web.HttpContext.Current.Application[key] = data;
            }
        }

        protected void RemoveFromApplication(string key)
        {
            System.Web.HttpContext.Current.Application.Remove(key);
        }
        #endregion

        #region Cache Functions
        public object GetCacheValue(string key)
        {
            return (System.Web.HttpContext.Current.Cache[key]);
        }

        public void SetCacheValue(string key, object data)
        {
            if (data == null)
            {
                RemoveFromCache(key);
            }
            else
            {
                System.Web.HttpContext.Current.Cache[key] = data;
            }
        }

        protected void RemoveFromCache(string key)
        {
            System.Web.HttpContext.Current.Cache.Remove(key);
        }
        #endregion
    }
}
