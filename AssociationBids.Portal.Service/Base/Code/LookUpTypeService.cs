using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class LookUpTypeService : BaseService, ILookUpTypeService
    {
        protected ILookUpTypeRepository __repository;

        public LookUpTypeService()
            : this(new LookUpTypeRepository()) { }

        public LookUpTypeService(string connectionString)
            : this(new LookUpTypeRepository(connectionString)) { }

        public LookUpTypeService(ILookUpTypeRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(LookUpTypeModel item)
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

        public virtual bool IsFilterEnabled(LookUpTypeFilterModel filter)
        {
            LookUpTypeFilterModel defaultFilter = CreateFilter();

            return false;
        }

        public virtual LookUpTypeFilterModel CreateFilter()
        {
            LookUpTypeFilterModel filter = new LookUpTypeFilterModel();

            return UpdateFilter(filter);
        }

        public virtual LookUpTypeFilterModel CreateFilter(LookUpTypeModel item)
        {
            LookUpTypeFilterModel filter = new LookUpTypeFilterModel();
            return UpdateFilter(filter);
        }

        public virtual LookUpTypeFilterModel UpdateFilter(LookUpTypeFilterModel filter)
        {
            return filter;
        }

        public virtual LookUpTypeModel Create()
        {
            ResetSiteSettings();

            LookUpTypeModel item = new LookUpTypeModel();

            return item;
        }

        public virtual bool Create(LookUpTypeModel item)
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

        public virtual bool Update(LookUpTypeModel item)
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

        public virtual LookUpTypeModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<LookUpTypeModel> GetAll()
        {
            LookUpTypeFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<LookUpTypeModel> GetAll(LookUpTypeFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<LookUpTypeModel> GetAll(LookUpTypeFilterModel filter, PagingModel paging)
        {
            IList<LookUpTypeModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
