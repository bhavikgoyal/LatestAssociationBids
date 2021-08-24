using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class LookUpService : BaseService, ILookUpService
    {
        protected ILookUpRepository __repository;

        public LookUpService()
            : this(new LookUpRepository()) { }

        public LookUpService(string connectionString)
            : this(new LookUpRepository(connectionString)) { }

        public LookUpService(ILookUpRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(LookUpModel item)
        {
            if (!Util.IsValidInt(item.LookUpTypeKey))
            {
                AddError("LookUpTypeKey", "Look Up Type can not be empty.");
            }
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

        public virtual bool IsFilterEnabled(LookUpFilterModel filter)
        {
            LookUpFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.LookUpTypeKey != filter.LookUpTypeKey)
                return true;

            return false;
        }

        public virtual LookUpFilterModel CreateFilter()
        {
            LookUpFilterModel filter = new LookUpFilterModel();

            return UpdateFilter(filter);
        }

        public virtual LookUpFilterModel CreateFilter(LookUpModel item)
        {
            LookUpFilterModel filter = new LookUpFilterModel();

            filter.LookUpTypeKey = item.LookUpTypeKey;

            return UpdateFilter(filter);
        }

        public virtual LookUpFilterModel UpdateFilter(LookUpFilterModel filter)
        {
            return filter;
        }

        public virtual LookUpModel Create()
        {
            ResetSiteSettings();

            LookUpModel item = new LookUpModel();

            return item;
        }

        public virtual bool Create(LookUpModel item)
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

        public virtual bool Update(LookUpModel item)
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

        public virtual LookUpModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<LookUpModel> GetAll()
        {
            LookUpFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<LookUpModel> GetAll(LookUpFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<LookUpModel> GetAll(LookUpFilterModel filter, PagingModel paging)
        {
            IList<LookUpModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
