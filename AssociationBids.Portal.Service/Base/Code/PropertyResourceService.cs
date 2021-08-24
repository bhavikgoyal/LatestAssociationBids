using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class PropertyResourceService : BaseService, IPropertyResourceService
    {
        protected IPropertyResourceRepository __repository;

        public PropertyResourceService()
            : this(new PropertyResourceRepository()) { }

        public PropertyResourceService(string connectionString)
            : this(new PropertyResourceRepository(connectionString)) { }

        public PropertyResourceService(IPropertyResourceRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(PropertyResourceModel item)
        {
            if (!Util.IsValidInt(item.PropertyKey))
            {
                AddError("PropertyKey", "Property can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }
            if (!Util.IsValidInt(item.Status))
            {
                AddError("Status", "Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(PropertyResourceFilterModel filter)
        {
            PropertyResourceFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.PropertyKey != filter.PropertyKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual PropertyResourceFilterModel CreateFilter()
        {
            PropertyResourceFilterModel filter = new PropertyResourceFilterModel();

            if (SiteSettings.AdminPortal)
            {
                filter.Status = (int)LookUpType.RecordStatus.PendingApprovalOrApproved;
            }
            else
            {
                filter.Status = (int)LookUpType.RecordStatus.Approved;
            }

            return UpdateFilter(filter);
        }

        public virtual PropertyResourceFilterModel CreateFilter(PropertyResourceModel item)
        {
            PropertyResourceFilterModel filter = new PropertyResourceFilterModel();

            filter.PropertyKey = item.PropertyKey;
            filter.ResourceKey = item.ResourceKey;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual PropertyResourceFilterModel UpdateFilter(PropertyResourceFilterModel filter)
        {
            return filter;
        }

        public virtual PropertyResourceModel Create()
        {
            ResetSiteSettings();

            PropertyResourceModel item = new PropertyResourceModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(PropertyResourceModel item)
        {
            item.DateAdded = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(PropertyResourceModel item)
        {
            if (Validate(item))
            {
                return __repository.Update(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            return __repository.Delete(id);
        }

        public virtual PropertyResourceModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<PropertyResourceModel> GetAll()
        {
            PropertyResourceFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<PropertyResourceModel> GetAll(PropertyResourceFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<PropertyResourceModel> GetAll(PropertyResourceFilterModel filter, PagingModel paging)
        {
            IList<PropertyResourceModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

            // Account for last record update on a page
            if (itemList.Count == 0 && paging.TotalRecordCount > 0)
            {
                paging.PageNumber--;
                return GetAll(filter, paging);
            }
            else
            {
                return itemList;
            }
        }
    }
}
