using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class GroupModuleAccessService : BaseService, IGroupModuleAccessService
    {
        protected IGroupModuleAccessRepository __repository;

        public GroupModuleAccessService()
            : this(new GroupModuleAccessRepository()) { }

        public GroupModuleAccessService(string connectionString)
            : this(new GroupModuleAccessRepository(connectionString)) { }

        public GroupModuleAccessService(IGroupModuleAccessRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(GroupModuleAccessModel item)
        {
            if (!Util.IsValidInt(item.PortalKey))
            {
                AddError("PortalKey", "Portal can not be empty.");
            }
            if (!Util.IsValidInt(item.GroupKey))
            {
                AddError("GroupKey", "Group can not be empty.");
            }
            if (!Util.IsValidInt(item.ModuleKey))
            {
                AddError("ModuleKey", "Module can not be empty.");
            }
            if (!Util.IsValidInt(item.Access))
            {
                AddError("Access", "Access can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(GroupModuleAccessFilterModel filter)
        {
            GroupModuleAccessFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.GroupKey != filter.GroupKey)
                return true;

            if (defaultFilter.ModuleKey != filter.ModuleKey)
                return true;

            return false;
        }

        public virtual GroupModuleAccessFilterModel CreateFilter()
        {
            GroupModuleAccessFilterModel filter = new GroupModuleAccessFilterModel();

            filter.PortalKey = SiteSettings.CurrentPortalKey;

            return UpdateFilter(filter);
        }

        public virtual GroupModuleAccessFilterModel CreateFilter(GroupModuleAccessModel item)
        {
            GroupModuleAccessFilterModel filter = new GroupModuleAccessFilterModel();

            filter.PortalKey = item.PortalKey;
            filter.GroupKey = item.GroupKey;
            filter.ModuleKey = item.ModuleKey;

            return UpdateFilter(filter);
        }

        public virtual GroupModuleAccessFilterModel UpdateFilter(GroupModuleAccessFilterModel filter)
        {
            return filter;
        }

        public virtual GroupModuleAccessModel Create()
        {
            ResetSiteSettings();

            GroupModuleAccessModel item = new GroupModuleAccessModel();

            item.PortalKey = SiteSettings.CurrentPortalKey;

            return item;
        }

        public virtual bool Create(GroupModuleAccessModel item)
        {
            if (Validate(item))
            {
                UpdateSiteSettings(item.PortalKey);

                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(GroupModuleAccessModel item)
        {
            if (Validate(item))
            {
                UpdateSiteSettings(item.PortalKey);

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

        public virtual GroupModuleAccessModel Get(int id)
        {
            GroupModuleAccessModel item = __repository.Get(id);

            UpdateSiteSettings(item.PortalKey);

            return item;
        }

        public virtual IList<GroupModuleAccessModel> GetAll()
        {
            GroupModuleAccessFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<GroupModuleAccessModel> GetAll(GroupModuleAccessFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<GroupModuleAccessModel> GetAll(GroupModuleAccessFilterModel filter, PagingModel paging)
        {
            IList<GroupModuleAccessModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

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
