using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class PortalService : BaseService, IPortalService
    {
        protected IPortalRepository __repository;

        public PortalService()
            : this(new PortalRepository()) { }

        public PortalService(string connectionString)
            : this(new PortalRepository(connectionString)) { }

        public PortalService(IPortalRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(PortalModel item)
        {
            if (!Util.IsValidText(item.PortalID))
            {
                AddError("PortalID", "Portal can not be empty.");
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

        public virtual bool IsFilterEnabled(PortalFilterModel filter)
        {
            PortalFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual PortalFilterModel CreateFilter()
        {
            PortalFilterModel filter = new PortalFilterModel();

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

        public virtual PortalFilterModel CreateFilter(PortalModel item)
        {
            PortalFilterModel filter = new PortalFilterModel();
            filter.Status = item.Status;
            return UpdateFilter(filter);
        }

        public virtual PortalFilterModel UpdateFilter(PortalFilterModel filter)
        {
            return filter;
        }

        public virtual PortalModel Create()
        {
            ResetSiteSettings();

            PortalModel item = new PortalModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(PortalModel item)
        {
            item.DateAdded = DateTime.Now;
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(PortalModel item)
        {
            item.LastModificationTime = DateTime.Now;

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

        public virtual PortalModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<PortalModel> GetAll()
        {
            PortalFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<PortalModel> GetAll(PortalFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<PortalModel> GetAll(PortalFilterModel filter, PagingModel paging)
        {
            IList<PortalModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
