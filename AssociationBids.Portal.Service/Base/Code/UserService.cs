using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class UserService : BaseService, IUserService
    {
        protected IUserRepository __repository;

        public UserService()
            : this(new UserRepository()) { }

        public UserService(string connectionString)
            : this(new UserRepository(connectionString)) { }

        public UserService(IUserRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(UserModel item)
        {
            if (!Util.IsValidInt(item.ResourceKey))
            {
                AddError("ResourceKey", "Resource can not be empty.");
            }
            if (!Util.IsValidText(item.Username))
            {
                AddError("Username", "Username can not be empty.");
            }
            if (!Util.IsValidText(item.Password))
            {
                AddError("Password", "Password can not be empty.");
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

        public virtual bool IsFilterEnabled(UserFilterModel filter)
        {
            UserFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.ResourceKey != filter.ResourceKey)
                return true;

            if (defaultFilter.Username != filter.Username)
                return true;

            if (defaultFilter.TokenReset != filter.TokenReset)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual UserFilterModel CreateFilter()
        {
            UserFilterModel filter = new UserFilterModel();

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

        public virtual UserFilterModel CreateFilter(UserModel item)
        {
            UserFilterModel filter = new UserFilterModel();

            filter.ResourceKey = item.ResourceKey;
            filter.Username = item.Username;
            filter.TokenReset = item.TokenReset;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual UserFilterModel UpdateFilter(UserFilterModel filter)
        {
            return filter;
        }

        public virtual UserModel Create()
        {
            ResetSiteSettings();

            UserModel item = new UserModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(UserModel item)
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

        public virtual bool Update(UserModel item)
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

        public virtual UserModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<UserModel> GetAll()
        {
            UserFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<UserModel> GetAll(UserFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<UserModel> GetAll(UserFilterModel filter, PagingModel paging)
        {
            IList<UserModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
