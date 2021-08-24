using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class ReminderService : BaseService, IReminderService
    {
        protected IReminderRepository __repository;

        public ReminderService()
            : this(new ReminderRepository()) { }

        public ReminderService(string connectionString)
            : this(new ReminderRepository(connectionString)) { }

        public ReminderService(IReminderRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(ReminderModel item)
        {
            if (!Util.IsValidInt(item.ModuleKey))
            {
                AddError("ModuleKey", "Module can not be empty.");
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

        public virtual bool IsFilterEnabled(ReminderFilterModel filter)
        {
            ReminderFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.ModuleKey != filter.ModuleKey)
                return true;

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.ObjectKey != filter.ObjectKey)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual ReminderFilterModel CreateFilter()
        {
            ReminderFilterModel filter = new ReminderFilterModel();

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

        public virtual ReminderFilterModel CreateFilter(ReminderModel item)
        {
            ReminderFilterModel filter = new ReminderFilterModel();

            filter.ModuleKey = item.ModuleKey;
            filter.ResourceKey = item.ResourceKey;
            filter.ObjectKey = item.ObjectKey;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual ReminderFilterModel UpdateFilter(ReminderFilterModel filter)
        {
            return filter;
        }

        public virtual ReminderModel Create()
        {
            ResetSiteSettings();

            ReminderModel item = new ReminderModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(ReminderModel item)
        {
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

        public virtual bool Update(ReminderModel item)
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

        public virtual ReminderModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<ReminderModel> GetAll()
        {
            ReminderFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<ReminderModel> GetAll(ReminderFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<ReminderModel> GetAll(ReminderFilterModel filter, PagingModel paging)
        {
            IList<ReminderModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
