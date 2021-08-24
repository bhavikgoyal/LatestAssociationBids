using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class ModuleService : BaseService, IModuleService
    {
        protected IModuleRepository __repository;

        public ModuleService()
            : this(new ModuleRepository()) { }

        public ModuleService(string connectionString)
            : this(new ModuleRepository(connectionString)) { }

        public ModuleService(IModuleRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(ModuleModel item)
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

        public virtual bool IsFilterEnabled(ModuleFilterModel filter)
        {
            ModuleFilterModel defaultFilter = CreateFilter();

            return false;
        }

        public virtual ModuleFilterModel CreateFilter()
        {
            ModuleFilterModel filter = new ModuleFilterModel();

            return UpdateFilter(filter);
        }

        public virtual ModuleFilterModel CreateFilter(ModuleModel item)
        {
            ModuleFilterModel filter = new ModuleFilterModel();
            return UpdateFilter(filter);
        }

        public virtual ModuleFilterModel UpdateFilter(ModuleFilterModel filter)
        {
            return filter;
        }

        public virtual ModuleModel Create()
        {
            ResetSiteSettings();

            ModuleModel item = new ModuleModel();

            return item;
        }

        public virtual bool Create(ModuleModel item)
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

        public virtual bool Update(ModuleModel item)
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

        public virtual ModuleModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<ModuleModel> GetAll()
        {
            ModuleFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<ModuleModel> GetAll(ModuleFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<ModuleModel> GetAll(ModuleFilterModel filter, PagingModel paging)
        {
            IList<ModuleModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
