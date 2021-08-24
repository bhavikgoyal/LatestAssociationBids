using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class StateService : BaseService, IStateService
    {
        protected IStateRepository __repository;

        public StateService()
            : this(new StateRepository()) { }

        public StateService(string connectionString)
            : this(new StateRepository(connectionString)) { }

        public StateService(IStateRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(StateModel item)
        {
            if (!Util.IsValidText(item.Title))
            {
                AddError("Title", "Title can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(StateFilterModel filter)
        {
            StateFilterModel defaultFilter = CreateFilter();

            return false;
        }

        public virtual StateFilterModel CreateFilter()
        {
            StateFilterModel filter = new StateFilterModel();

            return UpdateFilter(filter);
        }

        public virtual StateFilterModel CreateFilter(StateModel item)
        {
            StateFilterModel filter = new StateFilterModel();
            return UpdateFilter(filter);
        }

        public virtual StateFilterModel UpdateFilter(StateFilterModel filter)
        {
            return filter;
        }

        public virtual StateModel Create()
        {
            ResetSiteSettings();

            StateModel item = new StateModel();

            return item;
        }

        public virtual bool Create(StateModel item)
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

        public virtual bool Update(StateModel item)
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

        public virtual bool Delete(string id)
        {
            return __repository.Delete(id);
        }

        public virtual StateModel Get(string id)
        {
            return __repository.Get(id);
        }

        public virtual IList<StateModel> GetAll()
        {
            StateFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<StateModel> GetAll(StateFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<StateModel> GetAll(StateFilterModel filter, PagingModel paging)
        {
            IList<StateModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
