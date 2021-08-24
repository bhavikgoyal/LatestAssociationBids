using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class LoginHistoryService : BaseService, ILoginHistoryService
    {
        protected ILoginHistoryRepository __repository;

        public LoginHistoryService()
            : this(new LoginHistoryRepository()) { }

        public LoginHistoryService(string connectionString)
            : this(new LoginHistoryRepository(connectionString)) { }

        public LoginHistoryService(ILoginHistoryRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(LoginHistoryModel item)
        {
            if (!Util.IsValidInt(item.UserKey))
            {
                AddError("UserKey", "User can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(LoginHistoryFilterModel filter)
        {
            LoginHistoryFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.UserKey != filter.UserKey)
                return true;

            return false;
        }

        public virtual LoginHistoryFilterModel CreateFilter()
        {
            LoginHistoryFilterModel filter = new LoginHistoryFilterModel();

            return UpdateFilter(filter);
        }

        public virtual LoginHistoryFilterModel CreateFilter(LoginHistoryModel item)
        {
            LoginHistoryFilterModel filter = new LoginHistoryFilterModel();

            filter.UserKey = item.UserKey;

            return UpdateFilter(filter);
        }

        public virtual LoginHistoryFilterModel UpdateFilter(LoginHistoryFilterModel filter)
        {
            return filter;
        }

        public virtual LoginHistoryModel Create()
        {
            ResetSiteSettings();

            LoginHistoryModel item = new LoginHistoryModel();

            return item;
        }

        public virtual bool Create(LoginHistoryModel item)
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

        public virtual bool Update(LoginHistoryModel item)
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

        public virtual LoginHistoryModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<LoginHistoryModel> GetAll()
        {
            LoginHistoryFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<LoginHistoryModel> GetAll(LoginHistoryFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<LoginHistoryModel> GetAll(LoginHistoryFilterModel filter, PagingModel paging)
        {
            IList<LoginHistoryModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
