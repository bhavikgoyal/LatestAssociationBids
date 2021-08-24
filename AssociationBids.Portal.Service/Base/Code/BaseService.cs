using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public class BaseService : IBaseService
    {
        #region Local Variables
        
        #endregion
        protected BaseService()
        {
            Errors = new List<ErrorModel>();
        }

        #region Properties
        protected bool IsValid
        {
            get { return (Errors.Count == 0); }
        }
        public string ConnectionString { get; set; }
        public IList<ErrorModel> Errors { get; set; }
        public SiteSettingsModel SiteSettings { get; set; }
        #endregion

        #region Methods
        public T CreateService<T>() where T : new()
        {
            T service = new T();

            ((IBaseService)service).SiteSettings = SiteSettings;
            ((IBaseService)service).ConnectionString = ConnectionString;

            return service;
        }

        public void ResetSiteSettings()
        {
            /*
            if (SiteSettings.SelectedPortalKey > 0)
            {
                SiteSettings.PortalKey = SiteSettings.SelectedPortalKey;
            }

            SiteSettings.SelectedPortalKey = 0;
            SiteSettings.PropertyKeyList = "";
            */
        }
        
        public void UpdateSiteSettings(int portalKey)
        {
            SiteSettings.SelectedPortalKey = portalKey;
        }

        public void UpdateSiteSettings(int portalKey, string title)
        {
            SiteSettings.SelectedPortalKey = portalKey;
            SiteSettings.SelectedPortalTitle = title;
        }

        public virtual T UpdateFilter<T>(T filter) where T : BaseFilterModel
        {
            filter.PortalKey = 0;
            filter.PropertyKeyList = "";

            if (SiteSettings.SelectedPortalKey > 0)
            {
                filter.PortalKey = SiteSettings.SelectedPortalKey;
            }
            else if (!String.IsNullOrEmpty(SiteSettings.PropertyKeyList))
            {
                filter.PropertyKeyList = SiteSettings.PropertyKeyList;
            }
            else
            {
                filter.PortalKey = SiteSettings.PortalKey;
            }

            return filter;
        }

        public void AddError(string key, string message)
        {
            AddError(new ErrorModel(key, message));
        }

        public void AddError(ErrorModel error)
        {
            if (!Errors.Contains(error))
            {
                Errors.Add(error);
            }
        }

        public void AddErrors(IList<ErrorModel> errors)
        {
            foreach (ErrorModel error in errors)
            {
                AddError(error);
            }
        }

        public void AddErrors(IList<ErrorModel> errors, string prefix)
        {
            foreach (ErrorModel error in errors)
            {
                AddError(new ErrorModel(String.Format("{0}.{1}", prefix, error.Key), error.ErrorMessage));
            }
        }
        #endregion
    }
}
