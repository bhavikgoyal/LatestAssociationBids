using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class CalendarService : BaseService, ICalendarService
    {
        protected ICalendarRepository __repository;

        public CalendarService()
            : this(new CalendarRepository()) { }

        public CalendarService(string connectionString)
            : this(new CalendarRepository(connectionString)) { }

        public CalendarService(ICalendarRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(CalendarModel item)
        {
            if (!Util.IsValidInt(item.ModuleKey))
            {
                AddError("ModuleKey", "Module can not be empty.");
            }
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }
            if (!Util.IsValidText(item.Subject))
            {
                AddError("Subject", "Subject can not be empty.");
            }
            if (!Util.IsValidDateTime(item.StartDate))
            {
                AddError("StartDate", "Start Date can not be empty.");
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

        public virtual bool IsFilterEnabled(CalendarFilterModel filter)
        {
            CalendarFilterModel defaultFilter = CreateFilter();

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

        public virtual CalendarFilterModel CreateFilter()
        {
            CalendarFilterModel filter = new CalendarFilterModel();

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

        public virtual CalendarFilterModel CreateFilter(CalendarModel item)
        {
            CalendarFilterModel filter = new CalendarFilterModel();

            filter.ModuleKey = item.ModuleKey;
            filter.ResourceKey = item.ResourceKey;
            filter.ObjectKey = item.ObjectKey;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual CalendarFilterModel UpdateFilter(CalendarFilterModel filter)
        {
            return filter;
        }

        public virtual CalendarModel Create()
        {
            ResetSiteSettings();

            CalendarModel item = new CalendarModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(CalendarModel item)
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

        public virtual bool Update(CalendarModel item)
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

        public virtual CalendarModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<CalendarModel> GetAll()
        {
            CalendarFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<CalendarModel> GetAll(CalendarFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<CalendarModel> GetAll(CalendarFilterModel filter, PagingModel paging)
        {
            IList<CalendarModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
