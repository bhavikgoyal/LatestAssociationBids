using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class UserAgreementService : BaseService, IUserAgreementService
    {
        protected IUserAgreementRepository __repository;

        public UserAgreementService()
            : this(new UserAgreementRepository()) { }

        public UserAgreementService(string connectionString)
            : this(new UserAgreementRepository(connectionString)) { }

        public UserAgreementService(IUserAgreementRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(UserAgreementModel item)
        {
            if (!Util.IsValidInt(item.UserKey))
            {
                AddError("UserKey", "User can not be empty.");
            }
            if (!Util.IsValidInt(item.AgreementKey))
            {
                AddError("AgreementKey", "Agreement can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(UserAgreementFilterModel filter)
        {
            UserAgreementFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.UserKey != filter.UserKey)
                return true;

            if (defaultFilter.AgreementKey != filter.AgreementKey)
                return true;

            return false;
        }

        public virtual UserAgreementFilterModel CreateFilter()
        {
            UserAgreementFilterModel filter = new UserAgreementFilterModel();

            return UpdateFilter(filter);
        }

        public virtual UserAgreementFilterModel CreateFilter(UserAgreementModel item)
        {
            UserAgreementFilterModel filter = new UserAgreementFilterModel();

            filter.UserKey = item.UserKey;
            filter.AgreementKey = item.AgreementKey;

            return UpdateFilter(filter);
        }

        public virtual UserAgreementFilterModel UpdateFilter(UserAgreementFilterModel filter)
        {
            return filter;
        }

        public virtual UserAgreementModel Create()
        {
            ResetSiteSettings();

            UserAgreementModel item = new UserAgreementModel();

            return item;
        }

        public virtual bool Create(UserAgreementModel item)
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

        public virtual bool Update(UserAgreementModel item)
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

        public virtual UserAgreementModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<UserAgreementModel> GetAll()
        {
            UserAgreementFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<UserAgreementModel> GetAll(UserAgreementFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<UserAgreementModel> GetAll(UserAgreementFilterModel filter, PagingModel paging)
        {
            IList<UserAgreementModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
