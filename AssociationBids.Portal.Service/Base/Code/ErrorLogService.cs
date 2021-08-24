using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class ErrorLogService : BaseService, IErrorLogService
    {
        protected IErrorLogRepository __repository;

        public ErrorLogService()
            : this(new ErrorLogRepository()) { }

        public ErrorLogService(string connectionString)
            : this(new ErrorLogRepository(connectionString)) { }

        public ErrorLogService(IErrorLogRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(ErrorLogModel item)
        {
            if (!Util.IsValidText(item.Details))
            {
                AddError("Details", "Details can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(ErrorLogFilterModel filter)
        {
            ErrorLogFilterModel defaultFilter = CreateFilter();

            return false;
        }

        public virtual ErrorLogFilterModel CreateFilter()
        {
            ErrorLogFilterModel filter = new ErrorLogFilterModel();

            return UpdateFilter(filter);
        }

        public virtual ErrorLogFilterModel CreateFilter(ErrorLogModel item)
        {
            ErrorLogFilterModel filter = new ErrorLogFilterModel();
            return UpdateFilter(filter);
        }

        public virtual ErrorLogFilterModel UpdateFilter(ErrorLogFilterModel filter)
        {
            return filter;
        }

        public virtual ErrorLogModel Create()
        {
            ResetSiteSettings();

            ErrorLogModel item = new ErrorLogModel();

            return item;
        }

        public virtual bool Create(ErrorLogModel item)
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

        public virtual bool Update(ErrorLogModel item)
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

        public virtual ErrorLogModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<ErrorLogModel> GetAll()
        {
            ErrorLogFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<ErrorLogModel> GetAll(ErrorLogFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<ErrorLogModel> GetAll(ErrorLogFilterModel filter, PagingModel paging)
        {
            IList<ErrorLogModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
