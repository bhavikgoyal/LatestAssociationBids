using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class PropertyService : BaseService, IPropertyService
    {
        protected IPropertyRepository __repository;

        public PropertyService()
            : this(new PropertyRepository()) { }

        public PropertyService(string connectionString)
            : this(new PropertyRepository(connectionString)) { }

        public PropertyService(IPropertyRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(PropertyModel item)
        {
            if (!Util.IsValidInt(item.CompanyKey))
            {
                AddError("CompanyKey", "Company can not be empty.");
            }
            if (!Util.IsValidText(item.Title))
            {
                AddError("Title", "Title can not be empty.");
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

        public virtual bool IsFilterEnabled(PropertyFilterModel filter)
        {
            PropertyFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.CompanyKey != filter.CompanyKey)
                return true;

            if (defaultFilter.State != filter.State)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual PropertyFilterModel CreateFilter()
        {
            PropertyFilterModel filter = new PropertyFilterModel();

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

        public virtual PropertyFilterModel CreateFilter(PropertyModel item)
        {
            PropertyFilterModel filter = new PropertyFilterModel();

            filter.CompanyKey = item.CompanyKey;
            filter.State = item.State;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual PropertyFilterModel UpdateFilter(PropertyFilterModel filter)
        {
            return filter;
        }

        public virtual PropertyModel Create()
        {
            ResetSiteSettings();

            PropertyModel item = new PropertyModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(PropertyModel item)
        {
            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(PropertyModel item)
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

        public virtual PropertyModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<PropertyModel> GetAll()
        {
            PropertyFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<PropertyModel> GetAll(PropertyFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<PropertyModel> GetAll(PropertyFilterModel filter, PagingModel paging)
        {
            IList<PropertyModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
